namespace ConsoleGameProject
{
    static class Renderer
    {
        // Field (Member Variables)
        const int drawOffsetX = 10;
        const int drawOffsetY = 10;

        // Property

        // Method
        public static void DrawPlayer(int x, int y, Direction direction, bool grab)
        {
            Console.CursorVisible = false;

            string playerForm = "@@";
            ConsoleColor playerColor = ConsoleColor.Black;

            if (grab) playerColor = ConsoleColor.DarkCyan;

            switch (direction)
            {
                case Direction.up:
                    playerForm = "@^";
                    break;
                case Direction.down:
                    playerForm = "@v";
                    break;
                case Direction.left:
                    playerForm = "<@";
                    break;
                case Direction.right:
                    playerForm = "@>";
                    break;
            }
            Console.SetCursorPosition(drawOffsetX + x * 2, drawOffsetY + y);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = playerColor;
            Console.Write(playerForm);
            Console.ResetColor();
        }
        public static void DrawTile(int x, int y, int tileNumber)
        {
            Console.CursorVisible = false;

            ConsoleColor backgroundColor = ConsoleColor.Black;
            ConsoleColor foregroundColor = ConsoleColor.Black;
            string tile = "  ";

            switch (tileNumber)
            {
                case 0: // ground
                    backgroundColor = ConsoleColor.White;
                    foregroundColor = ConsoleColor.White;
                    tile = "  ";
                    break;
                case 1: // button
                    backgroundColor = ConsoleColor.White;
                    foregroundColor = ConsoleColor.DarkYellow;
                    tile = "B!";
                    break;
                case 2: // stair
                    backgroundColor = ConsoleColor.White;
                    foregroundColor = ConsoleColor.Green;
                    tile = ">>";
                    break;
                case 3: // door (open)
                    backgroundColor = ConsoleColor.White;
                    foregroundColor = ConsoleColor.Red;
                    tile = "||";
                    break;
                case 4: // wall
                    backgroundColor = ConsoleColor.Gray;
                    foregroundColor = ConsoleColor.Gray;
                    tile = "  ";
                    break;
                case 5: // door (close)
                    backgroundColor = ConsoleColor.White;
                    foregroundColor = ConsoleColor.Red;
                    tile = ")(";
                    break;
                case 6: // box
                case 7: // box on button
                    backgroundColor = ConsoleColor.White;
                    foregroundColor = ConsoleColor.Blue;
                    tile = "[]";
                    break;
            }

            Console.SetCursorPosition(drawOffsetX + x * 2, drawOffsetY + y);
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.Write(tile);
            Console.ResetColor();
        }
    }
}