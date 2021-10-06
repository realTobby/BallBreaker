using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BallBreaker
{
   

    public class Program
    {
        static int WINDOW_WIDTH = 300;
        static int WINDOW_HEIGHT = 300;

        static BallBreakerGame bbg = new BallBreakerGame();

        static void Main(string[] args)
        {
            bbg.InitGame(WINDOW_WIDTH, WINDOW_HEIGHT);

            Raylib.InitWindow(WINDOW_WIDTH, WINDOW_HEIGHT, "BallBreaker");
            while (!Raylib.WindowShouldClose())
            {

                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.WHITE);

                bbg.HandlePlayer();
                bbg.HandleBall();
                bbg.HandleBricks();

                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }

        

       
    }
}
