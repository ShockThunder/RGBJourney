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
            int screenHeight, SpriteBatch spriteBatch, ResourceManager resourceManager)
        {
            this.cellSize = cellSize;
            this.cellSpacing = cellSpacing;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            this.spriteBatch = spriteBatch;
            _resourceManager = resourceManager;

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
        private readonly ResourceManager _resourceManager;

        public Rectangle redSquare;
        public Rectangle blueSquare;
        public Rectangle greenSquare;

        public void Draw()
        {
            string colorMsg = "Select color";
            spriteBatch.DrawString(_resourceManager.Font, colorMsg, new Vector2(screenWidth / 2, screenHeight / 10 - cellSize * 2), Color.White);

            string keysMessage = "1 - RED, 2 - BLUE, 3 - GREEN";
            spriteBatch.DrawString(_resourceManager.Font, keysMessage, new Vector2(screenWidth / 2, screenHeight / 10), Color.White);

            var redRect = new Rectangle(screenWidth / 2, screenHeight / 10 + cellSize * 2, cellSize, cellSize);
            var blueRect = new Rectangle(screenWidth / 2 + cellSize * 2, screenHeight / 10 + cellSize * 2, cellSize, cellSize);
            var greenRect = new Rectangle(screenWidth / 2 + cellSize * 4, screenHeight / 10 + cellSize * 2, cellSize, cellSize);

            spriteBatch.Draw(_resourceManager.RedTexture, redRect, Color.White);
            spriteBatch.Draw(_resourceManager.BlueTexture, blueRect, Color.White);
            spriteBatch.Draw(_resourceManager.GreenTexture, greenRect, Color.White);
        }

        public void DrawSelectedSquare(CustomColor color)
        {
            string colorName = string.Empty;
            switch (color)
            {
                case CustomColor.Red:
                    colorName = "RED";
                    break;
                case CustomColor.Green:
                    colorName = "GREEN"; break;
                case CustomColor.Blue:
                    colorName = "BLUE"; break;
                default:
                    break;
            }
            string colorMsg = $"You selected {colorName}";
            spriteBatch.DrawString(_resourceManager.Font, colorMsg, new Vector2(screenWidth / 2, screenHeight / 10 + cellSize * 4), Color.White);
        }

        public void DrawDiceResult(int[] diceRoll, int diceResult)
        {
            string diceMessage = $"You rolled {diceRoll[0]} and {diceRoll[1]}. 1 - sum, 2 - substract.";
            spriteBatch.DrawString(_resourceManager.Font, diceMessage, new Vector2(screenWidth / 2, screenHeight / 10 + cellSize * 6), Color.White);
            spriteBatch.DrawString(_resourceManager.Font, $"Roll result - {diceResult}", new Vector2(screenWidth / 2, screenHeight / 10 + cellSize * 8), Color.White);
        }

        public void DrawIllegalTurn()
        {
            string message1 = $"Non legal turn!";
            string message2 = $"You should end turn in selected color!";
            spriteBatch.DrawString(_resourceManager.Font, message1, new Vector2(screenWidth / 2, screenHeight / 10 + cellSize * 10), Color.Coral);
            spriteBatch.DrawString(_resourceManager.Font, message2, new Vector2(screenWidth / 2, screenHeight / 10 + cellSize * 12), Color.Coral);
        }

        public void DrawWinText()
        {
            string message = "YOU WIN!";
            spriteBatch.DrawString(_resourceManager.Font, message, new Vector2(screenWidth / 2, screenHeight / 2), Color.AliceBlue);
            spriteBatch.DrawString(_resourceManager.Font, message, new Vector2(screenWidth / 2, screenHeight / 2 + cellSize * 2), Color.AliceBlue);
            spriteBatch.DrawString(_resourceManager.Font, message, new Vector2(screenWidth / 2, screenHeight / 2 + cellSize * 4), Color.AliceBlue);
        }
    }
}
