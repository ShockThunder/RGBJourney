using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RgbJourney.Controls;
using RgbJourney.Enums;
using RgbJourney.Models;
using System;
using System.Linq;

namespace RgbJourney.GameScreens
{
    public class GamePlayScreen : BaseGameState
    {
        private int _fieldSize = 15;
        private int _cellSize = 45;
        private int _cellSpacing = 2;
        private CustomColor _selectedColor;
        private CustomColor _currentColor;
        private GameStep _gameStep;
        private GameStepManager _gameStepManager = new GameStepManager(new Random());
        private UIManager _uiManager;


        Field _field;
        Player _player;

        private Texture2D backgroundImage;
        private Texture2D playerTexture;

        private PictureBox redTargetColor { get; set; }
        private PictureBox blueTargetColor { get; set; }
        private PictureBox greenTargetColor { get; set; }

        public int Score { get; set; }

        public GamePlayScreen(Game game, GameStateManager stateManager) : base(game, stateManager)
        {

        }

        protected override void LoadContent()
        {
            var content = Game.Content;

            base.LoadContent();

            Score = 0;
            backgroundImage = content.Load<Texture2D>("BackTexture");

            playerTexture = content.Load<Texture2D>("Player");

            _player = new Player(_cellSize, _cellSpacing, _fieldSize, playerTexture);
            _field = new Field(_cellSize, _cellSpacing, _fieldSize, GameRef.Content);
            _uiManager = new UIManager(GameRef);
            _gameStep = GameStep.First;

            LoadTargetColorUI(content);
        }



        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef.SpriteBatch.Begin();
            base.Draw(gameTime);

            GameRef.SpriteBatch.Draw(
               backgroundImage,
               GameRef.ScreenRectangle,
               Color.White);

            _field.Draw(GameRef.SpriteBatch);
            _player.Draw(GameRef.SpriteBatch);
            ControlManager.Draw(GameRef.SpriteBatch);
            _uiManager.Draw(GameRef.SpriteBatch);
            GameRef.SpriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _player.Update(gameTime);
            _uiManager.Update(gameTime, _player, Score);
            HandleGame();
        }

        private void HandleGame()
        {
            switch (_gameStep)
            {
                case GameStep.First:
                    HandleFirstStep();
                    break;
                case GameStep.Second:
                    HandleSecondStep();
                    break;
                case GameStep.Third:
                    HandleThirdStep();
                    break;
                case GameStep.Fourth:
                    HandleFourthStep();
                    break;
                default:
                    break;
            }
        }

        private void HandleFirstStep()
        {
            _selectedColor = _gameStepManager.GenerateTargetColor();
            switch (_selectedColor)
            {
                case CustomColor.Red:
                    {
                        redTargetColor.Visible = true;
                        blueTargetColor.Visible = false;
                        greenTargetColor.Visible = false;
                    }
                    break;
                case CustomColor.Blue:
                    {
                        redTargetColor.Visible = false;
                        blueTargetColor.Visible = true;
                        greenTargetColor.Visible = false;
                    }
                    break;
                case CustomColor.Green:
                    {
                        redTargetColor.Visible = false;
                        blueTargetColor.Visible = false;
                        greenTargetColor.Visible = true;
                    }
                    break;
                default:
                    break;
            }
            _gameStep = GameStep.Second;
        }

        private void HandleSecondStep()
        {
            if (_player.Character.CurrentStamina == 0)
            {
                var playerCell = _field.Cells.First(x =>
                x.Position.FieldX == _player.Position.FieldX
                && x.Position.FieldY == _player.Position.FieldY);
                _currentColor = playerCell.Color;
                _gameStep = GameStep.Third;
            }

        }

        private void HandleThirdStep()
        {
            HandleInput();
            if (_player.Character.CurrentStamina == _player.Character.MaxStamina)
                _gameStep = GameStep.Fourth;
        }

        private void HandleFourthStep()
        {
            if (Score < 0)
                LoseGame();

            _gameStep = GameStep.First;
        }

        private void LoadTargetColorUI(ContentManager content)
        {
            var targetPosition = new Vector2(800, 100);

            redTargetColor = new PictureBox(content.Load<Texture2D>("Red2"), new Rectangle(0, 0, _cellSize, _cellSize), new Rectangle(0, 0, _cellSize, _cellSize))
            {
                Enabled = false,
                Visible = false,
                HasFocus = false,
                TabStop = false
            };
            redTargetColor.SetPosition(targetPosition);
            ControlManager.Add(redTargetColor);

            blueTargetColor = new PictureBox(content.Load<Texture2D>("Blue2"), new Rectangle(0, 0, _cellSize, _cellSize), new Rectangle(0, 0, _cellSize, _cellSize))
            {
                Enabled = false,
                Visible = false,
                HasFocus = false,
                TabStop = false,
            };
            blueTargetColor.SetPosition(targetPosition);
            ControlManager.Add(blueTargetColor);

            greenTargetColor = new PictureBox(content.Load<Texture2D>("Green2"), new Rectangle(0, 0, _cellSize, _cellSize), new Rectangle(0, 0, _cellSize, _cellSize))
            {
                Enabled = false,
                Visible = false,
                HasFocus = false,
                TabStop = false,
            };
            greenTargetColor.SetPosition(targetPosition);
            ControlManager.Add(greenTargetColor);
        }

        private void HandleInput()
        {
            if (InputHandler.KeyPressed(Keys.O))
                OpenCell();
        }

        private void OpenCell()
        {
            switch (_currentColor)
            {
                case CustomColor.Red:
                    {
                        if (_player.Character.RedKeys > 0)
                            _player.Character.RedKeys--;
                        else
                            Score -= 200;

                    }
                    break;
                case CustomColor.Blue:
                    {
                        if (_player.Character.BlueKeys > 0)
                            _player.Character.BlueKeys--;
                        else
                            Score -= 200;
                    }
                    break;
                case CustomColor.Green:
                    {
                        if (_player.Character.GreenKeys > 0)
                            _player.Character.GreenKeys--;
                        else
                            Score -= 200;
                    }
                    break;
                case CustomColor.White:
                    {
                        WinGame();
                    }
                    break;
                default:
                    break;
            }

            if (_currentColor == _selectedColor)
                Score += 100;

            _player.RefreshStamina();
        }

        private void LoseGame()
        {
            StateManager.PushState(GameRef.LoseGameScreen);
            GameRef.LoseGameScreen.SetLoseInformation(Score);
        }

        private void WinGame()
        {
            StateManager.PushState(GameRef.WinGameScreen);
            GameRef.WinGameScreen.SetWinInformation(Score);
        }

        public void ResetGame()
        {
            LoadContent();
        }
    }
}
