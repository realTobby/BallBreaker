

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
            if (targetX >= colliderX && targetX <= colliderX + ColliderWidth
                && targetY >= colliderY && targetY <= colliderY + ColliderHeight)
            {

                if(targetX >= colliderX && targetX >= colliderX+ColliderWidth)
                {
                    System.Console.WriteLine("Collide Right!");
                    return CollisionDirection.Right;
                }

                if(targetX <= colliderX)
                {
                    System.Console.WriteLine("Collide Left!");
                    return CollisionDirection.Left;
                }

                if(targetY > colliderY && targetY >= colliderY+ColliderHeight)
                {
                    System.Console.WriteLine("Collide Down!");
                    return CollisionDirection.Down;
                }

                if(targetY <= colliderY)
                {
                    System.Console.WriteLine("Collide Right!");
                    return CollisionDirection.Top;
                }

                return CollisionDirection.Any;
            }
            return CollisionDirection.NoCollision;
        }

    }
}
