using CheckersGame.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace CheckersGame
{
    public class Game
    {
        private readonly Board r_Board;
        private readonly Player[] r_Players;
        private IGameState m_CurrentState;
        private int m_CurrentPlayerIndex;
        public int[] Scores { get; set; }
        public Player[] Players { get { return r_Players; } }
       

        public Board Board => r_Board;

        public Game(Board i_Board, Player[] i_Players)
        {
            r_Board = i_Board;
            r_Players = i_Players;
            Scores = new int[i_Players.Length];
            m_CurrentPlayerIndex = 0;
            m_CurrentState = new PlayerTurnState();

        }

        public void SetState(IGameState i_NewState)
        {
            m_CurrentState = i_NewState;
            m_CurrentState.Handle(this);
        }

        public Player GetCurrentPlayer()
        {
            return r_Players[m_CurrentPlayerIndex];
        }

        public Player GetNextPlayer()
        {
            int nextPlayerIndex = (m_CurrentPlayerIndex + 1) % r_Players.Length;
            return r_Players[nextPlayerIndex];
        }

        public Player GetLoser(Player i_Winner)
        {
            if (r_Players[m_CurrentPlayerIndex].Equals(i_Winner))
            {
                int nextPlayerIndex = (m_CurrentPlayerIndex + 1) % r_Players.Length;
                return r_Players[nextPlayerIndex];
            }
            return r_Players[m_CurrentPlayerIndex];
        }

        public Player GetWinner()
        {
            int player1Pieces = CountPlayerPieces(r_Players[0]);
            int player2Pieces = CountPlayerPieces(r_Players[1]);
            bool player1HasMoves = HasValidMoves(r_Players[0]);
            bool player2HasMoves = HasValidMoves(r_Players[1]);

            Player winner = null;

            if (player1Pieces > 0 && player2Pieces == 0)
            {
                winner = r_Players[0];
            }
            if (player2Pieces > 0 && player1Pieces == 0)
            {
                winner = r_Players[1];
            }

            if (!player1HasMoves && player2HasMoves)
            {
                winner = r_Players[1];
            }

            if (!player2HasMoves && player1HasMoves)
            {
                winner = r_Players[0];
            }

            if (!player1HasMoves && !player2HasMoves)
            {
                winner = null;
            }

            return winner;
        }


        public void SwitchToNextPlayer()
        {
            m_CurrentPlayerIndex = (m_CurrentPlayerIndex + 1) % r_Players.Length;
        }

        public void RecordMove(Move i_Move)
        {
            r_Board.SetLastMove(i_Move);
        }

        public bool IsGameOver()
        {
            bool player1HasMoves = HasValidMoves(r_Players[0]);
            bool player2HasMoves = HasValidMoves(r_Players[1]);

            return !player1HasMoves || !player2HasMoves || CountPieces(r_Players[0]) == 0 || CountPieces(r_Players[1]) == 0;
        }


        public void ResetGame()
        {
            r_Board.InitializeBoard();
            m_CurrentPlayerIndex = 0;
            m_CurrentState = new PlayerTurnState();
        }

        private bool HasValidMoves(Player i_Player)
        {
            bool hasValidMoves = false;

            for (int row = 0; row < r_Board.Size; row++)
            {
                for (int col = 0; col < r_Board.Size; col++)
                {
                    Piece piece = r_Board.GetPieceAt(new Position(row, col));

                    if (piece != null && piece.Type == i_Player.PieceType)
                    {
                        if (r_Board.GetValidMoves(new Position(row, col), i_Player).Count > 0)
                        {
                            hasValidMoves = true;
                        }
                    }
                }
            }

            return hasValidMoves;
        }

        public int CountPieces(Player i_Player)
        {
            int count = 0;

            for (int row = 0; row < r_Board.Size; row++)
            {
                for (int col = 0; col < r_Board.Size; col++)
                {
                    Piece piece = r_Board.GetPieceAt(new Position(row, col));
                    if (piece != null)
                    {
                        if (i_Player.PieceType == ePieceType.X)
                        {
                            if (piece.Type == ePieceType.X)
                            {
                                count++;
                            }
                            else if (piece.Type == ePieceType.KingX)
                            {
                                count += 4;
                            }
                        }

                        if (i_Player.PieceType == ePieceType.O)
                        {
                            if (piece.Type == ePieceType.O)
                            {
                                count++;
                            }
                            else if (piece.Type == ePieceType.KingO)
                            {
                                count += 4;
                            }
                        }
                    }
                }
            }

            return count;
        }

        public int CountPlayerPieces(Player i_Player)
        {
            int count = 0;

            for (int row = 0; row < r_Board.Size; row++)
            {
                for (int col = 0; col < r_Board.Size; col++)
                {
                    Piece piece = r_Board.GetPieceAt(new Position(row, col));
                    if (piece != null)
                    {
                        if (i_Player.PieceType == ePieceType.X)
                        {
                            if (piece.Type == ePieceType.X || piece.Type == ePieceType.KingX)
                            {
                                count++;
                            }
                        }

                        if (i_Player.PieceType == ePieceType.O)
                        {
                            if (piece.Type == ePieceType.O || piece.Type == ePieceType.KingO)
                            {
                                count++;
                            }
                        }
                    }
                }
            }

            return count;

        }

    }
}
