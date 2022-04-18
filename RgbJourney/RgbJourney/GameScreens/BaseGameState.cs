using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RgbJourney.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RgbJourney.GameScreens
{
    public abstract partial class BaseGameState : GameState
    {
        protected Game1 GameRef;
        protected ControlManager ControlManager;
        protected PlayerIndex PlayerIndexControl;

        protected BaseGameState(Game game, GameStateManager stateManager) : base(game, stateManager)
        {
            GameRef = (Game1)game;
            PlayerIndexControl = PlayerIndex.One;
        }

        protected override void LoadContent()
        {
            var content = Game.Content;
            var menuFont = content.Load<SpriteFont>(Path.Combine("Fonts", "ControlFont"));
            ControlManager = new ControlManager(menuFont);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
