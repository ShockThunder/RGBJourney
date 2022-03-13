using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RgbJourney.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgbJourney
{
    public class UIManager
    {
        public UIManager(int cellSize, int cellSpacing, int screenWidth,
            int screenHeight, int fieldSize, SpriteBatch spriteBatch, ResourceManager resourceManager)
        {
            this.cellSize = cellSize;
            this.cellSpacing = cellSpacing;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            this.spriteBatch = spriteBatch;
            _resourceManager = resourceManager;
            leftEdge = (cellSize + cellSpacing) * (fieldSize + 1);
        }

        private int cellSize;
        private int cellSpacing;
        private readonly int screenWidth;
        private readonly int screenHeight;
        private int leftEdge;
        private readonly SpriteBatch spriteBatch;
        private readonly GraphicsDevice graphicsDevice;
        private readonly ResourceManager _resourceManager;



        public void Draw()
        {
            string colorMsg = "Select target";
            spriteBatch.DrawString(_resourceManager.Font, colorMsg, new Vector2(leftEdge, screenHeight / 10 - cellSize), Color.Black);

            spriteBatch.DrawString(_resourceManager.Font, "1", new Vector2(leftEdge, screenHeight / 10), Color.Black);
            spriteBatch.DrawString(_resourceManager.Font, "2", new Vector2(leftEdge + cellSize * 2, screenHeight / 10), Color.Black);
            spriteBatch.DrawString(_resourceManager.Font, "3", new Vector2(leftEdge + cellSize * 4, screenHeight / 10), Color.Black);

            var redRect = new Rectangle(leftEdge, screenHeight / 10 + cellSize, cellSize, cellSize);
            var blueRect = new Rectangle(leftEdge + cellSize * 2, screenHeight / 10 + cellSize, cellSize, cellSize);
            var greenRect = new Rectangle(leftEdge + cellSize * 4, screenHeight / 10 + cellSize, cellSize, cellSize);

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
                    colorName = "1";
                    break;
                case CustomColor.Blue:
                    colorName = "2"; break;
                case CustomColor.Green:
                    colorName = "3"; break;
                default:
                    break;
            }
            string colorMsg = $"You selected {colorName}";
            spriteBatch.DrawString(_resourceManager.Font, colorMsg, new Vector2(leftEdge, screenHeight / 10 + cellSize * 2 + cellSize / 2), Color.Black);
        }

        public void DrawDiceResult(int[] diceRoll, int diceResult)
        {
            string diceMessage = $"You rolled {diceRoll[0]} and {diceRoll[1]}";
            string hintMessage = " 1 - sum, 2 - sub.";
            spriteBatch.DrawString(_resourceManager.Font, diceMessage, new Vector2(leftEdge, screenHeight / 10 + cellSize * 4), Color.Black);
            spriteBatch.DrawString(_resourceManager.Font, hintMessage, new Vector2(leftEdge, screenHeight / 10 + cellSize * 5), Color.Black);
            spriteBatch.DrawString(_resourceManager.Font, $"Roll result - {diceResult}", new Vector2(leftEdge, screenHeight / 10 + cellSize * 6), Color.Black);
        }

        public void DrawIllegalTurn()
        {
            string message1 = $"Non legal turn!";
            spriteBatch.DrawString(_resourceManager.Font, message1, new Vector2(leftEdge, screenHeight / 10 + cellSize * 10), Color.Red);
        }

        public void DrawWinText()
        {
            string message = "YOU WIN!";
            spriteBatch.DrawString(_resourceManager.WinFont, message, new Vector2(screenWidth / 3, screenHeight / 3), Color.Black, 0.0f, new Vector2(0 ,0), 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(_resourceManager.WinFont, message, new Vector2(screenWidth / 3 + 5, screenHeight / 3 + 5), Color.White, 0.0f, new Vector2(0 ,0), 1f, SpriteEffects.None, 0);
        }

        public void DrawTimer(double gameStartSeconds, double currentSeconds)
        {
            var time = currentSeconds - gameStartSeconds;
            var stringTime = time.ToString("F2");
            var medal = new Rectangle(screenWidth - cellSize * 7, screenHeight - cellSize * 2, cellSize, cellSize);
            var texture = _resourceManager.GoldMedal;

            spriteBatch.DrawString(_resourceManager.Font, stringTime, new Vector2(screenWidth - cellSize * 5, screenHeight - cellSize * 2), Color.Black, 0.0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0);
            
            if(time > 15 && time < 30)
                texture = _resourceManager.SilverMedal;
            if (time > 30)
                texture = _resourceManager.BronzeMedal;

            spriteBatch.Draw(texture, medal, Color.White);
        }

        public void DrawTitleScreen()
        {
            string message = "Press 1 for big field or 2 for small field";

            var size = _resourceManager.Font.MeasureString(message);

            var rec = new Rectangle(screenWidth / 5, screenHeight / 3 + cellSize * 4, (int)size.X, (int)size.Y);

            spriteBatch.Draw(_resourceManager.SilverMedal, rec, Color.White);
            spriteBatch.DrawString(_resourceManager.Font, message, new Vector2(screenWidth / 5, screenHeight / 3 + cellSize * 4), Color.Black, 0.0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0);
        }

        public void DrawLoseText()
        {
            string message = "YOU LOSE!";
            spriteBatch.DrawString(_resourceManager.WinFont, message, new Vector2(screenWidth / 3, screenHeight / 3), Color.Black, 0.0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0);
            spriteBatch.DrawString(_resourceManager.WinFont, message, new Vector2(screenWidth / 3 + 5, screenHeight / 3 + 5), Color.White, 0.0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0);
        }
    }
}
