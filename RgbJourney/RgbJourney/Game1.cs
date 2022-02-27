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

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _fieldGenerator = new FieldGenerator();
            _field = _fieldGenerator.GenerateArray(_fieldSize);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            var spriteBatch = new SpriteBatch(GraphicsDevice);


            var redTexture = new Texture2D(GraphicsDevice, 1, 1);
            redTexture.SetData(new Color[] { Color.Red });

            var blueTexture = new Texture2D(GraphicsDevice, 1, 1);
            blueTexture.SetData(new Color[] { Color.Blue });

            var greenTexture = new Texture2D(GraphicsDevice, 1, 1);
            greenTexture.SetData(new Color[] { Color.Green });

            var defaultRect = new Rectangle(0, 0, 20, 20);


            spriteBatch.Begin();
            // TODO: Add your drawing code here
            for (int i = 0; i < _fieldSize; i++)
            {
                for (int j = 0; j < _fieldSize; j++)
                {
                    var rect = defaultRect;
                    rect.X = i * 22;
                    rect.Y = j * 22;
                    switch (_field[i, j])
                    {
                        case 0:
                            spriteBatch.Draw(redTexture, rect, Color.Red);
                            break;
                        case 1:
                            spriteBatch.Draw(blueTexture, rect, Color.Blue);
                            break;
                        case 2:
                            spriteBatch.Draw(greenTexture, rect, Color.Green);
                            break;
                    }

                }
            }
            spriteBatch.End();



            base.Draw(gameTime);
        }
    }
}
