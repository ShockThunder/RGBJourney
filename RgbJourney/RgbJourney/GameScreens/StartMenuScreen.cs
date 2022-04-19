using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RgbJourney.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RgbJourney.GameScreens
{
    public class StartMenuScreen : BaseGameState
    {
        PictureBox backgroundImage;
        PictureBox arrowImage;
        LinkLabel startGame;
        LinkLabel loadGame;
        LinkLabel exitGame;
        float maxItemWidth = 0f;

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

            var content = Game.Content;

            backgroundImage = new PictureBox(
                content.Load<Texture2D>(Path.Combine("Backgrounds", "TitleScreen")), GameRef.ScreenRectangle);

            ControlManager.Add(backgroundImage);

            var arrowTexture = content.Load<Texture2D>(Path.Combine("GUI", "leftarrowUp"));
            arrowImage = new PictureBox(
                arrowTexture, new Rectangle(0, 0, arrowTexture.Width, arrowTexture.Height));
            ControlManager.Add(arrowImage);

            startGame = new LinkLabel();
            startGame.Text = "Start the journey";
            startGame.Size = startGame.SpriteFont.MeasureString(startGame.Text);
            startGame.Selected += new EventHandler(MenuItem_Selected);
            ControlManager.Add(startGame);

            loadGame = new LinkLabel();
            loadGame.Text = "Load the journey";
            loadGame.Size = loadGame.SpriteFont.MeasureString(loadGame.Text);
            loadGame.Selected += new EventHandler(MenuItem_Selected);
            ControlManager.Add(loadGame);

            exitGame = new LinkLabel();
            exitGame.Text = "Exit the journey";
            exitGame.Size = exitGame.SpriteFont.MeasureString(exitGame.Text);
            exitGame.Selected += new EventHandler(MenuItem_Selected);
            ControlManager.Add(exitGame);

            ControlManager.NextControl();
            ControlManager.FocusChanged += new EventHandler(ControlManager_FocusChanged);

            var position = new Vector2(500, 500);

            foreach (var control in ControlManager)
            {
                if (control is LinkLabel)
                {
                    if (control.Size.X > maxItemWidth)
                        maxItemWidth = control.Size.X;

                    control.Position = position;
                    position.Y += control.Size.Y + 5f;
                }
            }

            ControlManager_FocusChanged(startGame, EventArgs.Empty);
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
            ControlManager.Draw(GameRef.SpriteBatch);
            GameRef.SpriteBatch.End();
        }

        private void ControlManager_FocusChanged(object sender, EventArgs e)
        {
            var control = sender as Control;
            var position = new Vector2(control.Position.X + maxItemWidth + 10f, control.Position.Y);

            arrowImage.SetPosition(position);
        }

        private void MenuItem_Selected(object sender, EventArgs e)
        {
            if (sender == startGame)
                StateManager.PushState(GameRef.GamePlayScreen);

            if (sender == loadGame)
                StateManager.PushState(GameRef.GamePlayScreen);

            if (sender == exitGame)
                GameRef.Exit();
        }
    }
}
