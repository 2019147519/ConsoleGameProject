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
            GrabState = false;
            HavePocketWatch = false;
        }
        public Player(int x, int y, Direction direction = Direction.up)
        {
            this.x = x;
            this.y = y;
            this.direction = direction;
            GrabState = false;
            HavePocketWatch = false;
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
        public bool GrabState    // box를 붙잡고 있는지 유무
        {
            get;
            set;
        }
        public bool HavePocketWatch // pocket watch 아이템 보유 유무
        {
            get;
            set;
        }
        public Direction Direction
        {
            get { return direction; }
        }
        // Method
        public void MoveUP()
        {
            if (GrabState && (direction == Direction.left || direction == Direction.right)) return;
            y--;
        }
        public void MoveDown()
        {
            if (GrabState && (direction == Direction.left || direction == Direction.right)) return;
            y++;
        }
        public void MoveLeft()
        {
            if (GrabState && (direction == Direction.up || direction == Direction.down)) return;
            x--;
        }
        public void MoveRight()
        {
            if (GrabState && (direction == Direction.up || direction == Direction.down)) return;
            x++;
        }
        public void LookUP() { if (!GrabState) direction = Direction.up; }
        public void LookDown() { if (!GrabState) direction = Direction.down; }
        public void LookLeft() { if (!GrabState) direction = Direction.left; }
        public void LookRight() { if (!GrabState) direction = Direction.right; }
        public void Render()
        {
            Renderer.DrawPlayer(x, y, direction, GrabState);
        }

    }

}