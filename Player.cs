namespace ConsoleGameProject
{
    class Player
    {
        // Field (Member Variables)
        private int x;  // x좌표
        private int y;  // y좌표
        private Direction direction;    // 플레이어 현재 방향
        // Constructor
        public Player()
        {
            x = y = 1;
            direction = Direction.up;
        }
        public Player(int x, int y, Direction direction = Direction.up)
        {
            this.x = x;
            this.y = y;
            this.direction = direction;
        }
        // Property
        public (int x, int y) Loc
        {
            get { return (x, y); }
            set
            {
                x = value.x;
                y = value.y;
            }
        }
        // Method
        public void MoveUP() { y--; }
        public void MoveDown() { y++; }
        public void MoveLeft() { x--; }
        public void MoveRight() { x++; }
        public void LookUP() { direction = Direction.up; }
        public void LookDown() { direction = Direction.down; }
        public void LookLeft() { direction = Direction.left; }
        public void LookRight() { direction = Direction.right; }
    }

}