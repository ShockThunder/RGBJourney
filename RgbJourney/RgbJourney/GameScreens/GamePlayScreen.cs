using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RgbJourney.Controls;
using RgbJourney.Enums;
using RgbJourney.Models;
using System;
using System.Linq;

namespace RgbJourney.GameScreens
{
    public class GamePlayScreen : BaseGameState
    {
        public int Score { get; set; } = 0;

        private int _fieldSize = 15;
        private int _cellSize = 45;
        private int _cellSpacing = 2;
        private CustomColor _selectedColor;
        private GameStep _gameStep;
        private GameStepManager _gameStepManager = new GameStepManager(new Random());

        private Label _targetColorLabel;

        Field _field;
        Player _player;

        private Texture2D backgroundImage;
        private Texture2D playerTexture;

        private PictureBox redTargetColor { get; set; }
        private PictureBox blueTargetColor { get; set; }
        private PictureBox greenTargetColor { get; set; }


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
            _gameStep = GameStep.First;

            var targetPosition = new Vector2(800, 100);
            var labelPosition = new Vector2(800, 70);

            redTargetColor = new PictureBox(content.Load<Texture2D>("Red2"), new Rectangle(0,0,_cellSize, _cellSize), new Rectangle(0, 0, _cellSize, _cellSize));
            redTargetColor.Enabled = false;
            redTargetColor.Visible = false;
            redTargetColor.HasFocus = false;
            redTargetColor.TabStop = false;
            redTargetColor.SetPosition(targetPosition);
            ControlManager.Add(redTargetColor);

            blueTargetColor = new PictureBox(content.Load<Texture2D>("Blue2"), new Rectangle(0, 0, _cellSize, _cellSize), new Rectangle(0, 0, _cellSize, _cellSize));
            blueTargetColor.Enabled = false;
            blueTargetColor.Visible = false;
            blueTargetColor.HasFocus = false;
            blueTargetColor.TabStop = false;
            blueTargetColor.SetPosition(targetPosition);
            ControlManager.Add(blueTargetColor);

            greenTargetColor = new PictureBox(content.Load<Texture2D>("Green2"), new Rectangle(0, 0, _cellSize, _cellSize), new Rectangle(0, 0, _cellSize, _cellSize));
            greenTargetColor.Enabled = false;
            greenTargetColor.Visible = false;
            greenTargetColor.HasFocus = false;
            greenTargetColor.TabStop = false;
            greenTargetColor.SetPosition(targetPosition);
            ControlManager.Add(greenTargetColor);

            _targetColorLabel = new Label();
            _targetColorLabel.Text = "Target Color";
            _targetColorLabel.Visible = true;
            _targetColorLabel.TabStop = false;
            _targetColorLabel.HasFocus = false;
            _targetColorLabel.Enabled = false;
            _targetColorLabel.Position = labelPosition;
            ControlManager.Add(_targetColorLabel);
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

            GameRef.SpriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _player.Update(gameTime);
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
                _gameStep = GameStep.Third;
        }

        private void HandleThirdStep()
        {
            var playerCell = _field.Cells.First(x => 
                x.Position.FieldX == _player.Position.FieldX 
                && x.Position.FieldY == _player.Position.FieldY);

            _player.OpenCell(playerCell.Color);
            _gameStep = GameStep.Fourth;
        }

        private void HandleFourthStep()
        {
            Score += 100;
            _gameStep = GameStep.First;
        }
    }
}
