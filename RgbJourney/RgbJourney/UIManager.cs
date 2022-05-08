using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RgbJourney.Controls;
using RgbJourney.Models;
using System.IO;

namespace RgbJourney
{
    public class UIManager
    {
        private Label _targetColorLabel;
        private Label _keysLabel;
        private Label _redValueLabel;
        private Label _blueValueLabel;
        private Label _greenValueLabel;
        private Label _scoreLabel;
        private Label _scoreValueLabel;
        private Label _playerHealthLabel;
        private Label _playerHealthValueLabel;

        private PictureBox _redKeyLabel;
        private PictureBox _blueKeyLabel;
        private PictureBox _greenKeyLabel;

        private ControlManager ControlManager;
        private ContentManager Content;

        private int squareSpace = 40;
        private PlayerIndex playerIndex = PlayerIndex.One;
        public UIManager(Game game)
        {
            Content = game.Content;
            var menuFont = Content.Load<SpriteFont>(Path.Combine("Fonts", "ControlFont"));
            ControlManager = new ControlManager(menuFont);

            Initialize();
        }

        public void Initialize()
        {
            var keysLabelPosition = new Vector2(800, 150);
            var colorsLabelPosition = new Vector2(800, 200);
            var keysValueLabelPosition = new Vector2(800, 250);
            var targetLabelPosition = new Vector2(800, 70);
            var scoreLabelPosition = new Vector2(800, 300);
            var playerHealthLabelPosition = new Vector2(800, 350);

            _targetColorLabel = new Label
            {
                Text = "Target Color",
                Visible = true,
                TabStop = false,
                HasFocus = false,
                Enabled = false,
                Position = targetLabelPosition
            };
            ControlManager.Add(_targetColorLabel);

            _keysLabel = new Label
            {
                Text = "Remained Keys",
                Visible = true,
                TabStop = false,
                HasFocus = false,
                Enabled = false,
                Position = keysLabelPosition
            };
            ControlManager.Add(_keysLabel);

            _redKeyLabel = new PictureBox(Content.Load<Texture2D>("Red2"), new Rectangle(0, 0, squareSpace, squareSpace), new Rectangle(0, 0, squareSpace, squareSpace))
            {
                Enabled = false,
                Visible = true,
                HasFocus = false,
                TabStop = false,
            };
            _redKeyLabel.SetPosition(colorsLabelPosition);
            ControlManager.Add(_redKeyLabel);

            _blueKeyLabel = new PictureBox(Content.Load<Texture2D>("Blue2"), new Rectangle(0, 0, squareSpace, squareSpace), new Rectangle(0, 0, squareSpace, squareSpace))
            {
                Enabled = false,
                Visible = true,
                HasFocus = false,
                TabStop = false
            };
            _blueKeyLabel.SetPosition(new Vector2(colorsLabelPosition.X + squareSpace + 10f, colorsLabelPosition.Y));

            ControlManager.Add(_blueKeyLabel);

            _greenKeyLabel = new PictureBox(Content.Load<Texture2D>("Green2"), new Rectangle(0, 0, squareSpace, squareSpace), new Rectangle(0, 0, squareSpace, squareSpace))
            {
                Enabled = false,
                Visible = true,
                HasFocus = false,
                TabStop = false
            };
            _greenKeyLabel.SetPosition(new Vector2(colorsLabelPosition.X + (squareSpace + 10f) * 2, colorsLabelPosition.Y));
            ControlManager.Add(_greenKeyLabel);

            _redValueLabel = new Label
            {
                Visible = true,
                TabStop = false,
                HasFocus = false,
                Enabled = false,
                Text = "R",
                Position = keysValueLabelPosition
            };
            ControlManager.Add(_redValueLabel);

            _blueValueLabel = new Label
            {
                Visible = true,
                TabStop = false,
                HasFocus = false,
                Enabled = false,
                Text = "B",
                Position = new Vector2(keysValueLabelPosition.X + squareSpace + 10f, keysValueLabelPosition.Y)
            };
            ControlManager.Add(_blueValueLabel);

            _greenValueLabel = new Label
            {
                Visible = true,
                TabStop = false,
                HasFocus = false,
                Enabled = false,
                Text = "G",
                Position = new Vector2(keysValueLabelPosition.X + (squareSpace + 10f) * 2, keysValueLabelPosition.Y)
            };            
            ControlManager.Add(_greenValueLabel);

            _scoreLabel = new Label
            {
                Visible = true,
                TabStop = false,
                HasFocus = false,
                Enabled = false,
                Text = "Score:",
                Position = new Vector2(scoreLabelPosition.X, scoreLabelPosition.Y)
            };            
            ControlManager.Add(_scoreLabel);

            _scoreValueLabel = new Label
            {
                Visible = true,
                TabStop = false,
                HasFocus = false,
                Enabled = false,
                Text = "0",
                Position = new Vector2(
                scoreLabelPosition.X + _scoreLabel.SpriteFont.MeasureString(_scoreLabel.Text).X,
                scoreLabelPosition.Y)
            };        
            ControlManager.Add(_scoreValueLabel);
            
            _playerHealthLabel = new Label
            {
                Visible = true,
                TabStop = false,
                HasFocus = false,
                Enabled = false,
                Text = "Health: ",
                Position = new Vector2(
                    playerHealthLabelPosition.X, playerHealthLabelPosition.Y)
            };        
            ControlManager.Add(_playerHealthLabel);
            
            _playerHealthValueLabel = new Label
            {
                Visible = true,
                TabStop = false,
                HasFocus = false,
                Enabled = false,
                Text = "0",
                Position = new Vector2(
                    playerHealthLabelPosition.X + _playerHealthLabel.SpriteFont.MeasureString(_playerHealthLabel.Text).X,
                    playerHealthLabelPosition.Y)
            };        
            ControlManager.Add(_playerHealthValueLabel);
        }    

        public void Update(GameTime gameTime, Player player, int score)
        {
            _redValueLabel.Text = player.Character.RedKeys.ToString();
            _blueValueLabel.Text = player.Character.BlueKeys.ToString();
            _greenValueLabel.Text = player.Character.GreenKeys.ToString();
            _scoreValueLabel.Text = score.ToString();
            _playerHealthValueLabel.Text = player.Character.CurrentHealth.ToString();
            ControlManager.Update(gameTime, playerIndex);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            ControlManager.Draw(spriteBatch);
        }
    }
}
