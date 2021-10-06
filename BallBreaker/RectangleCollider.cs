using System;
using System.Collections.Generic;
using System.Text;

namespace BallBreaker
{
    public class RectangleCollider
    {
        internal int ColliderWidth = 0;
        internal int ColliderHeight = 0;

        public bool IsColliding(int colliderX, int colliderY, int targetX, int targetY)
        {
            if (targetX > colliderX && targetX < colliderX + ColliderWidth
                && targetY > colliderY && targetY < colliderY + ColliderHeight)
            {
                return true;
            }
            return false;
        }

    }
}
