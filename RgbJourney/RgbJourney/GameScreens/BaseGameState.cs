using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgbJourney.GameScreens
{
    public abstract partial class BaseGameState : GameState
    {
        public Game1 GameRef;

        protected BaseGameState(Game game, GameStateManager stateManager) : base(game, stateManager)
        {
            GameRef = (Game1)game;
        }
    }
}
