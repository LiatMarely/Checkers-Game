using CheckersGame.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CheckersGame
{
    public class Board
    {
        private const int k_DefaultSize = 10;
        private int m_Size;
        private Piece[,] m_Grid;
        private Move m_LastMove;
        public Piece[,] Grid { get { return m_Grid; } }
        public Move LastMove { get { return m_LastMove; } }


        public int Size
        {
            get { return m_Size; }
        }

        public Board(int i_Size = k_DefaultSize)
        {
            m_Size = i_Size;
            m_Grid = new Piece[m_Size, m_Size];
            InitializeBoard();
        }

        public void InitializeBoard()
        {
            for (int row = 0; row < m_Size; row++)
            {
                for (int col = 0; col < m_Size; col++)
                {
                    m_Grid[row, col] = null;

                    if ((row + col) % 2 != 0)
                    {
                        if (row < m_Size / 2 - 1)
                        {
                            m_Grid[row, col] = new Piece(ePieceType.O, new Position(row, col));
                        }
                        else if (row > m_Size / 2)
                        {
                            m_Grid[row, col] = new Piece(ePieceType.X, new Position(row, col));
                        }
                    }
                }
            }
        }

        public int CountPieces(ePieceType i_Type)
        {
            int count = 0;

            for (int row = 0; row < m_Size; row++)
            {
                for (int col = 0; col < m_Size; col++)
                {
                    if (m_Grid[row, col]?.Type == i_Type)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        public List<Position> GetValidDestinations(Position i_From, Player i_CurrentPlayer)
        {
            List<Position> captureDestinations = new List<Position>();
            List<Position> regularDestinations = new List<Position>();
            Position[] directions;
            bool hasCaptures = false;

            Piece piece = GetPieceAt(i_From);

            if (piece != null)
            {
                if (piece.IsKing)
                {
                    directions = new Position[]
                    {
                        new Position(-1, -1),
                        new Position(-1, 1),
                        new Position(1, -1),
                        new Position(1, 1)
                    };
                }
                else if (piece.Type == ePieceType.X)
                {
                    directions = new Position[]
                    {
                        new Position(-1, -1),
                        new Position(-1, 1)
                    };
                }
                else
                {
                    directions = new Position[]
                    {
                        new Position(1, -1),
                        new Position(1, 1)
                    };
                }

                foreach (Position direction in directions)
                {
                    Position to = new Position(i_From.Row + 2 * direction.Row, i_From.Col + 2 * direction.Col);
                    if (IsMoveValid(i_From, to, i_CurrentPlayer))
                    {
                        captureDestinations.Add(to);
                        hasCaptures = true;
                    }
                }

                if (!hasCaptures)
                {
                    foreach (Position direction in directions)
                    {
                        Position to = new Position(i_From.Row + direction.Row, i_From.Col + direction.Col);
                        if (IsMoveValid(i_From, to, i_CurrentPlayer))
                        {
                            regularDestinations.Add(to);
                        }
                    }
                }
            }

            return hasCaptures ? captureDestinations : regularDestinations;
        }
        public Piece GetPieceAt(Position i_Position)
        {
            Piece piece = null;

            if (i_Position.Row >= 0 && i_Position.Row < m_Size &&
                i_Position.Col >= 0 && i_Position.Col < m_Size)
            {
                piece = m_Grid[i_Position.Row, i_Position.Col];
            }
            else
            {
               // m_View.PrintMessage("Error: Position is out of bounds.");
            }

            return piece;
        }
        private bool isPositionValid(Position i_Position)
        {
            bool isValid = i_Position.Row >= 0 && i_Position.Row < m_Size &&
                           i_Position.Col >= 0 && i_Position.Col < m_Size;

            return isValid;
        }



        public bool IsMoveValid(Position i_From, Position i_To, Player i_CurrentPlayer)
        {
            if (!isPositionValid(i_From) || !isPositionValid(i_To))
            {
                return false;
            }

            Piece piece = m_Grid[i_From.Row, i_From.Col];
            if (piece == null)
            {
                return false;
            }

            if (i_CurrentPlayer.PieceType == ePieceType.X)
            {
                if (piece.Type != i_CurrentPlayer.PieceType && piece.Type != ePieceType.KingX)
                {
                    return false;
                }
            }

            if (i_CurrentPlayer.PieceType == ePieceType.O)
            {
                if (piece.Type != i_CurrentPlayer.PieceType && piece.Type != ePieceType.KingO)
                {
                    return false;
                }
            }

            int rowDiff = i_To.Row - i_From.Row;
            int colDiff = Math.Abs(i_To.Col - i_From.Col);

            if (Math.Abs(rowDiff) == 1 && colDiff == 1)
            {
                if (piece.IsKing)
                {
                   return m_Grid[i_To.Row, i_To.Col] == null;
                }
                else if (piece.Type == ePieceType.X && rowDiff == -1)
                {
                    return m_Grid[i_To.Row, i_To.Col] == null;
                }
                else if (piece.Type == ePieceType.O && rowDiff == 1)
                {
                    return m_Grid[i_To.Row, i_To.Col] == null;
                }
            }

            if (Math.Abs(rowDiff) == 2 && colDiff == 2)
            {
                int midRow = (i_From.Row + i_To.Row) / 2;
                int midCol = (i_From.Col + i_To.Col) / 2;
                Piece middlePiece = m_Grid[midRow, midCol];

                if (middlePiece != null && middlePiece.Type != piece.Type && !isTheSamePlayer(middlePiece,piece))
                {
                    if (piece.IsKing)
                    {
                        return m_Grid[i_To.Row, i_To.Col] == null;
                    }
                    else if (piece.Type == ePieceType.X && rowDiff == -2)
                    {
                        return m_Grid[i_To.Row, i_To.Col] == null;
                    }
                    else if (piece.Type == ePieceType.O && rowDiff == 2)
                    {
                        return m_Grid[i_To.Row, i_To.Col] == null;
                    }
                }
            }

            return false;
        }

        private bool isTheSamePlayer(Piece i_MiddlePiece, Piece i_Piece)
        {
            if (i_MiddlePiece.Type == ePieceType.X && i_Piece.Type == ePieceType.KingX)
            {
                return true;
            }
            if (i_MiddlePiece.Type == ePieceType.KingX && i_Piece.Type == ePieceType.X)
            {
                return true;
            }
            if (i_MiddlePiece.Type == ePieceType.O && i_Piece.Type == ePieceType.KingO)
            {
                return true;
            }
            if (i_MiddlePiece.Type == ePieceType.KingO && i_Piece.Type == ePieceType.O)
            {
                return true;
            }
            return false;
        }


        public bool MovePiece(Position i_From, Position i_To, Player i_CurrentPlayer, Game i_Game, List<Position> i_NextCaptures)
        {
            List<Position> captureMoves = GetValidDestinations(i_From, i_CurrentPlayer)
                .Where(dest => Math.Abs(dest.Row - i_From.Row) == 2)
                .ToList();

            if (captureMoves.Count > 0 && Math.Abs(i_To.Row - i_From.Row) != 2)
            {
                //m_View.PrintMessage("You must perform a capture if one is available.");
                return false;
            }

            if (!IsMoveValid(i_From, i_To, i_CurrentPlayer))
            {
                //i_Game.View.PrintInvalidMove(i_From, i_To);
                return false;
            }

            Piece piece = m_Grid[i_From.Row, i_From.Col];
            m_Grid[i_From.Row, i_From.Col] = null;
            m_Grid[i_To.Row, i_To.Col] = piece;
            piece.Position = i_To;

            if (Math.Abs(i_From.Row - i_To.Row) == 2 && Math.Abs(i_From.Col - i_To.Col) == 2)
            {
                int midRow = (i_From.Row + i_To.Row) / 2;
                int midCol = (i_From.Col + i_To.Col) / 2;
                m_Grid[midRow, midCol] = null;

                List<Position> nextCaptures = GetValidDestinations(i_To, i_CurrentPlayer)
                    .Where(dest => Math.Abs(dest.Row - i_To.Row) == 2)
                    .ToList();

                if (nextCaptures.Any())
                {
                    i_NextCaptures.Clear();
                    i_NextCaptures.AddRange(nextCaptures);
                    return true;
                }
            }

            i_NextCaptures.Clear();

            SetLastMove(new Move(i_From, i_To));
            CheckAndPromote(i_To);
            return true;
        }


        public List<Position> GetValidMoves(Position i_From, Player i_CurrentPlayer)
        {
            List<Position> validMoves = new List<Position>();
            Position[] directions = { new Position(-1, -1), new Position(-1, 1), new Position(1, -1), new Position(1, 1) };

            foreach (Position direction in directions)
            {
                Position to = new Position(i_From.Row + direction.Row, i_From.Col + direction.Col);

                if (IsMoveValid(i_From, to, i_CurrentPlayer))
                {
                    validMoves.Add(to);
                }
            }

            return validMoves;
        }

        public void SetLastMove(Move i_Move)
        {
            m_LastMove = i_Move;
        }

        public void CheckAndPromote(Position i_Position)
        {
            Piece piece = GetPieceAt(i_Position);

            if (piece != null && !piece.IsKing)
            {
                bool shouldPromote = (piece.Type == ePieceType.X && i_Position.Row == 0) || (piece.Type == ePieceType.O && i_Position.Row == m_Size - 1);

                if (shouldPromote)
                {
                    piece.PromoteToKing();
                   // m_View.PrintMessage($"Piece at {i_Position} promoted to King!");
                }
            }
        }

        public bool HasMandatoryCapture(Player currentPlayer)
        {
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    Position from = new Position(row, col);
                    Piece piece = GetPieceAt(from);

                    if (piece != null && piece.Type == currentPlayer.PieceType)
                    {
                        List<Position> captureMoves = GetValidDestinations(from, currentPlayer)
                            .Where(dest => Math.Abs(dest.Row - from.Row) == 2)
                            .ToList();

                        if (captureMoves.Any())
                        {
                            return true; 
                        }
                    }
                }
            }
            return false; 
        }

    }
}

