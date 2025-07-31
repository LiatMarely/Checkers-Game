using CheckersGame.Enums;


namespace CheckersGame
{
    public class Piece
    {
        private ePieceType m_Type;
        private Position m_Position;

        public Piece(ePieceType i_Type, Position i_Position)
        {
            m_Type = i_Type;
            m_Position = i_Position;
        }

        public ePieceType Type
        {
            get { return m_Type; }
        }

        public Position Position
        {
            get { return m_Position; }
            set { m_Position = value; }
        }

        public bool IsKing
        {
            get { return m_Type == ePieceType.KingX || m_Type == ePieceType.KingO; }
        }

        public void PromoteToKing()
        {
            if (m_Type == ePieceType.X)
            {
                m_Type = ePieceType.KingX;
            }
            else if (m_Type == ePieceType.O)
            {
                m_Type = ePieceType.KingO;
            }
        }

        public override string ToString()
        {
            string pieceTypeString = null;
            switch (m_Type)
            {
                case ePieceType.X:
                    pieceTypeString = "X";
                    break;
                case ePieceType.O:
                    pieceTypeString = "O";
                    break;
                case ePieceType.KingX:
                    pieceTypeString = "K";
                    break;
                case ePieceType.KingO:
                    pieceTypeString = "U";
                    break;
                default:
                    pieceTypeString = " ";
                    break;
            }

            return pieceTypeString;
        }

    }


}
