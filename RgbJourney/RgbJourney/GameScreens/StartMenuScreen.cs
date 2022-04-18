using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgbJourney.GameScreens
{
    public class StartMenuScreen : BaseGameState
    {
        public StartMenuScreen(Game game, GameStateManager stateManager) : base(game, stateManager)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (InputHandler.KeyReleased(Keys.Escape))
                Game.Exit();

            base.Draw(gameTime);
        }
    }
}
