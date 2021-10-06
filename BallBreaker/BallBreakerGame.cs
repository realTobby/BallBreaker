using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;

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
                CollisionDirection cd = player.IsColliding(Convert.ToInt32(player.PLAYER_POSITION.X), Convert.ToInt32(player.PLAYER_POSITION.Y), Convert.ToInt32(ball.BallPosition.X), Convert.ToInt32(ball.BallPosition.Y));
                HandleBallVelocityAfterCollision(cd);

                // ALSO DETERMINE WHERE THE BALL HIT AND ADD OR REDUCE VELOCITY


            }


            Raylib.DrawCircle(Convert.ToInt32(ball.BallPosition.X), Convert.ToInt32(ball.BallPosition.Y), 10, Color.BLACK);
        }

        private void HandleBallVelocityAfterCollision(CollisionDirection cd)
        {
            switch (cd)
            {
                case CollisionDirection.Any:
                    ball.BallVelocity.X *= -1;
                    ball.BallVelocity.Y *= -1;
                    break;
                case CollisionDirection.Left:
                    ball.BallVelocity.X *= -1;
                    break;
                case CollisionDirection.Right:
                    ball.BallVelocity.X *= -1;
                    break;
                case CollisionDirection.Top:
                    ball.BallVelocity.Y *= -1;
                    break;
                case CollisionDirection.Down:
                    ball.BallVelocity.Y *= -1;
                    break;
            }
        }

        public void HandleBricks()
        {
            foreach(Brick b in brickCollection.ToList())
            {
                Raylib.DrawRectangle(Convert.ToInt32(b.Position.X), Convert.ToInt32(b.Position.Y), b.ColliderWidth, b.ColliderHeight, Color.GREEN);
                CollisionDirection cd = b.IsColliding(Convert.ToInt32(b.Position.X), Convert.ToInt32(b.Position.Y), Convert.ToInt32(ball.BallPosition.X), Convert.ToInt32(ball.BallPosition.Y));
                
                if(cd != CollisionDirection.NoCollision)
                {
                    brickCollection.Remove(b);
                }

                HandleBallVelocityAfterCollision(cd);
                
            }

            if(brickCollection.Count <= 0)
            {
                InitGame(gameWidth, gameHeight);
            }

        }

        public void HandlePlayer()
        {

            if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT) && player.PLAYER_POSITION.X > 0)
            {
                player.PLAYER_POSITION.X -= 155f * Raylib.GetFrameTime();
            }

            if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT) && player.PLAYER_POSITION.X < gameWidth - 64)
            {
                player.PLAYER_POSITION.X += 155f * Raylib.GetFrameTime();
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
