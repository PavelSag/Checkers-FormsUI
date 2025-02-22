using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Checkers
{
    public partial class UserInterface : Form
    {
        private Label m_Player1ScoreLabel = null;
        private Label m_Player2ScoreLabel = null;
        private bool m_IsVersusComputer = false;
        private bool m_IsPieceCaptured = false;
        private bool m_IsFirstButtonPressed = false;
        private bool m_IsSecondButtonPressed = false;
        private OriginAndDestinationSquare m_FromAndToSquare = new OriginAndDestinationSquare();
        private List<OriginAndDestinationSquare> m_CapturesArray = null;
        private bool m_IsLegal = false;
        private GameLogic m_GameLogic = null;
        private Button m_FirstClickedButton = null;
    
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public Board GameBoard { get; set; }

        public bool IsLegal
        {
            get { return m_IsLegal; }
            set
            {
                m_IsLegal = value;
            }
        }

        public bool IsVersusComputer
        {
            get { return m_IsVersusComputer; }
            set
            {
                m_IsVersusComputer = value;
            }
        }

        public bool IsPieceCaptured
        {
            get { return m_IsPieceCaptured; }
            set
            {
                m_IsPieceCaptured = value;
            }
        }

        public UserInterface()
        {
            InitializeComponent();
            initializeFromAndToSquare();
            m_GameLogic = new GameLogic();
        }

        internal void InitializeUserInteface(Board i_Board, Player i_Player1, Player i_Player2)
        {
            GameBoard = i_Board;
            Player1 = i_Player1;
            Player2 = i_Player2;
        }

        private void UserInterface_Load(object sender, EventArgs e)
        {

        }

        private void UserInterface_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        internal bool RunGame(Board i_Board, Player i_Player1, Player i_Player2)
        {
            GameLogic.s_ComputerIndicator = i_Player2.IsVersusTheComputer;
            ShowDialog();

            return m_GameLogic.IsAnotherGame;
        }

        private bool showWinner()
        {
            bool returnFlag = false;
            string winnerText = string.Format($"{m_GameLogic.Winner.PlayerName} Won!{Environment.NewLine}Another Round?");
            DialogResult winnerResult = MessageBox.Show(winnerText, "Checkers", MessageBoxButtons.YesNo);

            if(winnerResult == DialogResult.Yes)
            {
                returnFlag = true;
            }
            else
            {
                returnFlag = false;
            }

            return returnFlag;
        }

        private bool showDraw()
        {
            bool returnFlag = false;
            string winnerText = string.Format($"Tie!{Environment.NewLine}Another Round?");
            DialogResult winnerResult = MessageBox.Show(winnerText, "Checkers", MessageBoxButtons.YesNo);

            if (winnerResult == DialogResult.Yes)
            {
                returnFlag = true;
            }
            else
            {
                returnFlag = false;
            }

            return returnFlag;
        }

        internal void CreateBoard(int i_NumberOfButtonsInOneLine)
        {
            int buttonSize = 45;

            int panelSize = buttonSize * i_NumberOfButtonsInOneLine;
            panelGameBoard.Size = new Size(panelSize, panelSize);

            this.ClientSize = new Size(panelSize + 50, panelSize + 150);
            panelGameBoard.Location = new Point((this.ClientSize.Width - panelGameBoard.Width) / 2, 100);

            addButtonsToPanel(i_NumberOfButtonsInOneLine, buttonSize);
            addPlayerScoreLabels();
        }

        private void addButtonsToPanel(int i_NumberOfButtonsInOneLine, int i_ButtonSize)
        {
            for (int row = 0; row < i_NumberOfButtonsInOneLine; row++)
            {
                for (int col = 0; col < i_NumberOfButtonsInOneLine; col++)
                {
                    Button square = new Button
                    {
                        Size = new Size(i_ButtonSize, i_ButtonSize),
                        Location = new Point(col * i_ButtonSize, row * i_ButtonSize),
                        BackColor = (row + col) % 2 == 1 ? Color.White : Color.Black,
                        Tag = (row, col)
                    };

                    square.Click += square_Click;
                    panelGameBoard.Controls.Add(square);
                }
            }

            updateSymbolsOnBoard();
        }

        private void updateSymbolsOnBoard()
        {
            Button currentButton = null;

            for(int row = 0; row < GameBoard.BoardSize; row++)
            {
                for(int col = 0; col < GameBoard.BoardSize; col++)
                {
                    currentButton = getButtonWithGivenRowAndCol(row, col);

                    if (GameBoard.GameBoard[row,col].SquareOwner == eSquareOwner.Player1)
                    {
                        if(GameBoard.GameBoard[row, col].PieceInSquare == ePieceInSquare.RegularPiece)
                        {  
                            currentButton.Text = "O";
                        }
                        else if(GameBoard.GameBoard[row, col].PieceInSquare == ePieceInSquare.KingPiece)
                        {
                            currentButton.Text = "U";
                        }
                    }
                    else if(GameBoard.GameBoard[row, col].SquareOwner == eSquareOwner.Player2)
                    {
                        if (GameBoard.GameBoard[row, col].PieceInSquare == ePieceInSquare.RegularPiece)
                        {
                            currentButton.Text = "X";
                        }
                        else if (GameBoard.GameBoard[row, col].PieceInSquare == ePieceInSquare.KingPiece)
                        {
                            currentButton.Text = "K";
                        }
                    }
                    else
                    {
                        currentButton.Text = "";
                    }
                }
            }
        }

        private Button getButtonWithGivenRowAndCol(int i_Row, int i_Col)
        {
            Button returnButton = null;

            foreach (Button button in panelGameBoard.Controls)
            {
                (int row, int col) position = (ValueTuple<int, int>)button.Tag;

                if (position.row == i_Row && position.col == i_Col)
                {
                    returnButton = button;
                }
            }

            return returnButton;
        }

        private void addPlayerScoreLabels()
        {
            m_Player1ScoreLabel = new Label
            {
                Text = String.Format($"{Player1.PlayerName}: {Player1.NumOfPoints}"),
                Location = new Point(panelGameBoard.Width / 4, panelGameBoard.Location.Y - 50),
                AutoSize = true

            };
            m_Player2ScoreLabel = new Label
            {
                Text = String.Format($"{Player2.PlayerName}: {Player2.NumOfPoints}"),
                Location = new Point(panelGameBoard.Width / 2 + panelGameBoard.Width / 4, panelGameBoard.Location.Y - 50),
                AutoSize = true
            };

            this.Controls.Add(m_Player1ScoreLabel);
            this.Controls.Add(m_Player2ScoreLabel);
        }

        private void handleFirstPressedButton(Button i_ClickedButton)
        {
            (int row, int col) rowAndCol = (ValueTuple<int, int>)i_ClickedButton.Tag;

            m_FromAndToSquare.m_RowOrigin = rowAndCol.row;
            m_FromAndToSquare.m_ColOrigin = rowAndCol.col;
            i_ClickedButton.BackColor = Color.LightBlue;
            m_IsFirstButtonPressed = true;
            m_FirstClickedButton = i_ClickedButton;
            disableIrrelevantButtons(i_ClickedButton);
        }

        private eSquareOwner getSquareOwnerOfCurrentButton(Button i_Button)
        {
            (int row, int col) rowAndCol = (ValueTuple<int, int>)i_Button.Tag;

            return GameBoard.GameBoard[rowAndCol.row, rowAndCol.col].SquareOwner;
        }

        private void disableIrrelevantButtons(Button i_ClickedButton)
        {
            eSquareOwner squareOwner = getSquareOwnerOfCurrentButton(i_ClickedButton);
            List<OriginAndDestinationSquare> possibleSquaresToMoveTo = new List<OriginAndDestinationSquare>();
            possibleSquaresToMoveTo = m_GameLogic.GetArrayOfAllPossibleMovesOnBoard(possibleSquaresToMoveTo, GameBoard, squareOwner);

            foreach(Button button in panelGameBoard.Controls)
            {
                button.Enabled = false;
                button.BackColor = Color.LightGray;
            }

            (int row, int col) rowAndCol = (ValueTuple<int, int>)i_ClickedButton.Tag;

            foreach (OriginAndDestinationSquare possibleMove in possibleSquaresToMoveTo)
            {
                if(possibleMove.m_RowOrigin == rowAndCol.row && possibleMove.m_ColOrigin == rowAndCol.col)
                {
                    Button buttonToEnable = getButtonWithGivenRowAndCol(possibleMove.m_RowDestination, possibleMove.m_ColDestination);
                    buttonToEnable.Enabled = true;
                    buttonToEnable.BackColor = SystemColors.Control;
                }
            }

            i_ClickedButton.Enabled = true;
            i_ClickedButton.BackColor = SystemColors.Control;
        }

        private bool handleClickOnSameSquare(Button i_ClickedButton)
        {
            bool isCanceledMove = false;
            (int row, int col) rowAndCol = (ValueTuple<int, int>)m_FirstClickedButton.Tag;

            if (m_FromAndToSquare.m_RowOrigin == m_FromAndToSquare.m_RowDestination &&
                m_FromAndToSquare.m_ColOrigin == m_FromAndToSquare.m_ColDestination)
            {
                m_FirstClickedButton.BackColor = (rowAndCol.row + rowAndCol.col) % 2 == 1 ? Color.White : Color.Black;
                isCanceledMove = true;
                enableAllButtons();
            }

            return isCanceledMove;
        }

        private void handleSecondPressedButton(Button i_ClickedButton)
        {
            bool isPieceCaptured = false;
            string illegalMoveMessage = null;
            bool isCanceledMove = false;
            (int row, int col) rowAndCol = (ValueTuple<int, int>)i_ClickedButton.Tag;

            m_FromAndToSquare.m_RowDestination = rowAndCol.row;
            m_FromAndToSquare.m_ColDestination = rowAndCol.col;
            m_IsSecondButtonPressed = true;

            isCanceledMove = handleClickOnSameSquare(i_ClickedButton);

            if(isCanceledMove != true)
            {
                IsLegal = m_GameLogic.CheckIfLegalAndMove(ref isPieceCaptured, GameBoard, m_FromAndToSquare, out illegalMoveMessage, m_CapturesArray);
                if (IsLegal != true)
                {
                    enableAllButtons();
                    MessageBox.Show(illegalMoveMessage, "Illegal Move");
                }
                else
                {
                    updateSymbolsOnBoard();
                    changeTurn(isPieceCaptured);
                    handleCapture(ref isPieceCaptured);
                    if(GameLogic.s_TurnStatus != eTurnStatus.Player1Turn)
                    {
                        getComputerMoveIfVersusComputer();
                    }
                    
                    m_GameLogic.UpdateWinnerAndLoserIfVictory(Player1, Player2, GameBoard);
                    checkVictory();
                    enableAllButtons();
                }

                m_FirstClickedButton.BackColor = SystemColors.Control;
                m_FirstClickedButton = null;
            }
        }

        private void enableAllButtons()
        {
            foreach(Button button in panelGameBoard.Controls)
            {
                button.Enabled = true;
                (int row, int col) rowAndCol = (ValueTuple<int, int>)button.Tag;
                button.BackColor = (rowAndCol.row + rowAndCol.col) % 2 == 1 ? Color.White : Color.Black;
            }
        }

        private void changeTurn(bool i_IsPieceCaptured)
        {
            if(i_IsPieceCaptured != true)
            {
                m_GameLogic.ChangeTurn();
            }
        }

        private void handleCapture(ref bool io_IsPieceCaptured)
        {
            List<OriginAndDestinationSquare> captureOptionsArray = new List<OriginAndDestinationSquare>();

            if(io_IsPieceCaptured == true)
            {
                m_GameLogic.DecrementNumOfPieces(Player1, Player2);

                if (m_GameLogic.FindDoubleCapture(GameBoard, captureOptionsArray, m_FromAndToSquare) == true)
                {
                    m_CapturesArray = captureOptionsArray;
                }
                else
                {
                    m_CapturesArray = null;
                    io_IsPieceCaptured = false;
                    m_GameLogic.ChangeTurn();
                }
            }
        }

        private void getComputerMoveIfVersusComputer()
        {
            bool isPieceCaptured = false;

            while (GameLogic.s_TurnStatus == eTurnStatus.Player2Turn && GameLogic.s_ComputerIndicator == true)
            {
                m_FromAndToSquare = m_GameLogic.GenerateComputerMove(GameBoard);

                if(m_FromAndToSquare.m_ColOrigin != -1 && m_FromAndToSquare.m_RowOrigin != -1)
                {
                    isPieceCaptured = m_GameLogic.MovePiece(m_FromAndToSquare, GameBoard);
                }
                
                updateSymbolsOnBoard();
                changeTurn(isPieceCaptured);
                handleCapture(ref isPieceCaptured);

                if(m_GameLogic.Winner != null)
                {
                    m_GameLogic.UpdateWinnerAndLoserIfVictory(Player1, Player2, GameBoard);
                    checkVictory();
                }
            }
        }

        private void square_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;

            if(clickedButton != null)
            {
                if (m_IsFirstButtonPressed != true)
                {
                    handleFirstPressedButton(clickedButton);
                }
                else if(m_IsSecondButtonPressed != true)
                {
                    handleSecondPressedButton(clickedButton);
                }
                
                processMove(clickedButton);
            }
        }

        private void processMove(Button i_ClickedButtton)
        {
            if (m_IsFirstButtonPressed && m_IsSecondButtonPressed)
            {
                m_IsFirstButtonPressed = false;
                m_IsSecondButtonPressed = false;
                initializeFromAndToSquare();
            }
            
            if(m_GameLogic.IsAnotherGame == true)
            {
                m_GameLogic.Winner = null;
                m_GameLogic.Loser = null;
                m_GameLogic.IsAnotherGame = false;
            }
        }

        private void handleVictory()
        {
            if(m_GameLogic.IsAnotherGame)
            {
                int points = GameBoard.CalculatePoints(m_GameLogic.Winner, m_GameLogic.Loser);

                if (m_GameLogic.Winner == Player1)
                {
                    Player1.NumOfPoints += points;
                }
                else
                {
                    Player2.NumOfPoints += points;
                }

                updateScoreboard();
                GameBoard.ResetBoard();
                Player1.ResetNumOfPiecesOnBoard(GameBoard.BoardSize);
                Player2.ResetNumOfPiecesOnBoard(GameBoard.BoardSize);
                updateSymbolsOnBoard();
                resetTurns(m_GameLogic.Winner);
            }
        }

        private void resetTurns(Player i_Winner)
        {
            if(i_Winner == Player1)
            {
                GameLogic.s_TurnStatus = eTurnStatus.Player1Turn;
            }
            else
            {
                GameLogic.s_TurnStatus = eTurnStatus.Player2Turn;
            }
        }

        private void updateScoreboard()
        {
            if (m_Player1ScoreLabel != null && m_Player2ScoreLabel != null)
            {
                m_Player1ScoreLabel.Text = $"{Player1.PlayerName}: {Player1.NumOfPoints}";
                m_Player2ScoreLabel.Text = $"{Player2.PlayerName}: {Player2.NumOfPoints}";
            }
        }

        private void checkVictory()
        {
            if (m_GameLogic.Winner != null)
            {
                if (m_GameLogic.Winner != null)
                {
                    m_GameLogic.IsAnotherGame = showWinner();

                    if(m_GameLogic.IsAnotherGame == false)
                    {
                        Application.Exit();
                    }
                }
                else
                {
                    m_GameLogic.IsAnotherGame = showDraw();
                }

                handleVictory();

                if(m_GameLogic.IsAnotherGame == true)
                {
                    m_GameLogic.IsGameGoing = true;
                }
                else
                {
                    m_GameLogic.IsGameGoing = false;
                }
                
            }
        }

        private void initializeFromAndToSquare()
        {
            m_FromAndToSquare.m_RowOrigin = -1;
            m_FromAndToSquare.m_ColOrigin = -1;
            m_FromAndToSquare.m_ColDestination = -1;
            m_FromAndToSquare.m_RowDestination = -1;
        }
    }
}
