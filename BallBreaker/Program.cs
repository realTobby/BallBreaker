using Raylib_cs;
using System;
using System.Collections.Generic;

namespace BallBreaker
{
    public enum GameState
    {
        Ready,
        Playing
    }

    class Program
    {
        static GameState currentGameState = GameState.Ready;

        static Position player = new Position(0,0);
        static Ball ball = new Ball();
        static Random rnd = new Random();

        static int WINDOW_WIDTH = 300;
        static int WINDOW_HEIGHT = 300;

        static List<Position> collisions = new List<Position>();



        static void Main(string[] args)
        {
            player.X = WINDOW_WIDTH/2;
            player.Y = WINDOW_HEIGHT-64;

            ball.BallPosition.X = WINDOW_WIDTH/2;
            ball.BallPosition.Y = WINDOW_HEIGHT - 64;
   


            Raylib.InitWindow(WINDOW_WIDTH, WINDOW_HEIGHT, "BallBreaker");
            while (!Raylib.WindowShouldClose())
            {

                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.WHITE);

                HandlePlayer();
                HandleBall();

                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }

        static void HandleBall()
        {
            if(currentGameState == GameState.Ready)
            {
                ball.BallPosition.X = player.X;
                ball.BallPosition.Y = player.Y;
            }

            if(currentGameState == GameState.Playing)
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

                if (ball.BallPosition.X > WINDOW_WIDTH)
                {
                    // COLLIDE WITH RIGHT BORDER
                    ball.BallVelocity.X *= -1;
                }

                if (ball.BallPosition.Y > WINDOW_HEIGHT)
                {
                    // COLLIDE WITH BOTTOM BORDER
                    currentGameState = GameState.Ready;
                    ResetBall();
                }

                // 2: Player
                // 3: Blocks
               

            }
            

            Raylib.DrawCircle(Convert.ToInt32(ball.BallPosition.X), Convert.ToInt32(ball.BallPosition.Y), 10, Color.BLACK);
        }

        static void HandlePlayer()
        {

            if(Raylib.IsKeyDown(KeyboardKey.KEY_LEFT) && player.X > 32)
            {
                player.X -= 150f * Raylib.GetFrameTime();
            }

            if(Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT) && player.X < WINDOW_WIDTH-32)
            {
                player.X += 150f * Raylib.GetFrameTime();
            }

            if(Raylib.IsKeyDown(KeyboardKey.KEY_SPACE))
            {
                ResetBall();
                if(currentGameState == GameState.Ready)
                {
                    ball.BallVelocity.X = rnd.Next(-200,200);
                    ball.BallVelocity.Y = rnd.Next(-200,-100);

                    currentGameState = GameState.Playing;
                }
            }


            Raylib.DrawRectangle(Convert.ToInt32(player.X-32), Convert.ToInt32(player.Y-8), 64, 16, Color.RED);
        }

        static void ResetBall()
        {
            currentGameState = GameState.Ready;
            ball.BallPosition.X = player.X;
            ball.BallPosition.Y = player.Y;
            ball.BallVelocity.X = rnd.Next(-200, 200);
            ball.BallVelocity.Y = rnd.Next(-200, -100);
        }
    }
}
