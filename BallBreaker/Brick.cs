using System;
using System.Collections.Generic;
using System.Text;

namespace BallBreaker
{
    public class Brick
    {
        private int MaxHP = 2;
        private int CurrentHP = 2;

        public Position Position;

        public int Width = 32;
        public int Height = 8;

        public Brick(int x, int y)
        {
            Position = new Position(x, y);
        }

        public Brick(Position pos)
        {
            Position = new Position(pos);
        }

        public bool IsColliding(int x, int y)
        {
            if(x > Position.X && x < Position.X + Width 
                && y > Position.Y && y < Position.Y + Height)
            { 
                return true;
            }
            return false;
        }

    }
}
