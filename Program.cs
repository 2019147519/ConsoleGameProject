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

    internal class Program
    {
        [DllImport("msvcrt.dll")]
        static extern int _getch();  //c언어 함수 가져옴

        static void Main(string[] args)
        {


            // Stage testStage = new Stage("../../../Assets/map/test.txt");
            // testStage.Render();
            // Console.ReadLine();
            
        }
    }
}
// dotnet publish -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true