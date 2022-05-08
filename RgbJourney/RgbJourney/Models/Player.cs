using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RgbJourney.Enums;

namespace RgbJourney.Models
{
    public class Player
    {
        public Position Position { get; set; } = new Position();
        private int _cellSize;
        private int _cellSpacing;
        private int _fieldSize;
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

            Character = new Character(4, 10);
            _fieldSize = fieldSize;
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

        private void MoveRight()
        {
            if (Character.CurrentStamina <= 0)
                return;
            
            if (Position.FieldX >= _fieldSize)
                return;
            
            Position.X += _cellSize + _cellSpacing;
            Position.FieldX += 1;
            Character.CurrentStamina--;
        }

        private void MoveLeft()
        {
            if (Character.CurrentStamina <= 0)
                return;
            if (Position.FieldX < 0)
                return;
            Position.X -= _cellSize + _cellSpacing;
            Position.FieldX -= 1;
            Character.CurrentStamina--;
        }

        private void MoveUp()
        {
            if (Character.CurrentStamina <= 0)
                return;
            if(Position.FieldY < 0)
                return;
            
            Position.Y -= _cellSize + _cellSpacing;
            Position.FieldY -= 1;
            Character.CurrentStamina--;
        }

        private void MoveDown()
        {
            if (Character.CurrentStamina <= 0)
                return;
            if(Position.FieldY >= _fieldSize)
                return;
            
            Position.Y = Position.Y + _cellSize + _cellSpacing;
            Position.FieldY += 1;
            Character.CurrentStamina--;
        }

        public void RefreshStamina()
        {
            Character.CurrentStamina = Character.MaxStamina;
        }       
        
        public void RefreshHealth()
        {
            Character.CurrentHealth = Character.MaxHealth;
        }
    }
}