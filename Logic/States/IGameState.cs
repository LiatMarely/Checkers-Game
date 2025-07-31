using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckersGame
{
    public interface IGameState
    {
        void Handle(Game i_Game);
    }

}
