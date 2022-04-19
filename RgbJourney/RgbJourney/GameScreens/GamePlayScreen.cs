using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RgbJourney.Controls;
using RgbJourney.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RgbJourney.GameScreens
{
    public class GamePlayScreen : BaseGameState
    {
        private int _fieldSize = 15;
        private int _cellSize = 45;
        private int _cellSpacing = 2;

        FieldGenerator _fieldGenerator;
        FieldManager _fieldManager;
        ResourceManager _resourceManager;
        Field _field;
        Player _player;

        private PictureBox backgroundImage;
        private Texture2D playerTexture;

        public GamePlayScreen(Game game, GameStateManager stateManager) : base(game, stateManager)
        {
           
        }

        protected override void LoadContent()
        {
            var content = Game.Content;

            base.LoadContent();
            backgroundImage = new PictureBox(
                content.Load<Texture2D>("BackTexture"), GameRef.ScreenRectangle);

            ControlManager.Add(backgroundImage);

            playerTexture = content.Load<Texture2D>("Player");

            _player = new Player(_cellSize, _cellSpacing, _fieldSize, playerTexture);
            _resourceManager = new ResourceManager();
            _fieldGenerator = new FieldGenerator(_cellSize, _cellSpacing, _fieldSize, new Random());
            _field = _fieldGenerator.GenerateField();
            _fieldManager = new FieldManager(_cellSize, _cellSpacing, GameRef.SpriteBatch, _resourceManager, _fieldSize);
            _resourceManager.LoadContent(GameRef.GraphicsDevice, GameRef.Content);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef.SpriteBatch.Begin();
            base.Draw(gameTime);
            ControlManager.Draw(GameRef.SpriteBatch);
            _fieldManager.DrawField(_field);
            _player.Draw(GameRef.SpriteBatch);
            GameRef.SpriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputHandler.KeyPressed(Keys.Up))
                _player.MoveUp();
            if (InputHandler.KeyPressed(Keys.Down))
                _player.MoveDown();
            if (InputHandler.KeyPressed(Keys.Left))
                _player.MoveLeft();
            if (InputHandler.KeyPressed(Keys.Right))
                _player.MoveRight();
        }
    }
}
