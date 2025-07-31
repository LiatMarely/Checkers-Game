using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CheckersGame;

namespace Ex05_Liat_207918608.FormUI
{
    internal class ShowMessages
    {
        public void PrintWinner(Game game, FormBoardGame i_formGame, GameOverState gameOver)
        {
            Console.WriteLine("print Winner");
            Player i_Winner = game.GetWinner();
            DialogResult M_EndingMessage;
            if (!(i_Winner == null))
            {
                Console.WriteLine("winner");
                M_EndingMessage = MessageBox.Show($"{i_Winner.Name} Won!\nAnother Round?", "Damka", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
            else
            {
                Console.WriteLine("else draw");
                M_EndingMessage = MessageBox.Show($"It's a draw! \nAnother Round?", "Damka", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
            if (M_EndingMessage == DialogResult.Yes)
            {

                Console.WriteLine("Dialog yes");
                Console.WriteLine(string.Join(", ", game.Scores));
                gameOver.determineWinner(i_Winner, game);
                game.ResetGame();
                Console.WriteLine(string.Join(", ", game.Scores));
                i_formGame.UpdateBoard();
                Console.WriteLine(string.Join(", ", game.Scores));

                i_formGame.UpdateScoreBoard();

            }
            else
            {
                Console.WriteLine("Dialog esle");
                i_formGame.Close();
            }
        }

        public void PrintError(string i_ErrorMessage)
        {
            MessageBox.Show(i_ErrorMessage, "Damka", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
    }
}
