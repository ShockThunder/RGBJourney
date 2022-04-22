using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RgbJourney.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgbJourney.Models
{
    /// <summary>
    /// Game field
    /// </summary>
    public class Field
    {
        public int Size { get; set; }
        public int CellSize { get; set; }
        public int CellSpacing { get; set; }
        private Random random = new Random();
        public List<Cell> Cells { get; set; } = new List<Cell>();
        public List<Cell> WinCells { get; set; } = new List<Cell>();

        private Texture2D redTexture { get; set; }
        private Texture2D blueTexture { get; set; }
        private Texture2D greenTexture { get; set; }
        private Texture2D whiteTexture { get; set; }

        public Field(int cellSize, int cellSpacing, int fieldSize, ContentManager content)
        {
            CellSize = cellSize;
            CellSpacing = cellSpacing;
            Size = fieldSize;

            redTexture = content.Load<Texture2D>("Red2");
            blueTexture = content.Load<Texture2D>("Blue2");
            greenTexture = content.Load<Texture2D>("Green2");
            whiteTexture = content.Load<Texture2D>("WinCell");

            GenerateField();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw main field
            var cellSize = CellSize;

            var defaultRect = new Rectangle(0, 0, cellSize, cellSize);
            var smallRect = new Rectangle(0,0, cellSize/2, cellSize/2);

            foreach (var cell in Cells)
            {
                var rect = defaultRect;
                rect.X = cell.Position.X;
                rect.Y = cell.Position.Y;

                switch (cell.Color)
                {
                    case CustomColor.Red:
                        spriteBatch.Draw(redTexture, rect, Color.White);
                        break;
                    case CustomColor.Blue:
                        spriteBatch.Draw(blueTexture, rect, Color.White);
                        break;
                    case CustomColor.Green:
                        spriteBatch.Draw(greenTexture, rect, Color.White);
                        break;
                }

                if(cell.IsOpened && cell.CellType == CellType.Treasure)
                {
                    smallRect.X = cell.Position.X + cellSize / 4;
                    smallRect.Y = cell.Position.Y + cellSize / 4;
                    spriteBatch.Draw(whiteTexture, smallRect, Color.White);
                }
            }

            foreach (var winCell in WinCells)
            {
                defaultRect.X = winCell.Position.X;
                defaultRect.Y = winCell.Position.Y;
                spriteBatch.Draw(whiteTexture, defaultRect, Color.White);
            }        
        }

        private void GenerateField()
        {
            var array = new int[Size, Size];

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
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
                        || i == 0 && j == Size - 1
                        || i == Size - 1 && j == 0
                        || i == Size - 1 && j == Size - 1)
                        cell.Color = CustomColor.White;

                    Cells.Add(cell);
                }

                WinCells.Add(BuildCell(0, 0, CustomColor.White));
                WinCells.Add(BuildCell(0, Size - 1, CustomColor.White));
                WinCells.Add(BuildCell(Size - 1, 0, CustomColor.White));
                WinCells.Add(BuildCell(Size - 1, Size - 1, CustomColor.White));
            }
        }

        private Cell BuildCell(int i, int j, CustomColor color) => new Cell
        {
            Position = new Position
            {
                FieldX = i,
                FieldY = j,
                X = i * (CellSize + CellSpacing),
                Y = j * (CellSize + CellSpacing)
            },
            Color = color,
            IsOpened = false,
            CellType = random.Next(5) == 3 ? CellType.Treasure : CellType.Empty
        };
    }
}
