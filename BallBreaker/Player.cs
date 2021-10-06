

namespace BallBreaker
{
    public class Player : RectangleCollider
    {
        public Position PLAYER_POSITION { get; set; }

        public Player(int x, int y)
        {
            PLAYER_POSITION = new Position(x, y);
        }

        public Player(Position pos)
        {
            PLAYER_POSITION = new Position(pos);
        }

    }
}
