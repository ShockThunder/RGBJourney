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
        public List<CellModel> cells = new List<CellModel>();
        public List<CellModel> winCells = new List<CellModel>();


        public FieldManager(int cellSize, int cellSpacing,
            SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Random random)
        {
            this.cellSize = cellSize;
            this.cellSpacing = cellSpacing;
            _spriteBatch = spriteBatch;
            _graphicsDevice = graphicsDevice;
            this.random = random;

            redTexture = new Texture2D(_graphicsDevice, 1, 1);
            redTexture.SetData(new Color[] { Color.LightCoral });

            blueTexture = new Texture2D(_graphicsDevice, 1, 1);
            blueTexture.SetData(new Color[] { Color.DodgerBlue });

            greenTexture = new Texture2D(_graphicsDevice, 1, 1);
            greenTexture.SetData(new Color[] { Color.LightGreen });
        }

        public int[,] GenerateArray(int arraySize)
        {
            var array = new int[arraySize, arraySize];

            for (int i = 0; i < arraySize; i++)
            {
                for (int j = 0; j < arraySize; j++)
                {
                    var randomNumber = random.Next(0, 3);
                    array[i, j] = randomNumber;
                    var cell = BuildCell(i, j, CustomColor.Red);
                    switch (randomNumber)
                    {
                        case 0:
                            cell.Color = CustomColor.Red;
                            break;
                        case 1:
                            cell.Color = CustomColor.Blue;
                            break;
                        case 2:
                            cell.Color = CustomColor.Green;
                            break;
                        default:
                            break;
                    }
                    if (i == 0 && j == 0
                        || i == 0 && j == arraySize - 1
                        || i == arraySize - 1 && j == 0
                        || i == arraySize - 1 && j == arraySize - 1)
                        cell.Color = CustomColor.White;

                    cells.Add(cell);
                }
            }

            winCells.Add(BuildCell(0, 0, CustomColor.White));
            winCells.Add(BuildCell(0, arraySize - 1, CustomColor.White));
            winCells.Add(BuildCell(arraySize - 1, 0, CustomColor.White));
            winCells.Add(BuildCell(arraySize - 1, arraySize - 1, CustomColor.White));

            return array;
        }

        public void DrawField(int[,] field)
        {
            var bigRect = new Rectangle(0, 0, (cellSize + cellSpacing) * field.GetLength(0), (cellSize + cellSpacing) * field.GetLength(0));
            var backTexture = new Texture2D(_graphicsDevice, 1, 1);
            backTexture.SetData(new Color[] { Color.Black });
            _spriteBatch.Draw(backTexture, bigRect, Color.Black);


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
                            _spriteBatch.Draw(redTexture, rect, Color.LightCoral);
                            break;
                        case 1:
                            _spriteBatch.Draw(blueTexture, rect, Color.DodgerBlue);
                            break;
                        case 2:
                            _spriteBatch.Draw(greenTexture, rect, Color.LightGreen);
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
            if (endCell.Color == selectedColor || endCell.Color == CustomColor.White)
                return true;

            return false;
        }

        public bool CheckWinCondition(Position playerPosition) =>
            winCells.Any(x => x.Position.X == playerPosition.X && x.Position.Y == playerPosition.Y);
    }
}
