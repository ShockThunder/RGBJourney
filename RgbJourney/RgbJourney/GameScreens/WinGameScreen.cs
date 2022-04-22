using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RgbJourney.Controls;
using System;

namespace RgbJourney.GameScreens
{
    public class WinGameScreen : BaseGameState
    {
        private Texture2D backgroundImage;
        Label titleLabel;
        Label scoreText;
        Label scoreValue;
        LinkLabel startNewGame;

        public WinGameScreen(Game game, GameStateManager stateManager) : base(game, stateManager)
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
                Text = "You win the game!",
                Color = Color.White,
                TabStop = false,
                HasFocus = false,
                Enabled = false,
                Visible = true
            };
            ControlManager.Add(titleLabel);

            scoreText = new Label
            {
                Position = new Vector2(550, 250),
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
                Position = new Vector2(560, 300),
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

        public void SetWinInformation(int Score)
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
