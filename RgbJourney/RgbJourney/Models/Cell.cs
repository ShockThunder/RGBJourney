using RgbJourney.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgbJourney.Models
{
    public class Cell
    {
        public Position Position { get; set; } = new Position();
        public CustomColor Color { get; set; }
    }
}
