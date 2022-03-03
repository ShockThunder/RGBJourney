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
        private int stepSize;
        private int borderThickness;
        private Texture2D playerTexture;
        private SpriteBatch spriteBatch;

        public Player(int stepSize, int borderThickness, int fieldSize, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            X = fieldSize / 2 * (stepSize + borderThickness);
            Y = fieldSize / 2 * (stepSize + borderThickness);
            this.stepSize = stepSize;
            this.borderThickness = borderThickness;
            this.spriteBatch = spriteBatch;
            playerTexture = new Texture2D(graphicsDevice, 1, 1);
            playerTexture.SetData(new Color[] { Color.CornflowerBlue });
        }

        public void Draw()
        {
            var rec = new Rectangle(X, Y, stepSize, stepSize);
            spriteBatch.Draw(playerTexture, rec, Color.CornflowerBlue);
        }

        public void MoveRight()
        {
            X = X + (stepSize + borderThickness);
        }

        public void MoveLeft()
        {
            X = X - (stepSize + borderThickness);
        }

        public void MoveUp()
        {
            Y = Y - (stepSize + borderThickness);
        }
        public void MoveDown()
        {
            Y = Y + (stepSize + borderThickness);
        }
    }
}
