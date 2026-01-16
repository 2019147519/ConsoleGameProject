namespace ConsoleGameProject
{
    class Stage
    {
        // Field (Member Variables)
        private int width;  // 맵 가로 길이
        private int height; // 맵 세로 길이
        private int startX; // 시작 위치 x좌표
        private int startY; // 시작 위치 y좌표
        private int[] map;  // 2차원 맵 데이터를 1차원 배열로 저장
        // Constructor
        public Stage(string filename)
        {
            LoadMapFromFile(filename);
            DoorCheck();
        }
        // Property
        public (int x, int y) StartLoc
        {
            get { return (startX, startY); }
        }

        // Method
        public int GetTile(int x, int y)
        {
            return map[y * width + x];
        }
        public void MoveBox(int x, int y, Direction direction)  // box 이동하고 렌더링까지 처리
        {
            if (map[y * width + x] < 6)
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("버그 발생! 게임이 비정상적으로 동작할 수 있습니다.");
                return;
            }

            map[y * width + x] -= 6;
            if (map[y * width + x] == 1)
                DoorCheck();
            Renderer.DrawTile(x, y, map[y * width + x]);

            switch (direction)
            {
                case Direction.up:
                    y--;
                    break;
                case Direction.down:
                    y++;
                    break;
                case Direction.left:
                    x--;
                    break;
                case Direction.right:
                    x++;
                    break;
            }

            if (map[y * width + x] == 0)
            {
                map[y * width + x] += 6;
            }
            if (map[y * width + x] == 1)
            {
                map[y * width + x] += 6;
                DoorCheck();
            }

            Renderer.DrawTile(x, y, map[y * width + x]);
        }
        private void DoorCheck()
        {
            bool everyButtonPushed = true;

            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    if (map[j * width + i] == 1)
                    {
                        everyButtonPushed = false;
                    }
                }
            }

            if (everyButtonPushed)
            {
                for (int j = 0; j < height; j++)
                {
                    for (int i = 0; i < width; i++)
                    {
                        if (map[j * width + i] == 5)
                        {
                            map[j * width + i] = 3;
                            Renderer.DrawTile(i, j, 3);
                        }
                    }
                }
            }
            else
            {
                for (int j = 0; j < height; j++)
                {
                    for (int i = 0; i < width; i++)
                    {
                        if (map[j * width + i] == 3)
                        {
                            map[j * width + i] = 5;
                            Renderer.DrawTile(i, j, 5);
                        }
                    }
                }
            }
        }
        private void LoadMapFromFile(string filename)
        {
            string line;
            try
            {
                StreamReader sr = new StreamReader(filename);

                // 첫 줄 width height 읽기
                line = sr.ReadLine();

                string[] words = line.Split(' ');
                if (words.Length != 2) throw new Exception("맵 파일 형식이 잘못되었습니다.");
                width = int.Parse(words[0]);
                height = int.Parse(words[1]);

                map = new int[width * height];

                // 시작 위치 읽기
                line = sr.ReadLine();

                words = line.Split(' ');
                if (words.Length != 2) throw new Exception("맵 파일 형식이 잘못되었습니다.");
                startX = int.Parse(words[0]);
                startY = int.Parse(words[1]);
                if (startX >= width || startY >= height) throw new Exception("맵 파일에 기록된 시작 위치가 맵을 벗어났습니다.");

                // 맵 정보 읽기
                int count = 0;
                line = sr.ReadLine();
                while (line != null)
                {
                    words = line.Split(' ');
                    if (words.Length != width) throw new Exception("맵 파일 형식이 잘못되었습니다.");
                    for (int i = 0; i < width; i++)
                    {
                        map[count * width + i] = int.Parse(words[i]);
                    }
                    count++;
                    line = sr.ReadLine();
                }
                if (count != height) throw new Exception("맵 파일 형식이 잘못되었습니다.");
                if (map[startY * width + startX] != 0) throw new Exception("맵 파일에 기록된 시작 위치가 ground가 아닙니다.");

                // 파일 닫기
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                Console.WriteLine($"\"{filename}\"을 불러오는데 실패했습니다. 게임을 종료합니다.");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }
        public void Render()
        {
            Console.Clear();
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    Renderer.DrawTile(i, j, map[j * width + i]);
                }
            }
        }

    }
}