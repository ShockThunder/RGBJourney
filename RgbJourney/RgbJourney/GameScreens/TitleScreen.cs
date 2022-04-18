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
    public class TitleScreen : BaseGameState
    {
        Texture2D backgroundImage;
        LinkLabel startLabel;

        public TitleScreen(Game game, GameStateManager stateManager) : base(game, stateManager)
        {
        }

        protected override void LoadContent()
        {
            var content = GameRef.Content;
            backgroundImage = content.Load<Texture2D>(Path.Combine("Backgrounds", "TitleScreen"));
            base.LoadContent();

            startLabel = new LinkLabel();
            startLabel.Position = new Vector2(500, 500);
            startLabel.Text = "Press ENTER to begin";
            startLabel.Color = Color.White;
            startLabel.TabStop = true;
            startLabel.HasFocus = true;
            startLabel.Selected += new EventHandler(StartLabel_Selected);

            ControlManager.Add(startLabel);
        }

        public override void Update(GameTime gameTime)
        {
            ControlManager.Update(gameTime, PlayerIndex.One);
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

            ControlManager.Draw(GameRef.SpriteBatch);

            GameRef.SpriteBatch.End();
        }

        private void StartLabel_Selected(object sender, EventArgs e)
        {
            StateManager.PushState(GameRef.StartMenuScreen);
        }
    }
}
