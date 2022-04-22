using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RgbJourney.Enums;
using RgbJourney.GameScreens;
using RgbJourney.Models;
using System;

namespace RgbJourney
{
    public class Game1 : Game
    {
        //Alpha-2 refactor
        private FieldGenerator _fieldGenerator;
        private Random _random = new Random();
        private Field _field;
        
        private GraphicsDeviceManager _graphics;
        private GameStepManager _gameStepManager;
        public SpriteBatch SpriteBatch { get; set; }

        #region Game Screens
        public TitleScreen TitleScreen { get; private set; }
        public StartMenuScreen StartMenuScreen { get; private set; }
        public GamePlayScreen GamePlayScreen { get; private set; }
        public LoseGameScreen LoseGameScreen { get; private set; }
        #endregion

        public const int ScreenWidth = 1200;
        public const int ScreenHeight = 700;

        public readonly Rectangle ScreenRectangle = new Rectangle(
            0, 0, ScreenWidth, ScreenHeight);
        

        private GameStateManager _gameStateManager;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            Components.Add(new InputHandler(this));
            _gameStateManager = new GameStateManager(this);
            Components.Add(_gameStateManager);

            TitleScreen = new TitleScreen(this, _gameStateManager);
            StartMenuScreen = new StartMenuScreen(this, _gameStateManager);
            GamePlayScreen = new GamePlayScreen(this, _gameStateManager);
            LoseGameScreen = new LoseGameScreen(this, _gameStateManager);
            _gameStateManager.ChangeState(TitleScreen);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = ScreenWidth;
            _graphics.PreferredBackBufferHeight = ScreenHeight;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
        }



        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SpriteBatch.Begin();

            SpriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
