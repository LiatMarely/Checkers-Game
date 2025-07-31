using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckersGame
{
    public struct Position
    {
        private readonly int r_Row;
        private readonly int r_Col;

        public Position(int i_Row, int i_Col)
        {
            r_Row = i_Row;
            r_Col = i_Col;
        }

        public int Row
        {
            get { return r_Row; }
        }

        public int Col
        {
            get { return r_Col; }
        }

        public override string ToString()
        {
            return $"({(char)(r_Row + 'A')}, {(char)(r_Col + 'a')})";
        }
        public override bool Equals(object obj)
        {
            if (obj is Position other)
            {
                return Row == other.Row && Col == other.Col;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (Row, Col).GetHashCode();
        }
    }
}
