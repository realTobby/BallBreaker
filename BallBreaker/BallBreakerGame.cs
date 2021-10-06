using Raylib_cs;
using System;
using System.Collections.Generic;
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


        public void InitGame(int w, int h)
        {
            gameWidth = w;
            gameHeight = h;

            player = new Player(gameWidth / 2, gameHeight - 64);

            player.playerWidth = 64;
            player.playerHeight = 16;

            ball = new Ball();
            ResetBall();
            
        }

        public void HandleBall()
        {
            if (currentGameState == GameState.Ready)
            {
                ball.BallPosition.X = player.PLAYER_POSITION.X;
                ball.BallPosition.Y = player.PLAYER_POSITION.Y;
            }

            if (currentGameState == GameState.Playing)
            {
                ball.UpdateBall();
                //Raylib.DrawLine(Convert.ToInt32(ball.BallPosition.X), Convert.ToInt32(ball.BallPosition.Y), Convert.ToInt32(player.X), Convert.ToInt32(player.Y), Color.GOLD);
                // check if collision is happening now
                // collision can happen between:
                // 1: Window Border
                if (ball.BallPosition.X < 0)
                {
                    // COLLIDE WITH LEFT BORDER
                    ball.BallVelocity.X *= -1;
                }

                if (ball.BallPosition.Y < 0)
                {
                    // COLLIDE WITH TOP BORDER
                    ball.BallVelocity.Y *= -1;
                }

                if (ball.BallPosition.X > gameWidth)
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

                if(player.IsColliding(Convert.ToInt32(ball.BallPosition.X), Convert.ToInt32(ball.BallPosition.Y)))
                {
                    ball.BallVelocity.Y *= -1;
                }


                // 3: Blocks


            }


            Raylib.DrawCircle(Convert.ToInt32(ball.BallPosition.X), Convert.ToInt32(ball.BallPosition.Y), 10, Color.BLACK);
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
                ResetBall();
                currentGameState = GameState.Ready;
                if (currentGameState == GameState.Ready)
                {
                    ball.BallVelocity.X = random.Next(-200, 200);
                    ball.BallVelocity.Y = random.Next(-200, -100);

                    currentGameState = GameState.Playing;
                }
            }


            Raylib.DrawRectangle(Convert.ToInt32(player.PLAYER_POSITION.X - player.playerWidth/2), Convert.ToInt32(player.PLAYER_POSITION.Y - player.playerHeight/2), player.playerWidth, player.playerHeight, Color.RED);
        }

        private void ResetBall()
        {
            

            ball.BallPosition.X = gameWidth / 2;
            ball.BallPosition.Y = gameHeight - 64;
            ball.BallVelocity.X = random.Next(-200, 200);
            ball.BallVelocity.Y = random.Next(-200, -100);
        }

    }

    
}
