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
        }
        private int cellSize;
        private int cellSpacing;
        private readonly int screenWidth;
        private readonly int screenHeight;
        private readonly SpriteBatch spriteBatch;
        private readonly GraphicsDevice graphicsDevice;
        private readonly ContentManager content;
        private SpriteFont font;

        public void Draw()
        {
            var redTexture = new Texture2D(graphicsDevice, 1, 1);
            redTexture.SetData(new Color[] { Color.Red });

            var blueTexture = new Texture2D(graphicsDevice, 1, 1);
            blueTexture.SetData(new Color[] { Color.Blue });

            var greenTexture = new Texture2D(graphicsDevice, 1, 1);
            greenTexture.SetData(new Color[] { Color.Green });

            string colorMsg = "Select color";
            spriteBatch.DrawString(font, colorMsg, new Vector2(500, 100), Color.White);

            var defaultRect = new Rectangle(400, 400, cellSize, cellSize);

            spriteBatch.Draw(redTexture, defaultRect, Color.DarkRed);


            spriteBatch.Draw(blueTexture, defaultRect, Color.DarkBlue);
            spriteBatch.Draw(greenTexture, defaultRect, Color.Green);
        }
    }
}
