using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RgbJourney.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgbJourney
{
    public class Player
    {
        public Position Position { get; set; } = new Position();

        public int StepsCount = 0;
        public MovementDirection Direction = MovementDirection.NotSet;

        private int _cellSize;
        private int _cellSpacing;
        private SpriteBatch _spriteBatch;
        private readonly ResourceManager _resourceManager;

        public Player(int stepSize, int borderThickness, int fieldSize, SpriteBatch spriteBatch, ResourceManager resourceManager)
        {
            Position.X = fieldSize / 2 * (stepSize + borderThickness);
            Position.Y = fieldSize / 2 * (stepSize + borderThickness);
            _cellSize = stepSize;
            _cellSpacing = borderThickness;
            _spriteBatch = spriteBatch;
            _resourceManager = resourceManager;
            Position.FieldX = fieldSize / 2 + 1;
            Position.FieldY = fieldSize / 2 + 1;
        }

        public void Draw()
        {
            var rec = new Rectangle(Position.X, Position.Y, _cellSize, _cellSize);
            _spriteBatch.Draw(_resourceManager.PlayerTexture, rec, Color.White);
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
            Position.X = Position.X + (_cellSize + _cellSpacing);
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
            Position.Y = Position.Y + (_cellSize + _cellSpacing);
            Position.FieldY += 1;
            StepsCount--;
        }
    }
}
