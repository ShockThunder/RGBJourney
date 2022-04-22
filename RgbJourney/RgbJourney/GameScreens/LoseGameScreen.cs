using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RgbJourney.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgbJourney.GameScreens
{
    public class LoseGameScreen : BaseGameState
    {
        private Texture2D backgroundImage;
        Label titleLabel;
        Label scoreText;
        Label scoreValue;
        LinkLabel startNewGame;

        public LoseGameScreen(Game game, GameStateManager stateManager) : base(game, stateManager)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            var content = Game.Content;
            backgroundImage = content.Load<Texture2D>("BackTexture");

            titleLabel = new Label
            {
                Position = new Vector2(500, 200),
                Text = "You lose the game =(",
                Color = Color.White,
                TabStop = false,
                HasFocus = false,
                Enabled = false,
                Visible = true
            };
            ControlManager.Add(titleLabel);

            scoreText = new Label
            {
                Position = new Vector2(500, 250),
                Text = "Your score",
                Color = Color.White,
                TabStop = false,
                HasFocus = false,
                Enabled = false,
                Visible = true
            };
            ControlManager.Add(scoreText);

            scoreValue = new Label
            {
                Position = new Vector2(500, 300),
                Text = "points",
                Color = Color.White,
                TabStop = false,
                HasFocus = false,
                Enabled = false,
                Visible = true
            };
            ControlManager.Add(scoreValue);

            startNewGame = new LinkLabel
            {
                Position = new Vector2(500, 350),
                Text = "Start new game.",
                Color = Color.White,
                TabStop = true,
                HasFocus = true
            };
            startNewGame.Selected += StartNewGame_Selected;
            ControlManager.Add(startNewGame);
        }

        public override void Update(GameTime gameTime)
        {
            ControlManager.Update(gameTime, PlayerIndexControl);

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

        public void SetLoseInformation(int Score)
        {
            scoreValue.Text = $"{Score} points.";
        }

        private void StartNewGame_Selected(object sender, EventArgs e)
        {
            StateManager.PopState();
            StateManager.PopState();
            StateManager.PushState(GameRef.GamePlayScreen);
            GameRef.GamePlayScreen.ResetGame();
        }
    }
}
