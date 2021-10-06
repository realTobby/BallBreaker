using System;
using System.Collections.Generic;
using System.Text;

namespace BallBreaker
{
    public class Player
    {
        public int playerWidth = 0;
        public int playerHeight = 0;

        public Position PLAYER_POSITION { get; set; }

        public Player(int x, int y)
        {
            PLAYER_POSITION = new Position(x, y);
        }

        public Player(Position pos)
        {
            PLAYER_POSITION = new Position(pos);
        }

        public bool IsColliding(int x, int y)
        {
            if(x > PLAYER_POSITION.X-playerWidth/2 && x < PLAYER_POSITION.X + playerWidth/2
                && y > PLAYER_POSITION.Y - playerHeight/2 && y < PLAYER_POSITION.Y + playerHeight/2)
            {
                return true;
            }


            return false;
        }


    }
}
