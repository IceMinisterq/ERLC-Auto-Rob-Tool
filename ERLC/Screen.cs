using System.Drawing;
using System.Runtime.InteropServices;

namespace ERLC
{
    public static class Screen
    {
        private const int DesktopHorzres = 118;
        private const int DesktopVertres = 117;

        public static int ScreenWidth;
        public static int ScreenHeight;
        
        public static int centerX = ScreenWidth/2, centerY = ScreenHeight/2;

        [DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr hwnd);
        [DllImport("user32.dll")]
        private static extern IntPtr GetDesktopWindow();
        [DllImport("user32.dll")]
        static extern Int32 ReleaseDC(IntPtr hwnd, IntPtr hdc);
        [DllImport("gdi32.dll", EntryPoint = "GetDeviceCaps")]
        private static extern int GetDeviceCaps(IntPtr hdc, int nIndex);
        [DllImport("gdi32.dll")]
        static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);

        static Screen()
        {
            var desktopHandle = GetDesktopWindow();
            var desktopDc = GetDC(desktopHandle);

            ScreenWidth = GetDeviceCaps(desktopDc, DesktopHorzres);
            ScreenHeight = GetDeviceCaps(desktopDc, DesktopVertres);
        }
        
        public static (int, int) LocateColorInScreen(Color color, int tolerance = 0)
        {
            var screen = new Bitmap(ScreenWidth, ScreenHeight);
            Graphics.FromImage(screen).CopyFromScreen(0, 0, 0, 0, screen.Size);
            
            for (var y = 0; y < ScreenHeight; y ++)
            {
                for (var x = 0; x < ScreenWidth; x++)
                {
                    var pColor = screen.GetPixel(x, y);
                    if (pColor == color || AreColorsClose(color, pColor, tolerance))
                        return (x, y);
                }
            }
            
            return (0, 0);
        }
        
        public static (int, int) FindColorInArea(Color color1, Color color2, int tolerance, int fromX, int toX, int fromY, int toY)
        {
            var screen = new Bitmap(ScreenWidth, ScreenHeight);
            Graphics.FromImage(screen).CopyFromScreen(0, 0, 0, 0, screen.Size);
            
            for (var x = fromX; x < toX; x++)
            {
                for (var y = fromY; y < toY; y++)
                {
                    var pColor = screen.GetPixel(x, y);
                    if (
                        (pColor == color1) ||
                        (pColor == color2) ||
                        AreColorsClose(pColor, color1, tolerance) || AreColorsClose(pColor, color2, tolerance)
                    ) {
                        return (x, y);
                    }
                }
            }

            return (0, 0);
        }

        public static Color GetColorAtPixel(int x, int y)
        {
            var hdc = GetDC(IntPtr.Zero);
            var pixel = GetPixel(hdc, x, y);
            ReleaseDC(IntPtr.Zero, hdc);

            return Color.FromArgb(255,
                (int)(pixel & 0x000000FF),
                (int)((pixel & 0x0000FF00) >> 8),
                (int)((pixel & 0x00FF0000) >> 16)
            );
        }
        
        public static bool AreColorsClose(Color color1, Color color2, int maxDiff)
        {
            return Math.Abs(color1.R - color2.R) <= maxDiff &&
                   Math.Abs(color1.G - color2.G) <= maxDiff &&
                   Math.Abs(color1.B - color2.B) <= maxDiff;
        }
    }
}
