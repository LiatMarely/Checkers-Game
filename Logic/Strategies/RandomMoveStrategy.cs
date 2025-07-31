using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckersGame
{
    public class RandomMoveStrategy : IStrategy
    {
        private static readonly Random s_Random = new Random();

        public Move GetMove(Board i_Board, Player i_CurrentPlayer)
        {
            List<Move> mandatoryMoves = GetMandatoryMoves(i_Board, i_CurrentPlayer);

            if (mandatoryMoves.Count > 0)
            {
                int randomIndex = s_Random.Next(mandatoryMoves.Count);
                return mandatoryMoves[randomIndex];
            }

            List<Move> validMoves = GetAllValidMoves(i_Board, i_CurrentPlayer);

            if (validMoves.Count == 0)
            {
                throw new InvalidOperationException("No valid moves available for AI.");
            }

            int validMoveIndex = s_Random.Next(validMoves.Count);
            return validMoves[validMoveIndex];
        }

        private List<Move> GetMandatoryMoves(Board i_Board, Player i_CurrentPlayer)
        {
            List<Move> mandatoryMoves = new List<Move>();

            for (int row = 0; row < i_Board.Size; row++)
            {
                for (int col = 0; col < i_Board.Size; col++)
                {
                    Position from = new Position(row, col);
                    Piece piece = i_Board.GetPieceAt(from);

                    if (piece != null && piece.Type == i_CurrentPlayer.PieceType)
                    {
                        List<Position> captureDestinations = i_Board.GetValidDestinations(from, i_CurrentPlayer)
                            .Where(dest => Math.Abs(dest.Row - from.Row) == 2)
                            .ToList();

                        foreach (Position to in captureDestinations)
                        {
                            mandatoryMoves.Add(new Move(from, to));
                        }
                    }
                }
            }

            return mandatoryMoves;
        }

        public List<Move> GetAllValidMoves(Board i_Board, Player i_CurrentPlayer)
        {
            List<Move> validMoves = new List<Move>();

            for (int row = 0; row < i_Board.Size; row++)
            {
                for (int col = 0; col < i_Board.Size; col++)
                {
                    Position from = new Position(row, col);
                    Piece piece = i_Board.GetPieceAt(from);

                    if (piece != null && piece.Type == i_CurrentPlayer.PieceType)
                    {
                        List<Position> destinations = i_Board.GetValidDestinations(from, i_CurrentPlayer);

                        foreach (Position to in destinations)
                        {
                            validMoves.Add(new Move(from, to));
                        }
                    }
                }
            }

            return validMoves;
        }
    }

}
