using RgbJourney.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgbJourney
{
    public class CellModel
    {
        public Position Position { get; set; } = new Position();
        public CustomColor Color { get; set; }
    }
}
