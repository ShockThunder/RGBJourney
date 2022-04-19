using Microsoft.Xna.Framework;
using RgbJourney.Models;
using System;
using System.Collections.Generic;
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
        public GamePlayScreen(Game game, GameStateManager stateManager) : base(game, stateManager)
        {
           
        }

        protected override void LoadContent()
        {
            base.LoadContent();
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
            _fieldManager.DrawField(_field);
            GameRef.SpriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
