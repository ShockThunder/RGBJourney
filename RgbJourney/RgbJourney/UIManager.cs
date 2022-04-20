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


            _targetColorLabel = new Label();
            _targetColorLabel.Text = "Target Color";
            _targetColorLabel.Visible = true;
            _targetColorLabel.TabStop = false;
            _targetColorLabel.HasFocus = false;
            _targetColorLabel.Enabled = false;
            _targetColorLabel.Position = targetLabelPosition;
            ControlManager.Add(_targetColorLabel);

            _keysLabel = new Label();
            _keysLabel.Text = "Remained Keys";
            _keysLabel.Visible = true;
            _keysLabel.TabStop = false;
            _keysLabel.HasFocus = false;
            _keysLabel.Enabled = false;
            _keysLabel.Position = keysLabelPosition;
            ControlManager.Add(_keysLabel);

            _redKeyLabel = new PictureBox(Content.Load<Texture2D>("Red2"), new Rectangle(0, 0, squareSpace, squareSpace), new Rectangle(0, 0, squareSpace, squareSpace));
            _redKeyLabel.Enabled = false;
            _redKeyLabel.Visible = true;
            _redKeyLabel.HasFocus = false;
            _redKeyLabel.TabStop = false;
            _redKeyLabel.SetPosition(colorsLabelPosition);
            ControlManager.Add(_redKeyLabel);

            _blueKeyLabel = new PictureBox(Content.Load<Texture2D>("Blue2"), new Rectangle(0, 0, squareSpace, squareSpace), new Rectangle(0, 0, squareSpace, squareSpace));
            _blueKeyLabel.Enabled = false;
            _blueKeyLabel.Visible = true;
            _blueKeyLabel.HasFocus = false;
            _blueKeyLabel.TabStop = false;
            _blueKeyLabel.SetPosition(new Vector2(colorsLabelPosition.X + squareSpace + 10f, colorsLabelPosition.Y));
            ControlManager.Add(_blueKeyLabel);

            _greenKeyLabel = new PictureBox(Content.Load<Texture2D>("Green2"), new Rectangle(0, 0, squareSpace, squareSpace), new Rectangle(0, 0, squareSpace, squareSpace));
            _greenKeyLabel.Enabled = false;
            _greenKeyLabel.Visible = true;
            _greenKeyLabel.HasFocus = false;
            _greenKeyLabel.TabStop = false;
            _greenKeyLabel.SetPosition(new Vector2(colorsLabelPosition.X + (squareSpace + 10f) * 2, colorsLabelPosition.Y));
            ControlManager.Add(_greenKeyLabel);

            _redValueLabel = new Label();
            _redValueLabel.Visible = true;
            _redValueLabel.TabStop = false;
            _redValueLabel.HasFocus = false;
            _redValueLabel.Enabled = false;
            _redValueLabel.Text = "R";
            _redValueLabel.Position = keysValueLabelPosition;
            ControlManager.Add(_redValueLabel);

            _blueValueLabel = new Label();
            _blueValueLabel.Visible = true;
            _blueValueLabel.TabStop = false;
            _blueValueLabel.HasFocus = false;
            _blueValueLabel.Enabled = false;
            _blueValueLabel.Text = "B";
            _blueValueLabel.Position = new Vector2(keysValueLabelPosition.X + squareSpace + 10f, keysValueLabelPosition.Y);
            ControlManager.Add(_blueValueLabel);

            _greenValueLabel = new Label();
            _greenValueLabel.Visible = true;
            _greenValueLabel.TabStop = false;
            _greenValueLabel.HasFocus = false;
            _greenValueLabel.Enabled = false;
            _greenValueLabel.Text = "G";
            _greenValueLabel.Position = new Vector2(keysValueLabelPosition.X + (squareSpace + 10f) * 2, keysValueLabelPosition.Y); ;
            ControlManager.Add(_greenValueLabel);
        }    

        public void Update(GameTime gameTime, Player player)
        {
            _redValueLabel.Text = player.Character.RedKeys.ToString();
            _blueValueLabel.Text = player.Character.BlueKeys.ToString();
            _greenValueLabel.Text = player.Character.GreenKeys.ToString();
            ControlManager.Update(gameTime, playerIndex);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            ControlManager.Draw(spriteBatch);
        }
    }
}
