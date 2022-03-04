using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgbJourney
{
    public class UIManager
    {
        public UIManager(int cellSize, int cellSpacing, int screenWidth,
            int screenHeight, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, ContentManager content)
        {
            this.cellSize = cellSize;
            this.cellSpacing = cellSpacing;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            this.spriteBatch = spriteBatch;
            this.graphicsDevice = graphicsDevice;
            this.content = content;
            font = content.Load<SpriteFont>("Main");

            redSquare = new Rectangle(screenWidth / 2, screenHeight / 10 + cellSize * 2, cellSize, cellSize);
            blueSquare = new Rectangle(screenWidth / 2 + cellSize * 2, screenHeight / 10 + cellSize * 2, cellSize, cellSize);
            greenSquare = new Rectangle(screenWidth / 2 + cellSize * 4, screenHeight / 10 + cellSize * 2, cellSize, cellSize);

        }
        private int cellSize;
        private int cellSpacing;
        private readonly int screenWidth;
        private readonly int screenHeight;
        private readonly SpriteBatch spriteBatch;
        private readonly GraphicsDevice graphicsDevice;
        private readonly ContentManager content;
        private SpriteFont font;

        public Rectangle redSquare;
        public Rectangle blueSquare;
        public Rectangle greenSquare;

        public void Draw()
        {
            var redTexture = new Texture2D(graphicsDevice, 1, 1);
            redTexture.SetData(new Color[] { Color.Red });

            var blueTexture = new Texture2D(graphicsDevice, 1, 1);
            blueTexture.SetData(new Color[] { Color.Blue });

            var greenTexture = new Texture2D(graphicsDevice, 1, 1);
            greenTexture.SetData(new Color[] { Color.Green });

            string colorMsg = "Select color";
            spriteBatch.DrawString(font, colorMsg, new Vector2(screenWidth / 2, screenHeight / 10 - cellSize * 2), Color.White);

            string keysMessage = "1 - RED, 2 - GREEN, 3 - BLUE";
            spriteBatch.DrawString(font, keysMessage, new Vector2(screenWidth / 2, screenHeight / 10), Color.White);

            var redRect = new Rectangle(screenWidth / 2, screenHeight / 10 + cellSize * 2, cellSize, cellSize);
            var blueRect = new Rectangle(screenWidth / 2 + cellSize * 2, screenHeight / 10 + cellSize * 2, cellSize, cellSize);
            var greenRect = new Rectangle(screenWidth / 2 + cellSize * 4, screenHeight / 10 + cellSize * 2, cellSize, cellSize);

            spriteBatch.Draw(redTexture, redRect, Color.DarkRed);


            spriteBatch.Draw(blueTexture, blueRect, Color.DarkBlue);
            spriteBatch.Draw(greenTexture, greenRect, Color.Green);
        }

        public void DrawSelectedSquare(CustomColors color)
        {
            string colorName = string.Empty;
            Texture2D selectedTexture;
            switch (color)
            {
                case CustomColors.Red:
                    colorName = "RED";
                    break;
                case CustomColors.Green:
                    colorName = "GREEN"; break;
                case CustomColors.Blue:
                    colorName = "BLUE"; break;
                default:
                    break;
            }
            string colorMsg = $"You selected {colorName}";
            spriteBatch.DrawString(font, colorMsg, new Vector2(screenWidth / 2, screenHeight / 10 + cellSize * 4), Color.White);
            //var a = font.MeasureString(colorMsg);
            //var redRect = new Rectangle((int)a.X, (int)a.Y + cellSize * 4, cellSize, cellSize);

            //spriteBatch.Draw(greenTexture, greenRect, Color.Green);

        }
    }
}
