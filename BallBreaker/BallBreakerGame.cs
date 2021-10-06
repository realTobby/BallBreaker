using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BallBreaker
{
    public enum GameState
    {
        Ready,
        Playing
    }

    public class BallBreakerGame
    {
        private int gameWidth = 0;
        private int gameHeight = 0;

        private Player player;
        private Ball ball;

        private Random random = new Random();

        GameState currentGameState = GameState.Ready;

        private List<Brick> brickCollection = new List<Brick>();

        private void SpawnBricks()
        {
            int brickCount = 10;

            for(int y = 0; y < gameHeight/2; y+=18)
            {
                for(int x = 8; x < gameWidth-32; x+=36)
                {
                    Brick b = new Brick(x, y);
                    brickCollection.Add(b);
                }
            }
        }

        public void InitGame(int w, int h)
        {
            gameWidth = w;
            gameHeight = h;

            SpawnBricks();


            player = new Player(gameWidth / 2, gameHeight - 64);

            player.ColliderWidth = 64;
            player.ColliderHeight = 16;

            player.PLAYER_POSITION.X = player.PLAYER_POSITION.X - player.ColliderWidth / 2;

            ball = new Ball();
            ResetBall();
        }

        public void HandleBall()
        {
            if (currentGameState == GameState.Ready)
            {
                ball.BallPosition.X = player.PLAYER_POSITION.X + player.ColliderWidth/2;
                ball.BallPosition.Y = player.PLAYER_POSITION.Y - 32;
            }

            if (currentGameState == GameState.Playing)
            {
                ball.UpdateBall();

                // collision can happen between:
                // 1: Window Border
                if (ball.BallPosition.X < 2)
                {
                    // COLLIDE WITH LEFT BORDER
                    ball.BallVelocity.X *= -1;
                }

                if (ball.BallPosition.Y < 2)
                {
                    // COLLIDE WITH TOP BORDER
                    ball.BallVelocity.Y *= -1;
                }

                if (ball.BallPosition.X > gameWidth-10)
                {
                    // COLLIDE WITH RIGHT BORDER
                    ball.BallVelocity.X *= -1;
                }

                if (ball.BallPosition.Y > gameHeight)
                {
                    // COLLIDE WITH BOTTOM BORDER
                    currentGameState = GameState.Ready;
                    ResetBall();
                }

                // 2: Player
                if(player.IsColliding(Convert.ToInt32(player.PLAYER_POSITION.X), Convert.ToInt32(player.PLAYER_POSITION.Y), Convert.ToInt32(ball.BallPosition.X), Convert.ToInt32(ball.BallPosition.Y)))
                {
                    ball.BallVelocity.Y *= -1;
                }

                // 3: Blocks


            }


            Raylib.DrawCircle(Convert.ToInt32(ball.BallPosition.X), Convert.ToInt32(ball.BallPosition.Y), 10, Color.BLACK);
        }

        public void HandleBricks()
        {
            foreach(Brick b in brickCollection.ToList())
            {
                Raylib.DrawRectangle(Convert.ToInt32(b.Position.X), Convert.ToInt32(b.Position.Y), b.ColliderWidth, b.ColliderHeight, Color.GREEN);
                if(b.IsColliding(Convert.ToInt32(b.Position.X), Convert.ToInt32(b.Position.Y),  Convert.ToInt32(ball.BallPosition.X), Convert.ToInt32(ball.BallPosition.Y)))
                {
                    brickCollection.Remove(b);
                    ball.BallVelocity.X *= -1;
                    ball.BallVelocity.Y *= -1;
                }
            }
        }

        public void HandlePlayer()
        {

            if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT) && player.PLAYER_POSITION.X > 32)
            {
                player.PLAYER_POSITION.X -= 150f * Raylib.GetFrameTime();
            }

            if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT) && player.PLAYER_POSITION.X < gameWidth - 32)
            {
                player.PLAYER_POSITION.X += 150f * Raylib.GetFrameTime();
            }

            if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE))
            {
                
                if (currentGameState == GameState.Ready)
                {
                    ball.BallVelocity.X = random.Next(-200, 200);
                    ball.BallVelocity.Y = random.Next(-200, -100);

                    currentGameState = GameState.Playing;
                }
                else
                {
                    currentGameState = GameState.Ready;
                    ResetBall();
                }
            }


            Raylib.DrawRectangle(Convert.ToInt32(player.PLAYER_POSITION.X), Convert.ToInt32(player.PLAYER_POSITION.Y), player.ColliderWidth, player.ColliderHeight, Color.RED);
        }

        private void ResetBall()
        {
            ball.BallPosition.X = gameWidth / 2;
            ball.BallPosition.Y = player.PLAYER_POSITION.Y - 32;
            ball.BallVelocity.X = random.Next(-200, 200);
            ball.BallVelocity.Y = random.Next(-200, -100);
        }

    }

    
}
