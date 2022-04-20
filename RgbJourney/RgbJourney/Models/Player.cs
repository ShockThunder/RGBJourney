using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RgbJourney.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgbJourney.Models
{
    public class Player
    {
        public Position Position { get; set; } = new Position();
        public int StepsCount = 0;
        public MovementDirection Direction = MovementDirection.NotSet;

        private int _cellSize;
        private int _cellSpacing;
        private Texture2D _texture { get; set; }

        public Player(int stepSize, int borderThickness, int fieldSize, Texture2D texture)
        {
            Position.X = fieldSize / 2 * (stepSize + borderThickness);
            Position.Y = fieldSize / 2 * (stepSize + borderThickness);
            _cellSize = stepSize;
            _cellSpacing = borderThickness;
            Position.FieldX = fieldSize / 2;
            Position.FieldY = fieldSize / 2;

            _texture = texture;
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

        public void Move()
        {
            switch (Direction)
            {
                case MovementDirection.Up:
                    MoveUp();
                    break;
                case MovementDirection.Down:
                    MoveDown();
                    break;
                case MovementDirection.Left:
                    MoveLeft();
                    break;
                case MovementDirection.Right:
                    MoveRight();
                    break;
                default:
                    break;
            }
        }

        public void MoveRight()
        {
            Position.X = Position.X + _cellSize + _cellSpacing;
            Position.FieldX += 1;
            StepsCount--;
        }

        public void MoveLeft()
        {
            Position.X = Position.X - (_cellSize + _cellSpacing);
            Position.FieldX -= 1;
            StepsCount--;
        }

        public void MoveUp()
        {
            Position.Y = Position.Y - (_cellSize + _cellSpacing);
            Position.FieldY -= 1;
            StepsCount--;
        }
        public void MoveDown()
        {
            Position.Y = Position.Y + _cellSize + _cellSpacing;
            Position.FieldY += 1;
            StepsCount--;
        }
    }
}
