using System;
using System.Runtime.InteropServices;

namespace ERLC
{
    public class Mouse
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint SendInput(uint nInputs, ref Input pInputs, int cbSize);

        [StructLayout(LayoutKind.Sequential)]
        private struct Input
        {
            public SendInputEventType type;
            public MouseKeybdhardwareInputUnion mkhi;
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct MouseKeybdhardwareInputUnion
        {
            [FieldOffset(0)] public MouseInputData mi;
            [FieldOffset(0)] public KEYBDINPUT ki;
            [FieldOffset(0)] public HARDWAREINPUT hi;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct KEYBDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct HARDWAREINPUT
        {
            public int uMsg;
            public short wParamL;
            public short wParamH;
        }

        struct MouseInputData
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public MouseFlags dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [Flags]
        private enum MouseFlags : uint
        {
            MOUSEEVENTF_MOVE = 0x0001,
            MOUSEEVENTF_LEFTDOWN = 0x0002,
            MOUSEEVENTF_LEFTUP = 0x0004,
            MOUSEEVENTF_RIGHTDOWN = 0x0008,
            MOUSEEVENTF_RIGHTUP = 0x0010,
            MOUSEEVENTF_ABSOLUTE = 0x8000
        }

        private static int ScreenWidth = Screen.ScreenWidth;
        private static int ScreenHeight = Screen.ScreenHeight;

        enum SendInputEventType : int
        {
            InputMouse,
            InputKeyboard,
            InputHardware
        }

        private static void SendMouseInput(MouseFlags flag)
        {
            var mouseDownInput = new Input();
            mouseDownInput.type = SendInputEventType.InputMouse;
            mouseDownInput.mkhi.mi.dwFlags = flag;
            SendInput(1, ref mouseDownInput, Marshal.SizeOf(typeof(Input)));
        }
        public static void Mouse1Down()
        {
            SendMouseInput(MouseFlags.MOUSEEVENTF_LEFTDOWN);
        }

        public static void Mouse1Up()
        {
            SendMouseInput(MouseFlags.MOUSEEVENTF_LEFTUP);
        }
        
        public static void Mouse2Down()
        {
            SendMouseInput(MouseFlags.MOUSEEVENTF_RIGHTDOWN);
        }

        public static void Mouse2Up()
        {
            SendMouseInput(MouseFlags.MOUSEEVENTF_RIGHTUP);
        }

        // --> Thank you IceMinister for mixing up the Mouse1 and Mouse2
        public static void LeftClick(int time = 1)
        {
            Mouse1Down();
            Thread.Sleep(time);
            Mouse1Up();
        }

        public static void RightClick(int time = 1)
        {
            Mouse2Down();
            Thread.Sleep(time);
            Mouse2Up();
        }
        
        public static void SetMousePos(int x, int y) // black magic - Credits to @MakeSureDudeDies
        {
            var mouseInput = new Input();
            mouseInput.type = SendInputEventType.InputMouse;
            mouseInput.mkhi.mi.dx = x * 65536 / ScreenWidth;
            mouseInput.mkhi.mi.dy = y * 65536 / ScreenHeight;
            mouseInput.mkhi.mi.dwFlags = MouseFlags.MOUSEEVENTF_ABSOLUTE | MouseFlags.MOUSEEVENTF_MOVE;
            SendInput(1, ref mouseInput, Marshal.SizeOf(typeof(Input)));
        }
    }
}
