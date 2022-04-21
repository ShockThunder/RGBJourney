using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RgbJourney.Enums;

namespace RgbJourney.Models
{
    public class Player
    {
        public Position Position { get; set; } = new Position();
        public int Score { get; set; } = 0;
        private int _cellSize;
        private int _cellSpacing;
        private Texture2D _texture { get; set; }

        public Character Character { get; set; }

        public Player(int stepSize, int borderThickness, int fieldSize, Texture2D texture)
        {
            Position.X = fieldSize / 2 * (stepSize + borderThickness);
            Position.Y = fieldSize / 2 * (stepSize + borderThickness);
            _cellSize = stepSize;
            _cellSpacing = borderThickness;
            Position.FieldX = fieldSize / 2;
            Position.FieldY = fieldSize / 2;

            _texture = texture;

            Character = new Character(4);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var rec = new Rectangle(Position.X, Position.Y, _cellSize, _cellSize);
            spriteBatch.Draw(_texture, rec, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            if (InputHandler.KeyPressed(Keys.Up))
                MoveUp();
            if (InputHandler.KeyPressed(Keys.Down))
                MoveDown();
            if (InputHandler.KeyPressed(Keys.Left))
                MoveLeft();
            if (InputHandler.KeyPressed(Keys.Right))
                MoveRight();
        }

        public void MoveRight()
        {
            if(Character.CurrentStamina > 0)
            {
                Position.X = Position.X + _cellSize + _cellSpacing;
                Position.FieldX += 1;
                Character.CurrentStamina--;
            }
        }

        public void MoveLeft()
        {
            if (Character.CurrentStamina > 0)
            {
                Position.X = Position.X - (_cellSize + _cellSpacing);
                Position.FieldX -= 1;
                Character.CurrentStamina--;
            }                
        }

        public void MoveUp()
        {
            if (Character.CurrentStamina > 0)
            {
                Position.Y = Position.Y - (_cellSize + _cellSpacing);
                Position.FieldY -= 1;
                Character.CurrentStamina--;
            }                
        }
        public void MoveDown()
        {
            if (Character.CurrentStamina > 0)
            {
                Position.Y = Position.Y + _cellSize + _cellSpacing;
                Position.FieldY += 1;
                Character.CurrentStamina--;
            }                
        }

        public void OpenCell(CustomColor color)
        {
            switch (color)
            {
                case CustomColor.Red:
                    Character.RedKeys--;
                    break;
                case CustomColor.Blue:
                    Character.BlueKeys--;
                    break;
                case CustomColor.Green:
                    Character.GreenKeys--;
                    break;
                default:
                    break;
            }

            Character.CurrentStamina = Character.MaxStamina;
        }
    }
}