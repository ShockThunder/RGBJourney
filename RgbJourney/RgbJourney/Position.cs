using System;
using System.Collections.Generic;
using System.Text;

namespace RgbJourney
{
    public class Position
    {
        public Position()
        {

        }

        public Position(Position position)
        {
            this.X = position.X;
            this.Y = position.Y;
            this.FieldX = position.FieldX;
            this.FieldY = position.FieldY;
        }

        public Position(int X, int Y, int FieldX, int FieldY)
        {
            this.X = X;
            this.Y = Y;
            this.FieldX = FieldX;
            this.FieldY = FieldY;
        }
        public int X { get; set; }
        public int Y { get; set; }
        public int FieldX { get; set; }
        public int FieldY { get; set; }
    }
}
