namespace ZeroDay.Entities
{
    public class Player(float x, float y)
    {
        public float X { get; private set; } = x;
        public float Y { get; private set; } = y;

        private float _speed { get; set; } = 500f;

        // Animation State
        public float FrameTimer { get; private set; }
        public int CurrentFrame { get; private set; }
        public int FacingRow { get; private set; } // 0 = Down, 1 = Left, 2 = Right, 3 = Up
        public bool IsMoving { get; private set; }

        public void Update(float dirX, float dirY, float deltaTime)
        {
            IsMoving = (dirX != 0 || dirY != 0);

            if (IsMoving)
            {
                X += dirX * _speed * deltaTime;
                Y += dirY * _speed * deltaTime;

                // Update Animation Frame
                FrameTimer += deltaTime;
                if (FrameTimer >= 0.1f) // Change frame every 0.1 seconds
                {
                    CurrentFrame = (CurrentFrame + 1) % 4; // Assuming 4 frames per row
                    FrameTimer = 0;
                }

                // Set Row based on direction
                if (dirY > 0) FacingRow = 0; // Down
                else if (dirX < 0) FacingRow = 2; // Left
                else if (dirX > 0) FacingRow = 3; // Right
                else if (dirY < 0) FacingRow = 1; // Up
            }
            else
            {
                CurrentFrame = 0; // Idle frame
            }
        }
    }
}
