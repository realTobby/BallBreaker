using System;
using System.Collections.Generic;
using System.Text;

namespace BallBreaker
{
    public class Brick : RectangleCollider
    {
        private int MaxHP = 2;
        private int CurrentHP = 2;

        public Position Position;

        public Brick()
        {
            base.ColliderWidth = 32;
            base.ColliderHeight = 8;
        }

        public Brick(int x, int y) : this()
        {
            Position = new Position(x, y);
        }
    }
}
