using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Windows.Forms;
using CheckersGame;
using CheckersGame.Enums;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace Ex05_Liat_207918608.FormUI
{
    public partial class FormBoardGame : Form
    {
        private Game m_Game;
        private Label m_LabelPlayer1;
        private Button[,] m_BoardButtons;
        private Position? m_SelectedPosition = null;
        private Label m_LabelPlayer2;
        private Panel m_PanelBoard;
        private ShowMessages m_ShowMessages = new ShowMessages();
        GameOverState m_GameOverState;


        public FormBoardGame(Game i_Game)
        {
            InitializeComponent();
            m_Game = i_Game;
            InitializeBoard();
            UpdateBoard();
            UpdateScoreBoard();
        }

        private void InitializeBoard()
        {
            //Designe of the form
            int boardSize = m_Game.Board.Size;
            this.Text = "Checkers Game";
            this.Size = new Size(boardSize * 60 + 50, boardSize * 60 + 50);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
           
            m_PanelBoard = new Panel();
            m_PanelBoard.Size = new Size(boardSize * 50, boardSize * 50);
            m_PanelBoard.Top = (this.ClientSize.Height - m_PanelBoard.Height) / 2;
            m_PanelBoard.Left = (this.ClientSize.Width - m_PanelBoard.Width) / 2;
            m_PanelBoard.BackColor = Color.DarkKhaki;

            m_LabelPlayer1 = new Label();
            m_LabelPlayer1.Text = "Player 1: 0";
            m_LabelPlayer1.AutoSize = true;
            m_LabelPlayer1.Top = 15;
            m_LabelPlayer1.Left = m_PanelBoard.Left;
            m_LabelPlayer1.BackgroundImage = Properties.Resources.Beige1;
            m_LabelPlayer1.Font = new Font("Stencil", 12, FontStyle.Regular);
            m_LabelPlayer1.ForeColor = Color.DarkGreen;
           
            m_LabelPlayer2 = new Label();
            m_LabelPlayer2.Text = "Player 2: 0";
            m_LabelPlayer2.AutoSize = true;
            m_LabelPlayer2.Top = 15;
            m_LabelPlayer2.Left = m_PanelBoard.Right - m_LabelPlayer2.Width;
            m_LabelPlayer2.BackgroundImage = Properties.Resources.Beige1;
            m_LabelPlayer2.Font = new Font("Stencil", 12, FontStyle.Regular);
            m_LabelPlayer2.ForeColor = Color.DarkGreen;

            this.Controls.Add(m_PanelBoard);
            this.Controls.Add(m_LabelPlayer1);
            this.Controls.Add(m_LabelPlayer2);

            this.BackgroundImage = Properties.Resources.Beige1;

             m_BoardButtons = new Button[boardSize, boardSize];

            for (int row = 0; row < boardSize; row++)
            {
                for (int col = 0; col < boardSize; col++)
                {
                    Button i_Button = new Button
                    {
                        Size = new Size(50, 50),
                        Location = new Point(col * 50, row * 50),
                        BackColor = (row + col) % 2 == 0 ? Color.HotPink : Color.Beige,
                        FlatStyle = FlatStyle.Flat,
                        Font = new Font("Stencil", 12, FontStyle.Regular),
                        ForeColor = Color.DarkGreen,
                    };

                    if ((row + col) % 2 != 0)
                    {
                        i_Button.Click += button_Click;
                    }
                    else
                    {
                        i_Button.Enabled = false;
                    }

                    m_BoardButtons[row, col] = i_Button;
                    m_PanelBoard.Controls.Add(i_Button);
                }
            }
        }

        private void button_Click(object i_Sender, EventArgs i_EventArgs)
        {
            Button clickedButton = i_Sender as Button;
            bool v_IsCaptureMove = false;
            Button i_FirstSelectedButton = null;
            Position i_ClickedPosition= clickedPosition(clickedButton);;
           
            // if its double click on the same tool we cancel the choice 
            if (m_SelectedPosition.HasValue && m_SelectedPosition.Value.Equals(i_ClickedPosition))
            {
                m_SelectedPosition = null;
                clickedButton.BackColor = Color.Beige;
                return;
            }
 
            if (!m_SelectedPosition.HasValue)
            {
                Piece i_Piece = m_Game.Board.GetPieceAt(i_ClickedPosition);
                if (ifSelectMyPiece(i_Piece))
                {
                    m_SelectedPosition = i_ClickedPosition;
                    clickedButton.BackColor = Color.LightBlue;
                }
            }
            else
            {
                //the first button selected
                i_FirstSelectedButton = m_BoardButtons[m_SelectedPosition.Value.Row, m_SelectedPosition.Value.Col];

                Move i_Move = new Move(m_SelectedPosition.Value, i_ClickedPosition);

                if (!m_Game.Board.IsMoveValid(m_SelectedPosition.Value, i_ClickedPosition, m_Game.GetCurrentPlayer()))
                {
                    m_ShowMessages.PrintError("Invalid Move!");
                    i_FirstSelectedButton.BackColor = Color.Beige; 
                    m_SelectedPosition = null;
                    return;
                }

                v_IsCaptureMove =( Math.Abs(m_SelectedPosition.Value.Row - i_ClickedPosition.Row) == 2);

                if (m_Game.Board.MovePiece(m_SelectedPosition.Value, i_ClickedPosition, m_Game.GetCurrentPlayer(), m_Game, new List<Position>()))
                {
                    List<Position> i_NextCaptures = m_Game.Board.GetValidDestinations(i_ClickedPosition, m_Game.GetCurrentPlayer())
                        .Where(dest => Math.Abs(dest.Row - i_ClickedPosition.Row) == 2)
                        .ToList();

                    if (v_IsCaptureMove && i_NextCaptures.Any())
                    {
                        UpdateBoard();
                        m_SelectedPosition = i_ClickedPosition;
                        clickedButton.BackColor = Color.LightBlue;
                        m_ShowMessages.PrintError("You must continue capturing with the same piece.");
                        return;
                    }
                    else
                    {
                        UpdateBoard();
                        isGameOverSwitchTurn();
                    }
                }
                else
                {
                    if (m_Game.Board.HasMandatoryCapture(m_Game.GetCurrentPlayer()))
                    {
                        m_ShowMessages.PrintError("Must capture if available.");
                    }
                    else
                    {
                        m_ShowMessages.PrintError("Invalid Move!");
                    }
                    i_FirstSelectedButton.BackColor = Color.Beige; 
                    m_SelectedPosition = null;
                    return;
                }

                Player nextPlayer = m_Game.GetCurrentPlayer();
                if (!nextPlayer.IsHuman)
                {
                    Move computerMove = nextPlayer.GetMove(m_Game.Board);
                    m_Game.Board.MovePiece(computerMove.From, computerMove.To, nextPlayer, m_Game, new List<Position>());
                    UpdateBoard();
                    isGameOverSwitchTurn();
                }

                m_SelectedPosition = null;
            }
        }

        private Position clickedPosition(Button clickedButton)
        {
            int row = -1, col = -1;
            for (int i = 0; i < m_BoardButtons.GetLength(0); i++)
            {
                for (int j = 0; j < m_BoardButtons.GetLength(1); j++)
                {
                    if (m_BoardButtons[i, j] == clickedButton)
                    {
                        row = i;
                        col = j;
                        break;
                    }
                }
                if (row != -1) break;
            }

            Position i_ClickedPosition = new Position(row, col);
            return i_ClickedPosition;

        }

        private bool ifSelectMyPiece(Piece i_Piece)
        {
            return i_Piece != null && ((i_Piece.Type == m_Game.GetCurrentPlayer().PieceType) ||
                    (i_Piece.Type == ePieceType.KingO && m_Game.GetCurrentPlayer().PieceType == ePieceType.O) ||
                    (i_Piece.Type == ePieceType.KingX && m_Game.GetCurrentPlayer().PieceType == ePieceType.X));
        }

        public void isGameOverSwitchTurn()
        {
            GameOverState gameOver = new GameOverState();
            if (m_Game.IsGameOver())
            {
                m_ShowMessages.PrintWinner(m_Game, this, gameOver);
                return;
            }

            m_Game.SwitchToNextPlayer();
        }

        public void UpdateBoard()
        {
            int boardSize = m_Game.Board.Size;

            for (int row = 0; row < boardSize; row++)
            {
                for (int col = 0; col < boardSize; col++)
                {
                    Piece piece = m_Game.Board.GetPieceAt(new Position(row, col));
                    Button button = m_BoardButtons[row, col];

                    button.Text = piece != null ? piece.ToString() : "";
                    button.BackColor = (row + col) % 2 == 0 ? Color.LightCoral : Color.Beige;
                }
            }
        }

        public void UpdateScoreBoard()
        {
            m_LabelPlayer1.Text = $"Player 1 : {m_Game.Scores[0]}";
            m_LabelPlayer2.Text = $"Player 2: {m_Game.Scores[1]}";
        }
    }
}
