using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckersGame
{
    public interface IStrategy
    {
        Move GetMove(Board i_Board, Player i_CurrentPlayer);
    }
}
