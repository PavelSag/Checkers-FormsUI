namespace Checkers
{
    public class Player
    {
        private string m_PlayerName = null;
        private int m_Score = 0;
        private int m_NumOfPiecesOnBoard = 0;
        private bool m_IsVersusTheComputer = false;

        public Player(bool i_IsVersusComputer, string i_Player1Name)
        {
            if (i_IsVersusComputer != true)
            {
                PlayerName = i_Player1Name;
                m_IsVersusTheComputer = false;
            }
            else
            {
                PlayerName = "Computer";
                m_IsVersusTheComputer = true;
            }

            NumOfPoints = 0;
        }

        public bool IsVersusTheComputer
        {
            get { return m_IsVersusTheComputer; }
            set
            {
                m_IsVersusTheComputer = value;
            }
        }

        public string PlayerName
        {
            get { return m_PlayerName; }
            set
            {
                m_PlayerName = value;
            }
        }

        internal int NumOfPoints
        {
            get { return m_Score; }
            set
            {
                m_Score = value;
            }
        }

        internal int NumOfPiecesOnBoard
        {
            get { return m_NumOfPiecesOnBoard; }
            set
            {
                m_NumOfPiecesOnBoard = value;
            }
        }

        internal void SetNumOfPieces(int i_BoardSize)
        {
            NumOfPiecesOnBoard = (i_BoardSize / 2 - 1) * (i_BoardSize / 2);
        }

        internal void ResetNumOfPiecesOnBoard(int i_BoardSize)
        {
            SetNumOfPieces(i_BoardSize);
        }
    }
}
