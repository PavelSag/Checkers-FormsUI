using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Checkers
{
    public partial class GameSettingsForm : Form
    {
        public GameSettingsForm()
        {
            InitializeComponent();
        }

        private void GameSettingsForm_Load(object sender, EventArgs e)
        {

        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            UserInterface userInterface = new UserInterface();
            this.Hide();
            Board board = new Board(getBoardSize());
            Player player1 = new Player(false, getPlayerName(ePlayerType.Player1));
            Player player2 = createSecondPlayer();

            player1.SetNumOfPieces(board.BoardSize);
            player2.SetNumOfPieces(board.BoardSize);

            board.SetBoard();
            userInterface.InitializeUserInteface(board, player1, player2);
            userInterface.CreateBoard(board.BoardSize);

            setUpGame(userInterface, board, player1, player2);
        }

        private void setUpGame(UserInterface i_UserInterface, Board i_Board, Player i_Player1, Player i_Player2)
        {
            bool isAnotherGame = true;

            while (isAnotherGame)
            {
                isAnotherGame = i_UserInterface.RunGame(i_Board, i_Player1, i_Player2);
            }
        } 

        private Player createSecondPlayer()
        {
            Player player2;

            if(checkBoxPlayer2.Checked)
            {
                player2 = new Player(false, getPlayerName(ePlayerType.Player2));
            }
            else
            {
                player2 = new Player(true, getPlayerName(ePlayerType.Computer));
            }

            return player2;
        }

        private int getBoardSize()
        {
            int boardSize = 0;

            if (this.radioButton6By6.Checked)
            {
                boardSize = 6;
            }
            else if (this.radioButton8By8.Checked)
            {
                boardSize = 8;
            }
            else if (this.radioButton10By10.Checked)
            {
                boardSize = 10;
            }
            else
            {
                boardSize = 6;
            }

            return boardSize;
        }

        private string getPlayerName(ePlayerType i_PlayerType)
        {
            string playerName = null;

            if(i_PlayerType == ePlayerType.Player1)
            {
                playerName = textBoxPlayer1Name.Text;
            }
            else if(i_PlayerType == ePlayerType.Player2)
            {
                playerName = textBoxPlayer2Name.Text;
            }
            else
            {
                playerName = "Computer";
            }

            return playerName;
        }

        private void checkBoxPlayer2_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxPlayer2.Checked)
            {
                textBoxPlayer2Name.Enabled = true;
            }
            else
            {
                textBoxPlayer2Name.Enabled = false;
            }
        }


    }
}
