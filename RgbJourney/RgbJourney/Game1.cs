﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

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
        private UIManager _manager;
        private CustomColor _selectedColor;
        private GameStep _gameStep;
        private int[] _diceRoll = new int[2] { 0, 0 };
        private int _diceResult = 0;
        private bool _isDiceRolled = false;
        private Random _random = new Random();

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

            _gameStep = GameStep.First;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _fieldGenerator = new FieldGenerator(_cellSize, _cellSpacing, _spriteBatch, GraphicsDevice, _random);
            _field = _fieldGenerator.GenerateArray(_fieldSize);
            _player = new Player(_cellSize, _cellSpacing, _fieldSize, _spriteBatch, GraphicsDevice);
            _manager = new UIManager(_cellSize, _cellSpacing,
                GraphicsDevice.Viewport.Width,
                GraphicsDevice.Viewport.Height, _spriteBatch, GraphicsDevice, Content);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {

            var keyboardNewState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            switch (_gameStep)
            {
                case GameStep.First:
                    HandleFirstStep(keyboardNewState);
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

            _keyboardOldState = keyboardNewState;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            // TODO: Add your drawing code here
            _fieldGenerator.DrawField(_field);
            _player.Draw();
            _manager.Draw();
            if (_gameStep != GameStep.First)
            {
                _manager.DrawSelectedSquare(_selectedColor);
                _manager.DrawDiceResult(_diceRoll, _diceResult);
            }

            _spriteBatch.End();


            base.Draw(gameTime);
        }

        private void HandleFirstStep(KeyboardState keyboardNewState)
        {
            if (keyboardNewState.IsKeyDown(Keys.NumPad1) && !_keyboardOldState.IsKeyDown(Keys.NumPad1)
                || keyboardNewState.IsKeyDown(Keys.D1) && !_keyboardOldState.IsKeyDown(Keys.D1))
            {
                _selectedColor = CustomColor.Red;
                _gameStep = GameStep.Second;
            }
            if (keyboardNewState.IsKeyDown(Keys.NumPad2) && !_keyboardOldState.IsKeyDown(Keys.NumPad2)
                || keyboardNewState.IsKeyDown(Keys.D2) && !_keyboardOldState.IsKeyDown(Keys.D2))
            {
                _selectedColor = CustomColor.Blue;
                _gameStep = GameStep.Second;
            }
            if (keyboardNewState.IsKeyDown(Keys.NumPad3) && !_keyboardOldState.IsKeyDown(Keys.NumPad3)
                || keyboardNewState.IsKeyDown(Keys.D3) && !_keyboardOldState.IsKeyDown(Keys.D3))
            {
                _selectedColor = CustomColor.Green;
                _gameStep = GameStep.Second;
            }

            _diceResult = 0;
            _isDiceRolled = false;
        }

        private void HandleSecondStep(KeyboardState keyboardNewState)
        {
            if (!_isDiceRolled)
            {
                _diceRoll = RollDice();
                _isDiceRolled = true;
            }
            //RollDice            
            if (keyboardNewState.IsKeyDown(Keys.NumPad1) && !_keyboardOldState.IsKeyDown(Keys.NumPad1)
                || keyboardNewState.IsKeyDown(Keys.D1) && !_keyboardOldState.IsKeyDown(Keys.D1))
            {
                _diceResult = _diceRoll[0] + _diceRoll[1];
                _player.StepsCount = _diceResult;
                _gameStep = GameStep.Third;
            }
            if (keyboardNewState.IsKeyDown(Keys.NumPad2) && !_keyboardOldState.IsKeyDown(Keys.NumPad2)
                || keyboardNewState.IsKeyDown(Keys.D2) && !_keyboardOldState.IsKeyDown(Keys.D2))
            {
                _diceResult = Math.Abs(_diceRoll[0] - _diceRoll[1]);
                _player.StepsCount = _diceResult;
                _gameStep = GameStep.Third;
            }
        }

        private void HandleThirdStep(KeyboardState keyboardNewState)
        {
            if (_player.StepsCount < 1)
                _gameStep = GameStep.First;
            //PlayerMovement
            if (keyboardNewState.IsKeyDown(Keys.Up) && !_keyboardOldState.IsKeyDown(Keys.Up))
            {
                _player.MoveUp();
            }
            if (keyboardNewState.IsKeyDown(Keys.Down) && !_keyboardOldState.IsKeyDown(Keys.Down))
            {
                _player.MoveDown();
            }

            if (keyboardNewState.IsKeyDown(Keys.Left) && !_keyboardOldState.IsKeyDown(Keys.Left))
            {
                _player.MoveLeft();
            }
            if (keyboardNewState.IsKeyDown(Keys.Right) && !_keyboardOldState.IsKeyDown(Keys.Right))
            {
                _player.MoveRight();
            }

            //Calcualte right cell

        }

        private int[] RollDice()
        {
            var diceRoll = new int[2];
            diceRoll[0] = _random.Next(1, 7);
            diceRoll[1] = _random.Next(1, 7);

            return diceRoll;
        }
    }
}
