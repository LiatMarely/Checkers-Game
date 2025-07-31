using CheckersGame;
using Ex05_Liat_207918608.FormUI;
using System;

public class GameOverState : IGameState
{
    private Player m_Winner;
    ShowMessages m_ShowMessages = new ShowMessages();

    public GameOverState()
    {
        m_Winner = null;
    }

    public GameOverState(Player i_Winner)
    {
        m_Winner = i_Winner;
    }

    public void Handle(Game i_Game)
    {
        //i_Game.View.PrintMessage("Game Over!");
        Player winner = i_Game.GetWinner();

        determineWinner(winner, i_Game);

        printCurrentScores(i_Game);

        //i_Game.View.PrintMessage("Would you like to play another round? (Y/N)");
        string input = Console.ReadLine()?.Trim().ToUpper();

        if (input == "Y")
        {
            i_Game.ResetGame();
            i_Game.SetState(new PlayerTurnState());
        }
        else
        {
            //i_Game.View.PrintMessage("Thanks for playing!");
            Environment.Exit(0);
        }
    }

    public void determineWinner(Player winner, Game i_Game)
    {
        if (m_Winner != null || winner != null)
        {
            if (m_Winner == null)
            {
                m_Winner = winner;
            }

            //i_Game.View.PrintMessage($"Winner: {m_Winner.Name}");
            Player loser = i_Game.GetLoser(m_Winner);
            int scoreDifference = Math.Abs(i_Game.CountPieces(m_Winner) - i_Game.CountPieces(loser));
            int winnerIndex = Array.IndexOf(i_Game.Players, m_Winner);
            i_Game.Scores[winnerIndex] += scoreDifference;
        }
        else
        {
            //i_Game.View.PrintMessage("It's a draw!");
        }
    }

    private void printCurrentScores(Game i_Game)
    {
        //i_Game.View.PrintMessage("Current Scores:");
        for (int i = 0; i < i_Game.Scores.Length; i++)
        {
            //i_Game.View.PrintMessage($"{i_Game.Players[i].Name}: {i_Game.Scores[i]}");
        }
    }
}
