using System.Drawing;
using System.Runtime.InteropServices;
using Microsoft.Win32;

#pragma warning disable CA1416

namespace ERLC
{
    public static class Screen
    {
        private const int DesktopHorzres = 118;
        private const int DesktopVertres = 117;

        public static int ScreenWidth;
        public static int ScreenHeight;
        
        [DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        static extern IntPtr GetDesktopWindow();
        
        [DllImport("user32.dll")]
        static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);
        
        [DllImport("gdi32.dll", EntryPoint = "GetDeviceCaps")]
        static extern int GetDeviceCaps(IntPtr hdc, int nIndex);
        
        [DllImport("gdi32.dll")]
        static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);

        static Screen()
        {
            IntPtr desktopDc = GetDC(GetDesktopWindow());

            ScreenWidth = GetDeviceCaps(desktopDc, DesktopHorzres);
            ScreenHeight = GetDeviceCaps(desktopDc, DesktopVertres);
        }
        
        static public Bitmap TakeScreenshot()
        {
            Bitmap nBitmap = new Bitmap(ScreenWidth, ScreenHeight);
            Graphics.FromImage(nBitmap).CopyFromScreen(0, 0, 0, 0, nBitmap.Size);

            return nBitmap;
        }

        public static (int, int) LocateColor(Color color, int tolerance = 0)
        {
            Bitmap screen = TakeScreenshot();
            for (int y = 0; y < ScreenHeight; y++)
            {
                for (int x = 0; x < ScreenWidth; x++)
                {
                    Color pColor = screen.GetPixel(x, y);
                    if (pColor == color || AreColorsClose(color, pColor, tolerance))
                        return (x, y);
                }
            }
            
            return (0, 0);
        }
        
        public static (int, int) FindColorInArea(Color color1, Color color2, int tolerance, int fromX, int toX, int fromY, int toY)
        {
            Bitmap screen = TakeScreenshot();

            for (int x = fromX; x < toX; x++)
            {
                for (int y = fromY; y < toY; y++)
                {
                    //Mouse.SetMousePos(x, y);

                    Color pColor = screen.GetPixel(x, y);
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
            IntPtr hdc = GetDC(IntPtr.Zero);
            uint pixel = GetPixel(hdc, x, y);
            ReleaseDC(IntPtr.Zero, hdc);

            return Color.FromArgb(255,
                (int)(pixel & 0xFF),
                (int)((pixel & 0xFF00) >> 8),
                (int)((pixel & 0xFF0000) >> 16)
            );
        }

        public static bool AreColorsClose(Color color1, Color color2, int maxDiff)
        {
            return Math.Abs(color1.R - color2.R) <= maxDiff &&
                   Math.Abs(color1.G - color2.G) <= maxDiff &&
                   Math.Abs(color1.B - color2.B) <= maxDiff;
        }
        
        public static double GetScale()
        {
            // If any if you reading this find this find a way to make it work with GetDpiForSystem from user32.dll? It wasn't working properly when i tried
            try
            {
                var key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop\WindowMetrics");

                if (key != null)
                {
                    object? scaleValue = key.GetValue("AppliedDPI");
                    if (scaleValue != null)
                    {
                        int dpi = Convert.ToInt32(scaleValue);
                        double scale = 1;
                        if (dpi != 96)
                        {
                            scale = (dpi / 96f);
                        }
                        
                        return scale;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return 1;
        }
        
        // Default values are set for the Scale & Layout Setting (100%)
        public static double SystemScaleMultiplier = Screen.GetScale();
    }
    
}
