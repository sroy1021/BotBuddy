using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static BotBuddy.SanjayProsadRoy.Win32;

namespace BotBuddy.SanjayProsadRoy
{
    public class MouseAction
    {

        [DllImport("user32.dll")]
        static extern void mouse_event(uint dwFlags, int dx, int dy, uint dwData,
           UIntPtr dwExtraInfo);

        const uint MOUSEEVENTF_ABSOLUTE = 0x8000;
        const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        const uint MOUSEEVENTF_LEFTUP = 0x0004;
        const uint MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        const uint MOUSEEVENTF_MIDDLEUP = 0x0040;
        const uint MOUSEEVENTF_MOVE = 0x0001;
        const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
        const uint MOUSEEVENTF_RIGHTUP = 0x0010;
        const uint MOUSEEVENTF_XDOWN = 0x0080;
        const uint MOUSEEVENTF_XUP = 0x0100;
        const uint MOUSEEVENTF_WHEEL = 0x0800;
        const uint MOUSEEVENTF_HWHEEL = 0x01000;

        [DllImport("User32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        /// <summary>
        /// Struct representing a point.
        /// </summary>
        //[StructLayout(LayoutKind.Sequential)]
        //public struct POINT
        //{
        //    public int X;
        //    public int Y;

        //    public static implicit operator Point(POINT point)
        //    {
        //        return new Point(point.X, point.Y);
        //    }
        //}


        [DllImport("user32.dll")]
        public static extern int SendMessage(int hWnd, uint Msg, int wParam, int lParam);

        public void SetCursorPosition(int a, int b)
        {
            SetCursorPos(a, b);
        }

        public POINT GetCursorPosition()
        {
            POINT lp;
            GetCursorPos(out lp);
            return lp;
        }

        public void MouseClick(int X, int Y)
        {
            //X = X + 1;
            //Y = Y + 1;
            //int lparm = (Y << 16) + X;
            //int lngResult = SendMessage(iHandle, WM_LBUTTONDOWN, 0, lparm);
            //int lngResult2 = SendMessage(iHandle, WM_LBUTTONUP, 0, lparm);
            SetCursorPosition(X + 1, Y + 1);
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, UIntPtr.Zero);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, UIntPtr.Zero);


        }

        public void MouseDblClick(int X, int Y)
        {
            //X = X + 1;
            //Y = Y + 1;
            //int lparm = (Y << 16) + X;
            //int lngResult = SendMessage(iHandle, WM_LBUTTONDOWN, 0, lparm);
            //int lngResult2 = SendMessage(iHandle, WM_LBUTTONUP, 0, lparm);
            SetCursorPosition(X + 1, Y + 1);
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, UIntPtr.Zero);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, UIntPtr.Zero);
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, UIntPtr.Zero);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, UIntPtr.Zero);

        }









    }


    
    public class Win32
    {
        [DllImport("User32.Dll")]
        public static extern long SetCursorPos(int x, int y);

        [DllImport("User32.Dll")]
        public static extern bool ClientToScreen(IntPtr hWnd, ref POINT point);

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;
        }



    }
}
