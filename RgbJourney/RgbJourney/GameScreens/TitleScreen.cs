using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RgbJourney.GameScreens
{
    public class TitleScreen : BaseGameState
    {
        Texture2D backgroundImage;
        public TitleScreen(Game game, GameStateManager stateManager) : base(game, stateManager)
        {
        }

        protected override void LoadContent()
        {
            var content = GameRef.Content;
            backgroundImage = content.Load<Texture2D>(Path.Combine("Backgrounds", "TitleScreen"));
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef.SpriteBatch.Begin();
            base.Draw(gameTime);

            GameRef.SpriteBatch.Draw(
                backgroundImage,
                GameRef.ScreenRectangle,
                Color.White);
            GameRef.SpriteBatch.End();
        }
    }
}
