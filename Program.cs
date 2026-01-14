using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameProject
{
    enum Direction   // 방향을 위한 열거형 (enum)
    {
        up,
        down,
        left,
        right
    }
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
    class Stage
    {

    }
    internal class Program
    {
        [DllImport("msvcrt.dll")]
        static extern int _getch();  //c언어 함수 가져옴

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Player player = new Player(3, 3);
            
        }
    }
}