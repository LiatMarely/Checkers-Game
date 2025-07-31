using CheckersGame.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CheckersGame
{
    public class Player
    {
        private readonly string r_Name;
        private readonly bool r_IsHuman;
        private IStrategy m_Strategy;
        private readonly Board r_Board;
       
        public ePieceType PieceType { get; }
        private readonly Move r_ExitMove = new Move(new Position(-1, -1), new Position(-1, -1));

        public Player(string i_Name, bool i_IsHuman, ePieceType i_PieceType, Board i_Board, IStrategy i_Strategy = null)
        {
            r_Name = i_Name;
            r_IsHuman = i_IsHuman;
            PieceType = i_PieceType;
            r_Board = i_Board;
            m_Strategy= i_Strategy;
        }

        public string Name
        {
            get { return r_Name; }
        }

        public bool IsHuman
        {
            get { return r_IsHuman; }
        }

        public IStrategy Strategy
        {
            get { return m_Strategy; }
            set { m_Strategy = value; }
        }

        public Move GetMove(Board i_Board)
        {
            Move nextMove = null;
            if (r_IsHuman)
            {
                //m_View.PrintMessage($"{r_Name}, Enter your move (e.g., Fa>Fb): ");
                string input = Console.ReadLine();
                nextMove = ParseMove(input, i_Board);
            }
            else
            {
                if (m_Strategy == null)
                {
                    throw new InvalidOperationException("Strategy is not set for AI player.");
                }
                nextMove = m_Strategy.GetMove(i_Board, this);
            }

            return nextMove;
        }

        public Move ParseMove(string i_Input, Board i_Board)
        {
            Move parsedMove = null;

            try
            {
                if (i_Input.ToUpper() == "Q")
                {
                    parsedMove = r_ExitMove;
                }
                else
                {
                    string[] parts = i_Input
                        .Split('>')
                        .Select(part => part.Trim())
                        .ToArray();

                    if (parts.Length != 2)
                    {
                        throw new FormatException("Invalid format. Please use the format 'ROWcol>ROWcol' (e.g., Fa>Fb).");
                    }

                    Position from = parsePosition(parts[0]);
                    Position to = parsePosition(parts[1]);

                    if (!i_Board.IsMoveValid(from, to, this))
                    {
                        //m_View.PrintMessage($"Invalid move from {from} to {to}. Please try again.");
                        parsedMove = GetMove(i_Board);
                    }
                    else
                    {
                        parsedMove = new Move(from, to);
                    }
                }
            }
            catch (Exception ex)
            {
                //m_View.PrintMessage($"Error: {ex.Message}");
                parsedMove = GetMove(i_Board);
            }

            return parsedMove;
        }


        private Position parsePosition(string i_Input)
        {
            Position parsedPosition;

            if (i_Input.Length != 2)
            {
                throw new FormatException($"Invalid position '{i_Input}'. Please use a valid format like 'Fa'.");
            }

            char row = i_Input[0];
            char col = i_Input[1];

            int rowIndex = row - 'A';
            int colIndex = col - 'a';

            bool isOutOfBounds = rowIndex < 0 || rowIndex >= r_Board.Size || colIndex < 0 || colIndex >= r_Board.Size;

            if (isOutOfBounds)
            {
                throw new ArgumentOutOfRangeException($"Position '{i_Input}' is out of bounds.");
            }

            parsedPosition = new Position(rowIndex, colIndex);

            return parsedPosition;
        }

    }
}
