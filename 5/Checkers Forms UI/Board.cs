using System;
using System.Windows.Forms;

namespace Checkers
{
    public class Board
    {
        private int m_BoardSize = 0;
        private Square[,] m_GameBoard = null;

        public Board(int i_BoardSize)
        {
            BoardSize = i_BoardSize;
            m_GameBoard = new Square[BoardSize, BoardSize];
        }

        public Square[,] GameBoard
        {
            get { return m_GameBoard; }
            set
            {
                m_GameBoard = value;
            }
        }

        public int BoardSize
        {
            get { return m_BoardSize; }
            set
            {
                m_BoardSize = value;
            }
        }

        internal void SetBoard()
        {
            createEmptyBoard();
            placePiecesOnBoard();
        }

        private void createEmptyBoard()
        {
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    GameBoard[i, j] = new Square();
                }
            }
        }

        private void placePiecesOnBoard()
        {
            for (int row = m_BoardSize - 1; row >= 0; row--)
            {
                for (int col = m_BoardSize - 1; col >= 0; col--)
                { 
                    if ((row + col) % 2 == 1)
                    {
                        if (row < (m_BoardSize / 2) - 1)
                        {
                            GameBoard[row, col].SquareOwner = eSquareOwner.Player2;
                            GameBoard[row, col].PieceInSquare = ePieceInSquare.RegularPiece;
                        }
                        else if (row > (m_BoardSize / 2))
                        {
                            GameBoard[row, col].SquareOwner = eSquareOwner.Player1;
                            GameBoard[row, col].PieceInSquare = ePieceInSquare.RegularPiece;
                        }
                    }
                }
            }
        }

        internal int CalculatePoints(Player i_Player1, Player i_Player2)
        {
            for (int row = 0; row < m_BoardSize; row++)
            {
                for (int col = 0; col < m_BoardSize; col++)
                {
                    if (this.GameBoard[row, col].SquareOwner == eSquareOwner.Player1)
                    {
                        this.addPoints(i_Player1, row, col);
                    }
                    else if (this.GameBoard[row, col].SquareOwner == eSquareOwner.Player2)
                    {
                        this.addPoints(i_Player2, row, col);
                    }
                }
            }

            return Math.Abs(i_Player1.NumOfPoints - i_Player2.NumOfPoints);
        }

        private void addPoints(Player i_Player, int i_Row, int i_Col)
        {
            if (this.GameBoard[i_Row, i_Col].PieceInSquare == ePieceInSquare.RegularPiece)
            {
                i_Player.NumOfPoints++;
            }
            else if (this.GameBoard[i_Row, i_Col].PieceInSquare == ePieceInSquare.KingPiece)
            {
                i_Player.NumOfPoints += 4;
            }
        }

        internal void ResetBoard()
        {
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    GameBoard[i, j].PieceInSquare = ePieceInSquare.Empty;
                    GameBoard[i, j].SquareOwner = eSquareOwner.None;
                }
            }

            this.placePiecesOnBoard();
        }
    }
}
