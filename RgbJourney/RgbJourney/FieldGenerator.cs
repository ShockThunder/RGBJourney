using RgbJourney.Enums;
using RgbJourney.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgbJourney
{
    /// <summary>
    /// Generate game field
    /// </summary>
    public class FieldGenerator
    {
        private readonly int _cellSize;
        private readonly int _cellSpacing;
        private readonly int _fieldSize;

        private readonly Random _random;

        public List<Cell> cells = new List<Cell>();
        public List<Cell> winCells = new List<Cell>();

        public FieldGenerator(int cellSize, int cellSpacing, int fieldSize, Random random)
        {
            _cellSize = cellSize;
            _cellSpacing = cellSpacing;
            _fieldSize = fieldSize;
            _random = random;
        }
        public Field GenerateField()
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

                winCells.Add(BuildCell(0, 0, CustomColor.White));
                winCells.Add(BuildCell(0, _fieldSize - 1, CustomColor.White));
                winCells.Add(BuildCell(_fieldSize - 1, 0, CustomColor.White));
                winCells.Add(BuildCell(_fieldSize - 1, _fieldSize - 1, CustomColor.White));
            }

            return new Field
            {
                Cells = cells,
                CellSize = _cellSize,
                CellSpacing = _cellSpacing,
                Size = _fieldSize,
                WinCells = winCells
            };
        }

        private Cell BuildCell(int i, int j, CustomColor color) => new Cell
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
    }
}
