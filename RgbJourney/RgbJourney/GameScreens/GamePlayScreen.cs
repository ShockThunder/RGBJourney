using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RgbJourney.Controls;
using RgbJourney.Models;

namespace RgbJourney.GameScreens
{
    public class GamePlayScreen : BaseGameState
    {
        private int _fieldSize = 15;
        private int _cellSize = 45;
        private int _cellSpacing = 2;

        Field _field;
        Player _player;

        private Texture2D backgroundImage;
        private Texture2D playerTexture;

        public GamePlayScreen(Game game, GameStateManager stateManager) : base(game, stateManager)
        {
           
        }

        protected override void LoadContent()
        {
            var content = Game.Content;
            
            base.LoadContent();
            
            backgroundImage = content.Load<Texture2D>("BackTexture");           

            playerTexture = content.Load<Texture2D>("Player");

            _player = new Player(_cellSize, _cellSpacing, _fieldSize, playerTexture);
            _field = new Field(_cellSize, _cellSpacing, _fieldSize, GameRef.Content);
            
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

            GameRef.SpriteBatch.Draw(
               backgroundImage,
               GameRef.ScreenRectangle,
               Color.White);

            _field.Draw(GameRef.SpriteBatch);
            _player.Draw(GameRef.SpriteBatch);

            GameRef.SpriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _player.Update(gameTime);
        }
    }
}
