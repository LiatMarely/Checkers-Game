using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CheckersGame;
using CheckersGame.Enums;

namespace Ex05_Liat_207918608.FormUI
{
    public partial class Form1 : Form
    {
        ShowMessages m_ShowMessages = new ShowMessages();

        public Form1()
        {
            InitializeComponent();
            textBoxPlayer1.Height = labelPlayer1.Height;
            textBoxPlayer2.Height = checkboxPlayer2.Height;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void checkboxPlayer2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkboxPlayer2.Checked)
            {
                textBoxPlayer2.Enabled = true;
                textBoxPlayer2.ReadOnly = false;
                textBoxPlayer2.Text = string.Empty;
            }
            else
            {
                textBoxPlayer2.Text = "[Computer]";
                textBoxPlayer2.ReadOnly = true;
                textBoxPlayer2.Enabled = false;
            }

        }

        private void buttonDone_Click(object i_Sender, EventArgs i_E)
        {
            if (!(radioButton10X10.Checked || radioButton6X6.Checked || radioButton8X8.Checked) || textBoxPlayer1.Text == string.Empty)
            {
                m_ShowMessages.PrintError("All fields must be filled");
                return;  
            }
            else
            {
                int boardSize = radioButton6X6.Checked ? 6 : radioButton8X8.Checked ? 8 : 10;
                string player1Name = textBoxPlayer1.Text;
                string player2Name = checkboxPlayer2.Checked ? textBoxPlayer2.Text : "Computer";
                Board board = new Board(boardSize);
                Player player1 = new Player(player1Name, true, ePieceType.X, board);
                Player player2 = checkboxPlayer2.Checked
                    ? new Player(player2Name, true, ePieceType.O, board)
                    : new Player("Computer", false, ePieceType.O, board, new RandomMoveStrategy());
                Game game = new Game(board, new Player[] { player1, player2 });
                FormBoardGame gameForm = new FormBoardGame(game);
                gameForm.Show();
                this.Hide();
            }
        }
    }
}


