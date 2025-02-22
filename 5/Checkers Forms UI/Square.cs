﻿namespace Checkers
{
    public class Square
    {
        private eSquareOwner m_SquareOwner;
        private ePieceInSquare m_PieceInSquare;
        public eSquareOwner SquareOwner
        {
            get { return m_SquareOwner; }
            set
            {
                m_SquareOwner = value;
            }
        }

        public ePieceInSquare PieceInSquare
        {
            get { return m_PieceInSquare; }
            set
            {
                m_PieceInSquare = value;
            }
        }
    }
}
