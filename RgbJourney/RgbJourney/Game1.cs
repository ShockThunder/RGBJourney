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
        private int _cellSize = 20;
        private int _cellSpacing = 2;
        private Player _player;

        private KeyboardState _keyboardOldState = Keyboard.GetState();

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
            _fieldGenerator = new FieldGenerator(_cellSize, _cellSpacing);
            _field = _fieldGenerator.GenerateArray(_fieldSize);
            _player = new Player(_cellSize, _cellSpacing, _fieldSize, _spriteBatch, GraphicsDevice);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            var keyboardNewState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (keyboardNewState.IsKeyDown(Keys.Up) && !_keyboardOldState.IsKeyDown(Keys.Up))
                _player.MoveUp();
            if (keyboardNewState.IsKeyDown(Keys.Down) && !_keyboardOldState.IsKeyDown(Keys.Down))
                _player.MoveDown();
            if (keyboardNewState.IsKeyDown(Keys.Left) && !_keyboardOldState.IsKeyDown(Keys.Left))
                _player.MoveLeft();
            if (keyboardNewState.IsKeyDown(Keys.Right) && !_keyboardOldState.IsKeyDown(Keys.Right))
                _player.MoveRight();

            // TODO: Add your update logic here

            _keyboardOldState = keyboardNewState;
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
