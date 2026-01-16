using System.Runtime.InteropServices;

namespace ConsoleGameProject
{
    class Game
    {
        // Field (Member Variables)
        private Player player;
        private List<Stage> stages;
        private int current;    // 현재 스테이지의 index

        // Constructor
        public Game()
        {
            current = 0;
            player = new Player();
            stages = new List<Stage>();
            LoadStages();
        }
        // Property

        // Method
        [DllImport("msvcrt.dll")]
        static extern int _getch();  //c언어 함수 가져옴
        private void LoadStages()
        {
            // 스테이지 데이터를 담은 파일 경로
            string Path = "../../../Assets/map/";

            string line;
            try
            {
                // Metadata 파일 열기
                StreamReader sr = new StreamReader(Path + "Meta.txt");

                // 첫 줄 읽기
                line = sr.ReadLine();

                string[] words = line.Split(' ');
                if (words.Length != 2) throw new Exception("Meta 파일 형식이 잘못되었습니다.");
                if (words[0] != "Total_number_of_stages") throw new Exception("Meta 파일 형식이 잘못되었습니다.");
                // 스테이지 개수
                int num = int.Parse(words[1]);
                string[] fileNames = new string[num];

                // 맵 파일 이름 읽기
                for (int i = 0; i < num; i++)
                {
                    line = sr.ReadLine();
                    if (line == null) throw new Exception("Meta 파일 형식이 잘못되었습니다.");

                    fileNames[i] = line;
                }

                // 파일 닫기
                sr.Close();

                // Stage 생성
                for (int i = 0; i < num; i++)
                {
                    stages.Add(new Stage(Path + fileNames[i]));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                Console.WriteLine($"\"{Path + "Meta.txt"}\"를 불러오는데 실패했습니다. 게임을 종료합니다.");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }
        public void Start()
        {
            int width = 80;
            int height = 25;
            
            stages[current].Render();
            player.Loc = stages[current].StartLoc;
            player.Render();

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    KeyControl();
                    if (CheckClear()) break;
                }
            }
            End();
        }
        private void KeyControl()
        {
            int pressKey;

            pressKey = _getch();
            if (pressKey == 0 || pressKey == 224) pressKey = _getch();

            switch (pressKey)
            {
                case 72:    //위쪽 방향키
                    ArrowUpHandler();
                    break;
                case 75:    //왼쪽 방향키
                    ArrowLeftHandler();
                    break;
                case 77:    //오른쪽 방향키
                    ArrowRightHandler();
                    break;
                case 80:    //아래 방향키
                    ArrowDownHandler();
                    break;
                case 'z':   // 잡기
                case 'Z':
                    Grab();
                    break;
                case 'x':   // pocket watch 사용
                case 'X':
                    TimeReverse();
                    break;
            }
        }
        private void ArrowUpHandler()
        {
            (int oldX, int oldY) = player.Loc;
            (int newX, int newY) = player.Loc;

            player.LookUP();
            if (CanMoveTo(Direction.up))
            {
                player.MoveUP();
                (newX, newY) = player.Loc;
                if (player.GrabState)
                {
                    if (player.Direction == Direction.up)
                        stages[current].MoveBox(oldX, oldY - 1, Direction.up);
                    else
                        stages[current].MoveBox(oldX, oldY + 1, Direction.up);
                }
            }
            RenderOnPlayerMove(oldX, oldY, newX, newY);
        }
        private void ArrowDownHandler()
        {
            (int oldX, int oldY) = player.Loc;
            (int newX, int newY) = player.Loc;

            player.LookDown();
            if (CanMoveTo(Direction.down))
            {
                player.MoveDown();
                (newX, newY) = player.Loc;
                if (player.GrabState)
                {
                    if (player.Direction == Direction.up)
                        stages[current].MoveBox(oldX, oldY - 1, Direction.down);
                    else
                        stages[current].MoveBox(oldX, oldY + 1, Direction.down);
                }
            }
            RenderOnPlayerMove(oldX, oldY, newX, newY);
        }
        private void ArrowLeftHandler()
        {
            (int oldX, int oldY) = player.Loc;
            (int newX, int newY) = player.Loc;

            player.LookLeft();
            if (CanMoveTo(Direction.left))
            {
                player.MoveLeft();
                (newX, newY) = player.Loc;
                if (player.GrabState)
                {
                    if (player.Direction == Direction.left)
                        stages[current].MoveBox(oldX - 1, oldY, Direction.left);
                    else
                        stages[current].MoveBox(oldX + 1, oldY, Direction.left);
                }
            }
            RenderOnPlayerMove(oldX, oldY, newX, newY);
        }
        private void ArrowRightHandler()
        {
            (int oldX, int oldY) = player.Loc;
            (int newX, int newY) = player.Loc;

            player.LookRight();
            if (CanMoveTo(Direction.right))
            {
                player.MoveRight();
                (newX, newY) = player.Loc;
                if (player.GrabState)
                {
                    if (player.Direction == Direction.left)
                        stages[current].MoveBox(oldX - 1, oldY, Direction.right);
                    else
                        stages[current].MoveBox(oldX + 1, oldY, Direction.right);
                }
            }
            RenderOnPlayerMove(oldX, oldY, newX, newY);
        }
        private bool CanMoveTo(Direction direction)
        {
            (int x, int y) = player.Loc;

            try
            {
                switch (direction)
                {
                    case Direction.up:
                        if (player.GrabState && player.Direction == Direction.up && stages[current].GetTile(x, y - 2) < 2) return true;
                        if (player.GrabState && player.Direction == Direction.down && stages[current].GetTile(x, y - 1) < 4 && stages[current].GetTile(x, y) < 2) return true;
                        if (!player.GrabState && stages[current].GetTile(x, y - 1) < 4) return true;
                        break;
                    case Direction.down:
                        if (player.GrabState && player.Direction == Direction.down && stages[current].GetTile(x, y + 2) < 2) return true;
                        if (player.GrabState && player.Direction == Direction.up && stages[current].GetTile(x, y + 1) < 4 && stages[current].GetTile(x, y) < 2) return true;
                        if (!player.GrabState && stages[current].GetTile(x, y + 1) < 4) return true;
                        break;
                    case Direction.left:
                        if (player.GrabState && player.Direction == Direction.left && stages[current].GetTile(x - 2, y) < 2) return true;
                        if (player.GrabState && player.Direction == Direction.right && stages[current].GetTile(x - 1, y) < 4 && stages[current].GetTile(x, y) < 2) return true;
                        if (!player.GrabState && stages[current].GetTile(x - 1, y) < 4) return true;
                        break;
                    case Direction.right:
                        if (player.GrabState && player.Direction == Direction.right && stages[current].GetTile(x + 2, y) < 2) return true;
                        if (player.GrabState && player.Direction == Direction.left && stages[current].GetTile(x + 1, y) < 4 && stages[current].GetTile(x, y) < 2) return true;
                        if (!player.GrabState && stages[current].GetTile(x + 1, y) < 4) return true;
                        break;
                }
            }
            catch (IndexOutOfRangeException)    // 맵 밖으로 나가는 경우
            {
                return false;
            }

            return false;
        }
        private void RenderOnPlayerMove(int oldX, int oldY, int newX, int newY)
        {
            Renderer.DrawTile(oldX, oldY, stages[current].GetTile(oldX, oldY));
            Renderer.DrawPlayer(newX, newY, player.Direction, player.GrabState);
        }
        private void Grab()
        {
            if (player.GrabState)
            {
                player.GrabState = false;
            }
            else
            {
                try
                {
                    (int x, int y) = player.Loc;

                    switch (player.Direction)
                    {
                        case Direction.up:
                            if (stages[current].GetTile(x, y - 1) > 5)
                            {
                                player.GrabState = true;
                            }
                            break;
                        case Direction.down:
                            if (stages[current].GetTile(x, y + 1) > 5)
                            {
                                player.GrabState = true;
                            }
                            break;
                        case Direction.left:
                            if (stages[current].GetTile(x - 1, y) > 5)
                            {
                                player.GrabState = true;
                            }
                            break;
                        case Direction.right:
                            if (stages[current].GetTile(x + 1, y) > 5)
                            {
                                player.GrabState = true;
                            }
                            break;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    player.GrabState = false;
                }
            }
            player.Render();
        }
        private void TimeReverse()
        {

        }
        private bool CheckClear()   // 게임 완전히 클리어 시 true 리턴, 그 외의 경우 false 리턴. 스테이지 클리어 시 다음 스테이지 진입
        {
            (int x, int y) = player.Loc;
            if (stages[current].GetTile(x, y) == 2)
            {
                current++;
                if (current >= stages.Count) return true;

                stages[current].Render();
                player.Loc = stages[current].StartLoc;
                player.GrabState = false;
                player.Render();
            }
            return false;
        }
        private void End()
        {
            // 게임 엔딩
            Console.WriteLine("게임 클리어!");
        }

    }

}
