using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RgbJourney.Enums;
using RgbJourney.Models;
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

        public Position OldPlayerPosition = new Position();
        public List<Cell> cells = new List<Cell>();
        public List<Cell> winCells = new List<Cell>();


        public FieldManager(int cellSize, int cellSpacing,
            SpriteBatch spriteBatch, ResourceManager resourceManager, int fieldSize)
        {
            _cellSize = cellSize;
            _cellSpacing = cellSpacing;
            _spriteBatch = spriteBatch;
            _resourceManager = resourceManager;
            _fieldSize = fieldSize;
        }

        public void DrawField(Field field)
        {
            //// Draw main field
            //var cellSize = field.CellSize;

            //var defaultRect = new Rectangle(0, 0, cellSize, cellSize);

            //foreach (var cell in field.Cells)
            //{
            //    var rect = defaultRect;
            //    rect.X = cell.Position.X;
            //    rect.Y = cell.Position.Y;

            //    switch (cell.Color)
            //    {
            //        case CustomColor.Red:
            //            _spriteBatch.Draw(_resourceManager.RedTexture, rect, Color.White);
            //            break;
            //        case CustomColor.Blue:
            //            _spriteBatch.Draw(_resourceManager.BlueTexture, rect, Color.White);
            //            break;
            //        case CustomColor.Green:
            //            _spriteBatch.Draw(_resourceManager.GreenTexture, rect, Color.White);
            //            break;
            //    }
            //}

            //foreach(var winCell in field.WinCells)
            //{
            //    defaultRect.X = winCell.Position.X;
            //    defaultRect.Y = winCell.Position.Y;
            //    _spriteBatch.Draw(_resourceManager.WhiteTexture, defaultRect, Color.White);
            //}

            //defaultRect.X = field.GetLength(0) / 2 * (_cellSize + _cellSpacing);
            //defaultRect.Y = field.GetLength(0) / 2 * (_cellSize + _cellSpacing);
            //_spriteBatch.Draw(_resourceManager.WhiteTexture, defaultRect, Color.White);            
        }

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

        public void HighlightPossibleCells(int X, int Y, int PLAYERSTEP, HighlightedCells highlightedCells)
        {

            if(highlightedCells == HighlightedCells.Both)
            {
                HighlightCells(X, Y, PLAYERSTEP, _resourceManager.SubHighlightTexture);
                HighlightCells(X, Y, PLAYERSTEP, _resourceManager.SumHighlightTexture);
            }
            else if(highlightedCells == HighlightedCells.Sub)
                HighlightCells(X, Y, PLAYERSTEP, _resourceManager.SubHighlightTexture);
            else if (highlightedCells == HighlightedCells.Sum)
                HighlightCells(X, Y, PLAYERSTEP, _resourceManager.SumHighlightTexture);

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
