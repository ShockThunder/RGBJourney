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
        public List<Cell> Cells { get; set; }
        public List<Cell> WinCells { get; set; }
    }
}
