using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace RgbJourney
{
    public class FieldGenerator
    {
        private int cellSize = 20;
        private int cellSpacing = 2;
        public int[,] GenerateArray(int arraySize)
        {
            var random = new Random();
            var array = new int[arraySize, arraySize];

            for (int i = 0; i < arraySize; i++)
            {
                for (int j = 0; j < arraySize; j++)
                {
                    array[i, j] = random.Next(3);
                }
            }

            return array;
        }

        public void DrawField(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, int[,] field)
        {
            var redTexture = new Texture2D(graphicsDevice, 1, 1);
            redTexture.SetData(new Color[] { Color.Red });

            var blueTexture = new Texture2D(graphicsDevice, 1, 1);
            blueTexture.SetData(new Color[] { Color.Blue });

            var greenTexture = new Texture2D(graphicsDevice, 1, 1);
            greenTexture.SetData(new Color[] { Color.Green });

            var defaultRect = new Rectangle(0, 0, cellSize, cellSize);

            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    var rect = defaultRect;
                    rect.X = i * (cellSize + cellSpacing);
                    rect.Y = j * (cellSize + cellSpacing);
                    switch (field[i, j])
                    {
                        case 0:
                            spriteBatch.Draw(redTexture, rect, Color.DarkRed);
                            break;
                        case 1:
                            spriteBatch.Draw(blueTexture, rect, Color.DarkBlue);
                            break;
                        case 2:
                            spriteBatch.Draw(greenTexture, rect, Color.Green);
                            break;
                    }

                }
            }

            var whiteTexture = new Texture2D(graphicsDevice, 1, 1);
            whiteTexture.SetData(new Color[] { Color.White });
            defaultRect.X = field.GetLength(0) / 2 * (cellSize + cellSpacing);
            defaultRect.Y = field.GetLength(0) / 2 * (cellSize + cellSpacing);
            spriteBatch.Draw(whiteTexture, defaultRect, Color.White);
        }
    }
}
