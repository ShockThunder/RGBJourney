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
        #endregion

        public const int ScreenWidth = 1200;
        public const int ScreenHeight = 700;

        public readonly Rectangle ScreenRectangle = new Rectangle(
            0, 0, ScreenWidth, ScreenHeight);

        private int PLAYERSTEP = 4;

        

        // Old code
        private FieldManager _fieldManager;
        private int _fieldSize = 15;
        private int _cellSize = 45;
        private int _cellSpacing = 2;
        private Player _player;
        private UIManager _uiManager;
        private ResourceManager _resourceManager;
        private CustomColor _selectedColor;
        private GameStep _gameStep;
        private GamePhase _gamePhase;
        private HighlightedCells _highlightedCells = HighlightedCells.Both;
        
        private bool _illegalTurn = false;

        private double _gameStartSeconds = 0;
        private double _fullGameTimeInSeconds = 45;

        private KeyboardState _keyboardOldState = Keyboard.GetState();

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
            _gameStateManager.ChangeState(TitleScreen);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = ScreenWidth;
            _graphics.PreferredBackBufferHeight = ScreenHeight;
            _graphics.ApplyChanges();

            _gamePhase = GamePhase.TitleScreen;

            _gameStep = GameStep.First;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            //_resourceManager = new ResourceManager(GraphicsDevice, Content);

            //ResetGameState();
            

            // TODO: use this.Content to load your game content here
        }

        //private void ResetGameState()
        //{
        //    _fieldManager = new FieldManager(_cellSize, _cellSpacing, SpriteBatch, _resourceManager, _fieldSize);

        //    //Alpha-2 refactor
        //    _fieldGenerator = new FieldGenerator(_cellSize, _cellSpacing, _fieldSize, _random);
        //    _field = _fieldGenerator.GenerateField();
        //    _gameStepManager = new GameStepManager(_random);
        //    //----

        //    _player = new Player(_cellSize, _cellSpacing, _fieldSize, SpriteBatch, _resourceManager);
        //    _fieldManager.OldPlayerPosition = new Position(_player.Position);
        //    _uiManager = new UIManager(_cellSize, _cellSpacing,
        //        GraphicsDevice.Viewport.Width,
        //        GraphicsDevice.Viewport.Height, _fieldSize, SpriteBatch, _resourceManager);
        //}

        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        private void HandleNoGameScreen(KeyboardState keyboardNewState, GameTime gameTime)
        {
            if (keyboardNewState.IsKeyDown(Keys.NumPad1) && !_keyboardOldState.IsKeyDown(Keys.NumPad1)
                || keyboardNewState.IsKeyDown(Keys.D1) && !_keyboardOldState.IsKeyDown(Keys.D1))
            {
                _fieldSize = 15;
                //ResetGameState();
                _gamePhase = GamePhase.Game;
                _gameStep = GameStep.First;
                _gameStartSeconds = gameTime.TotalGameTime.TotalSeconds;
            }
            if (keyboardNewState.IsKeyDown(Keys.NumPad2) && !_keyboardOldState.IsKeyDown(Keys.NumPad2)
                || keyboardNewState.IsKeyDown(Keys.D2) && !_keyboardOldState.IsKeyDown(Keys.D2))
            {
                _fieldSize = 9;
                //ResetGameState();
                _gamePhase = GamePhase.Game;
                _gameStep = GameStep.First;
                _gameStartSeconds = gameTime.TotalGameTime.TotalSeconds;
            }

            if (keyboardNewState.GetPressedKeyCount() > 0 && _keyboardOldState.GetPressedKeyCount() == 0)
            {

            }
        }

        private void HandleGame(KeyboardState keyboardNewState)
        {
            
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SpriteBatch.Begin();
            // TODO: Add your drawing code here
            
            //switch (_gamePhase)
            //{
            //    case GamePhase.TitleScreen:
            //        DrawTitleScreen();
            //        break;
            //    case GamePhase.Game:
            //        DrawGameScreen(gameTime);
            //        break;
            //    case GamePhase.WinScreen:
            //        DrawWinScreen();
            //        break;
            //    case GamePhase.LoseScreen:
            //        DrawLoseScreen();
            //        break;
            //    default:
            //        break;
            //}


            SpriteBatch.End();


            base.Draw(gameTime);
        }

        private void DrawLoseScreen()
        {
            DrawTitleScreen();
            _uiManager.DrawLoseText();
        }

        private void DrawWinScreen()
        {
            DrawTitleScreen();
            _uiManager.DrawWinText();
        }

        private void DrawGameScreen(GameTime gameTime)
        {
            _fieldManager.DrawField(_field);
            //_player.Draw();
            _uiManager.Draw();
            if (_gameStep != GameStep.First)
            {
                _uiManager.DrawSelectedSquare(_selectedColor);

                _fieldManager.HighlightPossibleCells(
                    _fieldManager.OldPlayerPosition.FieldX, _fieldManager.OldPlayerPosition.FieldY, PLAYERSTEP, _highlightedCells);

            }
            if (_illegalTurn)
                _uiManager.DrawIllegalTurn();

            _uiManager.DrawTimer(_gameStartSeconds, gameTime.TotalGameTime.TotalSeconds);
        }

        private void DrawTitleScreen()
        {
            _fieldManager.DrawField(_field);
            _uiManager.DrawTitleScreen();
        }
    }
}
