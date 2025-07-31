using System;
using System.Collections.Generic;
using System.Linq;
using  Ex05_Liat_207918608.FormUI;

namespace CheckersGame
{
    public class PlayerTurnState : IGameState
    {
        public List<Position> NextCaptures { get; private set; }
        public Position ActivePiecePosition { get; private set; }
        private readonly Move r_ExitMove = new Move(new Position(-1, -1), new Position(-1, -1));
        ShowMessages m_ShowMessages = new ShowMessages();

        public PlayerTurnState()
        {
            NextCaptures = new List<Position>();
            ActivePiecePosition = default(Position);
        }

        public PlayerTurnState(List<Position> i_NextCaptures, Position i_ActivePiecePosition)
        {
            NextCaptures = i_NextCaptures;
            ActivePiecePosition = i_ActivePiecePosition;
        }
        private List<Position> getMandatoryCapturePositionsForPiece(Board i_Board, Player i_Player, Position i_PiecePosition)
        {
            List<Position> captureMoves = i_Board.GetValidDestinations(i_PiecePosition, i_Player)
                .Where(dest => Math.Abs(dest.Row - i_PiecePosition.Row) == 2)
                .ToList();

            return captureMoves;
        }

        public void Handle(Game i_Game)
        {
            Player currentPlayer = i_Game.GetCurrentPlayer();
            Board board = i_Game.Board;
            string input = string.Empty;
            bool isValidMove = false;
            Move move = null;

            if (!currentPlayer.IsHuman)
            {
                handleComputerTurn(i_Game, currentPlayer, board);
            }
            else
            {
                List<Position> mandatoryCaptures = getMandatoryCapturePositions(board, currentPlayer);

                do
                {
                    //i_Game.UpdateBoard();
                    //i_Game.View.PrintMessage($"It's {currentPlayer.Name}'s turn ({currentPlayer.PieceType}):");

                    if (NextCaptures.Any())
                    {
                        m_ShowMessages.PrintError("You must continue capturing with the same piece!");
                    }
                    else if (mandatoryCaptures.Any())
                    {
                        m_ShowMessages.PrintError("You must use a piece that can capture an opponent's piece!");
                        m_ShowMessages.PrintError($"Pieces that can capture: {string.Join(", ", mandatoryCaptures)}");
                    }

                    if (input.ToUpper() == "Q")
                    {
                        //i_Game.View.PrintMessage($"{currentPlayer.Name} has resigned. The opponent wins!");
                        i_Game.SetState(new GameOverState(i_Game.GetNextPlayer()));
                        return;
                    }

                    try
                    {
                        move = currentPlayer.ParseMove(input, board);

                        if (move.Equals(r_ExitMove))
                        {
                           // i_Game.View.PrintMessage($"{currentPlayer.Name} has resigned. The opponent wins!");
                            i_Game.SetState(new GameOverState(i_Game.GetNextPlayer()));
                            return;
                        }

                        if (NextCaptures.Any() && !move.From.Equals(ActivePiecePosition))
                        {
                            m_ShowMessages.PrintError("You must continue capturing with the same piece!");
                            continue;
                        }
                        else if (mandatoryCaptures.Any() && !mandatoryCaptures.Contains(move.From))
                        {
                            m_ShowMessages.PrintError("You must use a piece that can capture an opponent's piece!");
                            continue;
                        }

                        isValidMove = board.MovePiece(move.From, move.To, currentPlayer, i_Game, NextCaptures);

                        if (isValidMove)
                        {
                            ActivePiecePosition = move.To;
                            mandatoryCaptures.Add(ActivePiecePosition);

                            NextCaptures = getMandatoryCapturePositionsForPiece(board, currentPlayer, ActivePiecePosition);
                            if (!NextCaptures.Any())
                            {
                                break;
                            }
                        }
                        else
                        {
                            m_ShowMessages.PrintError("Invalid move. Please try again.");
                        }
                    }
                    catch (Exception ex)
                    {
                        m_ShowMessages.PrintError($"Error: {ex.Message}");
                    }
                } while (NextCaptures.Any() || !isValidMove);
            }
            //i_Game.View.PrintBoard(board);
            i_Game.RecordMove(move);

            if (i_Game.IsGameOver())
            {
                i_Game.SetState(new GameOverState());
            }
            else
            {
                i_Game.SwitchToNextPlayer();
                i_Game.SetState(new PlayerTurnState());
            }
        }

      
        private void handleComputerTurn(Game i_Game, Player i_Computer, Board i_Board)
        {
            //i_Game.View.PrintBoard(i_Board);
            //i_Game.View.PrintMessage("Computer's Turn (press 'enter' to see its move)");
            Console.ReadLine();

            Move computerMove = i_Computer.GetMove(i_Board);
            //i_Game.View.PrintMessage($"Computer chose: {computerMove.From}>{computerMove.To}");
            bool isValidMove = i_Board.MovePiece(computerMove.From, computerMove.To, i_Computer, i_Game, NextCaptures);

            if (isValidMove && NextCaptures.Any())
            {
                ActivePiecePosition = computerMove.To;
                i_Game.SetState(new PlayerTurnState(NextCaptures, ActivePiecePosition));
            }
            else
            {
                i_Game.RecordMove(computerMove);
                if (i_Game.IsGameOver())
                {
                    i_Game.SetState(new GameOverState());
                }
                else
                {
                    i_Game.SwitchToNextPlayer();
                    i_Game.SetState(new PlayerTurnState());
                }
            }
        }

        private List<Position> getMandatoryCapturePositions(Board i_Board, Player i_Player)
        {
            List<Position> mandatoryCaptures = new List<Position>();

            for (int row = 0; row < i_Board.Size; row++)
            {
                for (int col = 0; col < i_Board.Size; col++)
                {
                    Position position = new Position(row, col);
                    Piece piece = i_Board.GetPieceAt(position);

                    if (piece != null && piece.Type == i_Player.PieceType)
                    {
                        List<Position> captureMoves = i_Board.GetValidDestinations(position, i_Player)
                            .Where(dest => Math.Abs(dest.Row - position.Row) == 2)
                            .ToList();

                        if (captureMoves.Any())
                        {
                            mandatoryCaptures.Add(position);
                        }
                    }
                }
            }

            return mandatoryCaptures;
        }
    }

}
