using System;
using System.Collections.Generic;
using System.Text;

namespace BallBreaker
{
    public class Position
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Position(Position pos)
        {
            X = pos.X;
            Y = pos.Y;
        }

    }
}
