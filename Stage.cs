using System.IO;

namespace ConsoleGameProject
{
    class Stage
    {
        // Field (Member Variables)
        private int width;  // 맵 가로 길이
        private int height; // 맵 세로 길이
        private int[] map;  // 2차원 맵 데이터를 1차원 배열로 저장
        // Constructor
        public Stage(string filename)
        {
            LoadMapFromFile(filename);
        }
        // Property

        // Method
        public int GetTile(int x, int y)
        {
            return map[y * width + x];
        }
        public void LoadMapFromFile(string filename)
        {
            String line;
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

                // 파일 닫기
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                Console.WriteLine("맵을 불러오는데 실패했습니다. 게임을 종료합니다.");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }
        // public void show()
        // {
        //     Console.WriteLine($"width: {width}");
        //     Console.WriteLine($"height: {height}");
        //     for (int i = 0; i < height; i++)
        //     {
        //         for (int j = 0; j < width; j++)
        //         {
        //             Console.Write(map[i * width + j] + " ");
        //         }
        //         Console.WriteLine();
        //     }
        // }
        public void Render()
        {
            Console.Clear();
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    switch (map[j * width + i])
                    {
                        case 0: // ground
                            DrawTile(i, j, "  ", ConsoleColor.White, ConsoleColor.White);
                            break;
                        case 1: // door
                            DrawTile(i, j, ")(", ConsoleColor.White, ConsoleColor.Red);
                            break;
                        case 2: // stair
                            DrawTile(i, j, ">>", ConsoleColor.White, ConsoleColor.Green);
                            break;
                        case 3: // wall
                            DrawTile(i, j, "  ", ConsoleColor.Gray, ConsoleColor.Gray);
                            break;
                        case 4: // button
                            DrawTile(i, j, "B!", ConsoleColor.White, ConsoleColor.DarkYellow);
                            break;
                        case 5: // box
                            DrawTile(i, j, "[]", ConsoleColor.White, ConsoleColor.Blue);
                            break;
                    }
                }
            }
            // DrawTile(5, 8, "@>", ConsoleColor.White, ConsoleColor.Black);
            // DrawTile(5, 7, "<@", ConsoleColor.White, ConsoleColor.Black);
            // DrawTile(5, 6, "@^", ConsoleColor.White, ConsoleColor.Black);
            // DrawTile(5, 5, "@v", ConsoleColor.White, ConsoleColor.Black);
        }
        public void DrawTile(int x, int y, string tile, ConsoleColor backgroundColor, ConsoleColor foregroundColor)
        {
            Console.SetCursorPosition(10 + x * 2, 10 + y);
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.Write(tile);
            Console.ResetColor();
        }

    }
}