﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RgbJourney.Enums;
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
        private GameStepManager _gameStepManager;
        private int PLAYERSTEP = 4;

        // Old code
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
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

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.PreferredBackBufferHeight = 700;
            _graphics.ApplyChanges();

            _gamePhase = GamePhase.TitleScreen;

            _gameStep = GameStep.First;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _resourceManager = new ResourceManager(GraphicsDevice, Content);

            ResetGameState();
            

            // TODO: use this.Content to load your game content here
        }

        private void ResetGameState()
        {
            _fieldManager = new FieldManager(_cellSize, _cellSpacing, _spriteBatch, _resourceManager, _fieldSize);

            //Alpha-2 refactor
            _fieldGenerator = new FieldGenerator(_cellSize, _cellSpacing, _fieldSize, _random);
            _field = _fieldGenerator.GenerateField();
            _gameStepManager = new GameStepManager(_random);
            //----

            _player = new Player(_cellSize, _cellSpacing, _fieldSize, _spriteBatch, _resourceManager);
            _fieldManager.OldPlayerPosition = new Position(_player.Position);
            _uiManager = new UIManager(_cellSize, _cellSpacing,
                GraphicsDevice.Viewport.Width,
                GraphicsDevice.Viewport.Height, _fieldSize, _spriteBatch, _resourceManager);
        }

        protected override void Update(GameTime gameTime)
        {
            var keyboardNewState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (gameTime.TotalGameTime.TotalSeconds - _gameStartSeconds >= _fullGameTimeInSeconds)
                _gamePhase = GamePhase.LoseScreen;

            switch (_gamePhase)
            {
                case GamePhase.TitleScreen:
                    HandleNoGameScreen(keyboardNewState, gameTime);
                    break;
                case GamePhase.WinScreen:
                    HandleNoGameScreen(keyboardNewState, gameTime);
                    break;
                case GamePhase.LoseScreen:
                    HandleNoGameScreen(keyboardNewState, gameTime);
                    break;
                case GamePhase.Game:
                    HandleGame(keyboardNewState);
                    break;
            }

            _keyboardOldState = keyboardNewState;

            base.Update(gameTime);
        }

        private void HandleNoGameScreen(KeyboardState keyboardNewState, GameTime gameTime)
        {
            if (keyboardNewState.IsKeyDown(Keys.NumPad1) && !_keyboardOldState.IsKeyDown(Keys.NumPad1)
                || keyboardNewState.IsKeyDown(Keys.D1) && !_keyboardOldState.IsKeyDown(Keys.D1))
            {
                _fieldSize = 15;
                ResetGameState();
                _gamePhase = GamePhase.Game;
                _gameStep = GameStep.First;
                _gameStartSeconds = gameTime.TotalGameTime.TotalSeconds;
            }
            if (keyboardNewState.IsKeyDown(Keys.NumPad2) && !_keyboardOldState.IsKeyDown(Keys.NumPad2)
                || keyboardNewState.IsKeyDown(Keys.D2) && !_keyboardOldState.IsKeyDown(Keys.D2))
            {
                _fieldSize = 9;
                ResetGameState();
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
            switch (_gameStep)
            {
                case GameStep.First:
                    HandleFirstStep();
                    break;
                case GameStep.Second:
                    HandleSecondStep(keyboardNewState);
                    break;
                case GameStep.Third:
                    HandleThirdStep(keyboardNewState);
                    break;
                case GameStep.Fourth:
                    break;
                default:
                    break;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            // TODO: Add your drawing code here

            switch (_gamePhase)
            {
                case GamePhase.TitleScreen:
                    DrawTitleScreen();
                    break;
                case GamePhase.Game:
                    DrawGameScreen(gameTime);
                    break;
                case GamePhase.WinScreen:
                    DrawWinScreen();
                    break;
                case GamePhase.LoseScreen:
                    DrawLoseScreen();
                    break;
                default:
                    break;
            }


            _spriteBatch.End();


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
            _player.Draw();
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

        private void HandleFirstStep()
        {
            _selectedColor = _gameStepManager.GenerateTargetColor();
            _gameStep = GameStep.Second;

            
            _player.Direction = MovementDirection.NotSet;
            _highlightedCells = HighlightedCells.Both;
        }

        private void HandleSecondStep(KeyboardState keyboardNewState)
        {
            
            //RollDice            
            if (keyboardNewState.IsKeyDown(Keys.NumPad1) && !_keyboardOldState.IsKeyDown(Keys.NumPad1)
                || keyboardNewState.IsKeyDown(Keys.D1) && !_keyboardOldState.IsKeyDown(Keys.D1))
            {

                _player.StepsCount = PLAYERSTEP;
                _fieldManager.OldPlayerPosition = new Position(_player.Position);
                _gameStep = GameStep.Third;
                _highlightedCells = HighlightedCells.Sum;
            }
            if (keyboardNewState.IsKeyDown(Keys.NumPad2) && !_keyboardOldState.IsKeyDown(Keys.NumPad2)
                || keyboardNewState.IsKeyDown(Keys.D2) && !_keyboardOldState.IsKeyDown(Keys.D2))
            {
                _player.StepsCount = PLAYERSTEP;
                _fieldManager.OldPlayerPosition = new Position(_player.Position);
                _gameStep = GameStep.Third;
                _highlightedCells = HighlightedCells.Sub;
            }
        }

        private void HandleThirdStep(KeyboardState keyboardNewState)
        {
            if (_fieldManager.CheckWinCondition(_player.Position))
                _gamePhase = GamePhase.WinScreen;
            else
            {
                if (_player.StepsCount < 1)
                {
                    var canEndTurn = _fieldManager.CanEndTurn(_player.Position, _selectedColor);
                    if (canEndTurn)
                    {
                        _fieldManager.OldPlayerPosition = new Position(_player.Position);
                        _gameStep = GameStep.First;
                    }
                    else
                    {
                        _player.Position = new Position(_fieldManager.OldPlayerPosition);
                        _player.StepsCount = PLAYERSTEP;
                        _illegalTurn = true;
                    }
                }


                //PlayerMovement
                // TODO refactor movement. Need to extract common parts
                if (keyboardNewState.IsKeyDown(Keys.Up) && !_keyboardOldState.IsKeyDown(Keys.Up))
                {
                    if (_illegalTurn)
                    {
                        _illegalTurn = false;
                        _player.Direction = MovementDirection.NotSet;
                    }

                    if (_player.Direction == MovementDirection.NotSet)
                        _player.Direction = MovementDirection.Up;

                    _player.Move();
                }
                if (keyboardNewState.IsKeyDown(Keys.Down) && !_keyboardOldState.IsKeyDown(Keys.Down))
                {
                    if (_illegalTurn)
                    {
                        _illegalTurn = false;
                        _player.Direction = MovementDirection.NotSet;
                    }

                    if (_player.Direction == MovementDirection.NotSet)
                        _player.Direction = MovementDirection.Down;

                    _player.Move();
                }

                if (keyboardNewState.IsKeyDown(Keys.Left) && !_keyboardOldState.IsKeyDown(Keys.Left))
                {
                    if (_illegalTurn)
                    {
                        _illegalTurn = false;
                        _player.Direction = MovementDirection.NotSet;
                    }

                    if (_player.Direction == MovementDirection.NotSet)
                        _player.Direction = MovementDirection.Left;

                    _player.Move();
                }
                if (keyboardNewState.IsKeyDown(Keys.Right) && !_keyboardOldState.IsKeyDown(Keys.Right))
                {
                    if (_illegalTurn)
                    {
                        _illegalTurn = false;
                        _player.Direction = MovementDirection.NotSet;
                    }

                    if (_player.Direction == MovementDirection.NotSet)
                        _player.Direction = MovementDirection.Right;

                    _player.Move();
                }
            }
            //Calcualte right cell

        }
    }
}
