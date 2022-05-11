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
        private const int FIELD_SIZE = 15;
        private const int CELL_SIZE = 45;
        private const int CELL_SPACING = 2;
        private CustomColor _selectedColor;
        private CustomColor _currentCellColor;
        private CustomColor _nextColor;
        private GameStep _gameStep;
        private readonly Random _random;
        private readonly GameStepManager _gameStepManager;
        private UIManager _uiManager;


        private Field _field;
        private Player _player;

        private Texture2D _backgroundImage;
        private Texture2D _playerTexture;

        private PictureBox RedTargetColor { get; set; }
        private PictureBox BlueTargetColor { get; set; }
        private PictureBox GreenTargetColor { get; set; }
        
        private PictureBox RedNextTargetColor { get; set; }
        private PictureBox BlueNextTargetColor { get; set; }
        private PictureBox GreenNextTargetColor { get; set; }

        public int Score { get; set; }

        public GamePlayScreen(Game game, GameStateManager stateManager) : base(game, stateManager)
        {
            _random = new Random();
            _gameStepManager = new GameStepManager(_random);
        }

        protected override void LoadContent()
        {
            var content = Game.Content;

            base.LoadContent();

            Score = 0;
            _backgroundImage = content.Load<Texture2D>("BackTexture");

            _playerTexture = content.Load<Texture2D>("Player");

            _player = new Player(CELL_SIZE, CELL_SPACING, FIELD_SIZE, _playerTexture);
            _field = new Field(CELL_SIZE, CELL_SPACING, FIELD_SIZE, GameRef.Content);
            _uiManager = new UIManager(GameRef);
            _gameStep = GameStep.First;

            LoadTargetColorUI(content);

            _nextColor = _gameStepManager.GenerateTargetColor();
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
               _backgroundImage,
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
            if(_player.Character.CurrentHealth <= 0)
                LoseGame();
            
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
            _selectedColor = _nextColor;
            _nextColor = _gameStepManager.GenerateTargetColor();
            switch (_selectedColor)
            {
                case CustomColor.Red:
                    {
                        RedTargetColor.Visible = true;
                        BlueTargetColor.Visible = false;
                        GreenTargetColor.Visible = false;
                    }
                    break;
                case CustomColor.Blue:
                    {
                        RedTargetColor.Visible = false;
                        BlueTargetColor.Visible = true;
                        GreenTargetColor.Visible = false;
                    }
                    break;
                case CustomColor.Green:
                    {
                        RedTargetColor.Visible = false;
                        BlueTargetColor.Visible = false;
                        GreenTargetColor.Visible = true;
                    }
                    break;
                default:
                    break;
            }
            
            switch (_nextColor)
            {
                case CustomColor.Red:
                {
                    RedNextTargetColor.Visible = true;
                    BlueNextTargetColor.Visible = false;
                    GreenNextTargetColor.Visible = false;
                }
                    break;
                case CustomColor.Blue:
                {
                    RedNextTargetColor.Visible = false;
                    BlueNextTargetColor.Visible = true;
                    GreenNextTargetColor.Visible = false;
                }
                    break;
                case CustomColor.Green:
                {
                    RedNextTargetColor.Visible = false;
                    BlueNextTargetColor.Visible = false;
                    GreenNextTargetColor.Visible = true;
                }
                    break;
                default:
                    break;
            }
            _gameStep = GameStep.Second;
        }

        private void HandleSecondStep()
        {
            //if (_player.Character.CurrentStamina == 0)
                _gameStep = GameStep.Third;

        }

        private void HandleThirdStep()
        {
            HandleInput();
            //Step moving in OpenCell
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
            var nextTargetPosition = new Vector2(800 + CELL_SIZE + CELL_SPACING, 100);

            RedTargetColor = new PictureBox(content.Load<Texture2D>("Red2"), new Rectangle(0, 0, CELL_SIZE, CELL_SIZE), new Rectangle(0, 0, CELL_SIZE, CELL_SIZE))
            {
                Enabled = false,
                Visible = false,
                HasFocus = false,
                TabStop = false
            };
            RedTargetColor.SetPosition(targetPosition);
            ControlManager.Add(RedTargetColor);

            BlueTargetColor = new PictureBox(content.Load<Texture2D>("Blue2"), new Rectangle(0, 0, CELL_SIZE, CELL_SIZE), new Rectangle(0, 0, CELL_SIZE, CELL_SIZE))
            {
                Enabled = false,
                Visible = false,
                HasFocus = false,
                TabStop = false,
            };
            BlueTargetColor.SetPosition(targetPosition);
            ControlManager.Add(BlueTargetColor);

            GreenTargetColor = new PictureBox(content.Load<Texture2D>("Green2"), new Rectangle(0, 0, CELL_SIZE, CELL_SIZE), new Rectangle(0, 0, CELL_SIZE, CELL_SIZE))
            {
                Enabled = false,
                Visible = false,
                HasFocus = false,
                TabStop = false,
            };
            GreenTargetColor.SetPosition(targetPosition);
            ControlManager.Add(GreenTargetColor);
            
            RedNextTargetColor = new PictureBox(content.Load<Texture2D>("Red2"), new Rectangle(0, 0, CELL_SIZE, CELL_SIZE), new Rectangle(0, 0, CELL_SIZE, CELL_SIZE))
            {
                Enabled = false,
                Visible = false,
                HasFocus = false,
                TabStop = false
            };
            RedNextTargetColor.SetPosition(nextTargetPosition);
            ControlManager.Add(RedNextTargetColor);

            BlueNextTargetColor = new PictureBox(content.Load<Texture2D>("Blue2"), new Rectangle(0, 0, CELL_SIZE, CELL_SIZE), new Rectangle(0, 0, CELL_SIZE, CELL_SIZE))
            {
                Enabled = false,
                Visible = false,
                HasFocus = false,
                TabStop = false,
            };
            BlueNextTargetColor.SetPosition(nextTargetPosition);
            ControlManager.Add(BlueNextTargetColor);

            GreenNextTargetColor = new PictureBox(content.Load<Texture2D>("Green2"), new Rectangle(0, 0, CELL_SIZE, CELL_SIZE), new Rectangle(0, 0, CELL_SIZE, CELL_SIZE))
            {
                Enabled = false,
                Visible = false,
                HasFocus = false,
                TabStop = false,
            };
            GreenNextTargetColor.SetPosition(nextTargetPosition);
            ControlManager.Add(GreenNextTargetColor);

        }

        private void HandleInput()
        {
            if (InputHandler.KeyPressed(Keys.O))
                OpenCell();
        }

        private void OpenCell()
        {
            var playerCell = _field.Cells.First(x =>
                x.Position.FieldX == _player.Position.FieldX
                && x.Position.FieldY == _player.Position.FieldY);

            _currentCellColor = playerCell.Color;

            switch (_currentCellColor)
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

            if (_currentCellColor == _selectedColor)
                Score += 100;
            else
            {
                HandleNonTargetColor();
            }

            if (!playerCell.IsOpened && playerCell.CellType == CellType.Treasure)
                Score += 300;

            playerCell.IsOpened = true;
            _player.RefreshStamina();

            _gameStep = GameStep.Fourth;
        }

        private void HandleNonTargetColor()
        {
            var isDebuff = _random.Next(5) == 2;
            if(!isDebuff)
                return;
            
            var debuffType = (Debuff)_random.Next(2);
            switch (debuffType)
            {
                case Debuff.Damage:
                    _player.Character.CurrentHealth--;
                    break;
                case Debuff.Exhaust:
                    _player.Character.CurrentStamina--;
                    break;
                case Debuff.Weakness:
                    Score -= 100;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
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
