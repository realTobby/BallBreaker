

namespace BallBreaker
{
    public enum CollisionDirection
    {
        NoCollision,
        Any,
        Top,
        Left,
        Right,
        Down
    }

    public class RectangleCollider
    {
        internal int ColliderWidth = 0;
        internal int ColliderHeight = 0;

        public CollisionDirection IsColliding(int colliderX, int colliderY, int targetX, int targetY)
        {
            if (targetX > colliderX && targetX < colliderX + ColliderWidth
                && targetY > colliderY && targetY < colliderY + ColliderHeight)
            {

                if(targetX > colliderX && targetX >= colliderX+ColliderWidth)
                {
                    return CollisionDirection.Right;
                }

                if(targetX <= colliderX)
                {
                    return CollisionDirection.Left;
                }

                if(targetY > colliderY && targetY >= colliderY+ColliderHeight)
                {
                    return CollisionDirection.Down;
                }

                if(targetY <= colliderY)
                {
                    return CollisionDirection.Top;
                }

                return CollisionDirection.Any;
            }
            return CollisionDirection.NoCollision;
        }

    }
}
