using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RgbJourney
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private FieldGenerator _fieldGenerator;
        private int[,] _field;
        private int _fieldSize = 15;
        private Player _player;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here



            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _fieldGenerator = new FieldGenerator();
            _field = _fieldGenerator.GenerateArray(_fieldSize);
            _player = new Player(20, 2, _fieldSize, _spriteBatch, GraphicsDevice);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                _player.MoveUp();
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                _player.MoveDown();
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                _player.MoveLeft();
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                _player.MoveRight();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);


            _spriteBatch.Begin();
            // TODO: Add your drawing code here
            _fieldGenerator.DrawField(_spriteBatch, GraphicsDevice, _field);
            _player.Draw();
            _spriteBatch.End();



            base.Draw(gameTime);
        }
    }
}
