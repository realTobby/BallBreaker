using Raylib_cs;


namespace BallBreaker
{
    public class Ball
    {
        public Position BallPosition { get; set; } = new Position(0,0);
        public Position BallVelocity { get; set; } = new Position(0,0);

        public void UpdateBall()
        {
            BallPosition.X += Raylib.GetFrameTime() * BallVelocity.X;
            BallPosition.Y += Raylib.GetFrameTime() * BallVelocity.Y;
        }

    }
}
