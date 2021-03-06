using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RgbJourney.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RgbJourney
{
    public class FieldManager
    {
        private int _cellSize;
        private int _cellSpacing;
        private int _fieldSize;
        private readonly SpriteBatch _spriteBatch;
        private readonly ResourceManager _resourceManager;
        private Random _random;

        public Position OldPlayerPosition = new Position();
        public List<CellModel> cells = new List<CellModel>();
        public List<CellModel> winCells = new List<CellModel>();


        public FieldManager(int cellSize, int cellSpacing,
            SpriteBatch spriteBatch, ResourceManager resourceManager, Random random, int fieldSize)
        {
            _cellSize = cellSize;
            _cellSpacing = cellSpacing;
            _spriteBatch = spriteBatch;
            _resourceManager = resourceManager;
            _random = random;
            _fieldSize = fieldSize;
        }

        public int[,] GenerateArray()
        {
            var array = new int[_fieldSize, _fieldSize];

            for (int i = 0; i < _fieldSize; i++)
            {
                for (int j = 0; j < _fieldSize; j++)
                {
                    var randomNumber = _random.Next(0, 3);
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
                        || i == 0 && j == _fieldSize - 1
                        || i == _fieldSize - 1 && j == 0
                        || i == _fieldSize - 1 && j == _fieldSize - 1)
                        cell.Color = CustomColor.White;

                    cells.Add(cell);
                }
            }

            winCells.Add(BuildCell(0, 0, CustomColor.White));
            winCells.Add(BuildCell(0, _fieldSize - 1, CustomColor.White));
            winCells.Add(BuildCell(_fieldSize - 1, 0, CustomColor.White));
            winCells.Add(BuildCell(_fieldSize - 1, _fieldSize - 1, CustomColor.White));

            return array;
        }

        public void DrawField(int[,] field)
        {
            // Draw back texture
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    var sourceRec = new Rectangle(0, 0, 256, 256);
                    _spriteBatch.Draw(_resourceManager.BackTexture, new Vector2(0 + 128 * i, 0 + 128 * j), sourceRec, Color.White, 0.0f, new Vector2(0), 0.5f, SpriteEffects.None, 0);
                }
            }

            // Draw main field
            var defaultRect = new Rectangle(0, 0, _cellSize, _cellSize);

            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    var rect = defaultRect;
                    rect.X = i * (_cellSize + _cellSpacing);
                    rect.Y = j * (_cellSize + _cellSpacing);
                    switch (field[i, j])
                    {
                        case 0:
                            _spriteBatch.Draw(_resourceManager.RedTexture, rect, Color.White);
                            break;
                        case 1:
                            _spriteBatch.Draw(_resourceManager.BlueTexture, rect, Color.White);
                            break;
                        case 2:
                            _spriteBatch.Draw(_resourceManager.GreenTexture, rect, Color.White);
                            break;
                    }
                }
            }

            defaultRect.X = field.GetLength(0) / 2 * (_cellSize + _cellSpacing);
            defaultRect.Y = field.GetLength(0) / 2 * (_cellSize + _cellSpacing);
            _spriteBatch.Draw(_resourceManager.WhiteTexture, defaultRect, Color.White);

            // Fill corners
            defaultRect.X = 0;
            defaultRect.Y = 0;
            _spriteBatch.Draw(_resourceManager.WhiteTexture, defaultRect, Color.White);

            defaultRect.X = 0;
            defaultRect.Y = (field.GetLength(0) - 1) * (_cellSize + _cellSpacing);
            _spriteBatch.Draw(_resourceManager.WhiteTexture, defaultRect, Color.White);

            defaultRect.X = (field.GetLength(0) - 1) * (_cellSize + _cellSpacing);
            defaultRect.Y = 0;
            _spriteBatch.Draw(_resourceManager.WhiteTexture, defaultRect, Color.White);

            defaultRect.X = (field.GetLength(0) - 1) * (_cellSize + _cellSpacing);
            defaultRect.Y = (field.GetLength(0) - 1) * (_cellSize + _cellSpacing);
            _spriteBatch.Draw(_resourceManager.WhiteTexture, defaultRect, Color.White);
        }

        private CellModel BuildCell(int i, int j, CustomColor color) => new CellModel
        {
            Position = new Position
            {
                FieldX = i,
                FieldY = j,
                X = i * (_cellSize + _cellSpacing),
                Y = j * (_cellSize + _cellSpacing)
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

        public void HighlightPossibleCells(int X, int Y, int[] rolledDice, HighlightedCells highlightedCells)
        {
            var subDice = Math.Abs(rolledDice[0] - rolledDice[1]);
            var sumDice = rolledDice[0] + rolledDice[1];

            if(highlightedCells == HighlightedCells.Both)
            {
                HighlightCells(X, Y, subDice, _resourceManager.SubHighlightTexture);
                HighlightCells(X, Y, sumDice, _resourceManager.SumHighlightTexture);
            }
            else if(highlightedCells == HighlightedCells.Sub)
                HighlightCells(X, Y, subDice, _resourceManager.SubHighlightTexture);
            else if (highlightedCells == HighlightedCells.Sum)
                HighlightCells(X, Y, sumDice, _resourceManager.SumHighlightTexture);

        }

        public void HighlightCells(int X, int Y, int stepModifier, Texture2D texture)
        {
            var defaultRec = new Rectangle(0, 0, _cellSize / 2, _cellSize / 2);

            //Down
            HighlighCell(X + stepModifier, Y, defaultRec, texture);
            //Up
            HighlighCell(X - stepModifier, Y, defaultRec, texture);
            //Right
            HighlighCell(X, Y + stepModifier, defaultRec, texture);
            //Left
            HighlighCell(X, Y - stepModifier, defaultRec, texture);
        }

        private void HighlighCell(int X, int Y, Rectangle rec, Texture2D texture)
        {
            if(X < _fieldSize && Y < _fieldSize)
            {
                rec.X = X * (_cellSize + _cellSpacing) + _cellSize / 4;
                rec.Y = Y * (_cellSize + _cellSpacing) + _cellSize / 4;
                _spriteBatch.Draw(texture, rec, Color.White);
            }
        }
    }
}
