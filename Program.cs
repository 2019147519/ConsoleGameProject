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
        // 콘솔 크기 변경 차단을 위해
        const int GWL_STYLE = -16;

        const int WS_SIZEBOX = 0x00040000;
        const int WS_MAXIMIZEBOX = 0x00010000;
        const int WS_MINIMIZEBOX = 0x00020000;

        const int SWP_NOMOVE = 0x0002;
        const int SWP_NOSIZE = 0x0001;
        const int SWP_NOZORDER = 0x0004;
        const int SWP_FRAMECHANGED = 0x0020;

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        static extern bool SetWindowPos(
            IntPtr hWnd,
            IntPtr hWndInsertAfter,
            int X,
            int Y,
            int cx,
            int cy,
            uint uFlags);

        static void Main(string[] args)
        {
            IntPtr console = GetConsoleWindow();

            int style = GetWindowLong(console, GWL_STYLE);

            // 크기 조절 + 최대화 버튼 제거
            style &= ~WS_SIZEBOX;
            style &= ~WS_MAXIMIZEBOX;

            // (선택) 최소화 버튼도 제거하고 싶다면
            // style &= ~WS_MINIMIZEBOX;

            SetWindowLong(console, GWL_STYLE, style);

            // 스타일 변경 사항을 즉시 반영
            SetWindowPos(
                console,
                IntPtr.Zero,
                0, 0, 0, 0,
                SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_FRAMECHANGED
            );

            // 콘솔 창 크기 조절
            Console.SetWindowSize(160, 45);
            Console.SetBufferSize(160, 45);

            // 게임 시작
            Game game = new Game();
            game.Start();
        }
    }
}
// dotnet publish -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true