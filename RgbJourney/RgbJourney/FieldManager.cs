using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RgbJourney
{
    public class FieldManager
    {
        private int cellSize;
        private int cellSpacing;
        private readonly SpriteBatch _spriteBatch;
        private readonly GraphicsDevice _graphicsDevice;
        private Texture2D redTexture;
        private Texture2D blueTexture;
        private Texture2D greenTexture;
        private Random random;

        public Position OldPlayerPosition = new Position();

        public FieldManager(int cellSize, int cellSpacing,
            SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Random random)
        {
            this.cellSize = cellSize;
            this.cellSpacing = cellSpacing;
            _spriteBatch = spriteBatch;
            _graphicsDevice = graphicsDevice;
            this.random = random;

            redTexture = new Texture2D(graphicsDevice, 1, 1);
            redTexture.SetData(new Color[] { Color.Red });

            blueTexture = new Texture2D(graphicsDevice, 1, 1);
            blueTexture.SetData(new Color[] { Color.Blue });

            greenTexture = new Texture2D(graphicsDevice, 1, 1);
            greenTexture.SetData(new Color[] { Color.Green });
        }

        public List<CellModel> cells = new List<CellModel>();
        public int[,] GenerateArray(int arraySize)
        {
            var array = new int[arraySize, arraySize];

            for (int i = 0; i < arraySize; i++)
            {
                for (int j = 0; j < arraySize; j++)
                {
                    var randomNumber = random.Next(0, 3);
                    array[i, j] = randomNumber;
                    switch (randomNumber)
                    {
                        case 0:
                            cells.Add(BuildCell(i, j, CustomColor.Red));
                            break;
                        case 1:
                            cells.Add(BuildCell(i, j, CustomColor.Blue));
                            break;
                        case 2:
                            cells.Add(BuildCell(i, j, CustomColor.Green));
                            break;
                        default:
                            break;
                    }
                }
            }

            return array;
        }

        public void DrawField(int[,] field)
        {
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
                            _spriteBatch.Draw(redTexture, rect, Color.DarkRed);
                            break;
                        case 1:
                            _spriteBatch.Draw(blueTexture, rect, Color.DarkBlue);
                            break;
                        case 2:
                            _spriteBatch.Draw(greenTexture, rect, Color.Green);
                            break;
                    }
                }
            }

            var whiteTexture = new Texture2D(_graphicsDevice, 1, 1);
            whiteTexture.SetData(new Color[] { Color.White });
            defaultRect.X = field.GetLength(0) / 2 * (cellSize + cellSpacing);
            defaultRect.Y = field.GetLength(0) / 2 * (cellSize + cellSpacing);
            _spriteBatch.Draw(whiteTexture, defaultRect, Color.White);

            // Fill corners
            defaultRect.X = 0;
            defaultRect.Y = 0;
            _spriteBatch.Draw(whiteTexture, defaultRect, Color.White);

            defaultRect.X = 0;
            defaultRect.Y = (field.GetLength(0) - 1) * (cellSize + cellSpacing);
            _spriteBatch.Draw(whiteTexture, defaultRect, Color.White);

            defaultRect.X = (field.GetLength(0) - 1) * (cellSize + cellSpacing);
            defaultRect.Y = 0;
            _spriteBatch.Draw(whiteTexture, defaultRect, Color.White);

            defaultRect.X = (field.GetLength(0) - 1) * (cellSize + cellSpacing);
            defaultRect.Y = (field.GetLength(0) - 1) * (cellSize + cellSpacing);
            _spriteBatch.Draw(whiteTexture, defaultRect, Color.White);

        }

        private CellModel BuildCell(int i, int j, CustomColor color) => new CellModel
        {
            Position = new Position
            {
                FieldX = i,
                FieldY = j,
                X = i * (cellSize + cellSpacing),
                Y = j * (cellSize + cellSpacing)
            },
            Color = color,
        };

        public bool CanEndTurn(Position playerPosition, CustomColor selectedColor)
        {
            var endCell = cells.FirstOrDefault(x => x.Position.X == playerPosition.X
                                                && x.Position.Y == playerPosition.Y);
            if (endCell.Color == selectedColor)
                return true;

            return false;
        }
    }
}
