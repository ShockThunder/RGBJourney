using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgbJourney
{
    public class Player
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int FieldX { get; set; }
        public int FieldY { get; set; }
        private int _cellSize;
        private int _cellSpacing;
        private Texture2D playerTexture;
        private SpriteBatch _spriteBatch;

        public Player(int stepSize, int borderThickness, int fieldSize, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            X = fieldSize / 2 * (stepSize + borderThickness);
            Y = fieldSize / 2 * (stepSize + borderThickness);
            _cellSize = stepSize;
            _cellSpacing = borderThickness;
            _spriteBatch = spriteBatch;
            playerTexture = new Texture2D(graphicsDevice, 1, 1);
            playerTexture.SetData(new Color[] { Color.CornflowerBlue });
            FieldX = fieldSize / 2 + 1;
            FieldY = fieldSize / 2 + 1;
        }

        public void Draw()
        {
            var rec = new Rectangle(X, Y, _cellSize, _cellSize);
            _spriteBatch.Draw(playerTexture, rec, Color.CornflowerBlue);
        }

        public void MoveRight()
        {
            X = X + (_cellSize + _cellSpacing);
            FieldX += 1;
        }

        public void MoveLeft()
        {
            X = X - (_cellSize + _cellSpacing);
            FieldX -= 1;
        }

        public void MoveUp()
        {
            Y = Y - (_cellSize + _cellSpacing);
            FieldY -= 1;
        }
        public void MoveDown()
        {
            Y = Y + (_cellSize + _cellSpacing);
            FieldY += 1;
        }
    }
}
