using System.Drawing;

namespace ERLC.Robberies;

public class GlassCutting
{
    private const int StartTime = 1;
    
    private static Color SquareColor = Color.FromArgb(255, 85, 255, 0);
    private static Color SquareColor0 = Color.FromArgb(255, 255,0,0);

    private const int OFFSET = 20;
    
    public static void StartProcess()
    {
        Console.WriteLine($"Starting process in {StartTime.ToString()}");
        Roblox.FocusRoblox();
        Thread.Sleep(StartTime * 1000);

        int screenWidth = Screen.ScreenWidth, screenHeight = Screen.ScreenHeight;
        
        int hightPer5 = screenHeight / 5;
        int widthPer3 = screenWidth / 3;
        
        int left = widthPer3, right = screenWidth - widthPer3;
        int top = hightPer5, bottom = screenHeight - hightPer5;
        
        var wasSquareFound = false;
        var findingAttempts = 0;
        
        int oldX = 0, oldY = 0;
        
        while (true)
        {
            int x = 0, y = 0;
            if (!Roblox.IsRobloxOpened()) break;
            
            if (wasSquareFound)
            {
                (x, y) = Screen.FindColorInArea(
                    SquareColor, SquareColor0,
                    10,
                    oldX - OFFSET, oldX + OFFSET,
                    oldY - OFFSET, oldY + OFFSET
                );
            }
            else
            {
                (x, y) = Screen.FindColorInArea(
                    SquareColor, SquareColor0,
                    15,
                    left, right,
                    top, bottom
                );
            }

            if (x == 0 && y == 0)
            {
                wasSquareFound = false;
                findingAttempts++;
                
                if (findingAttempts > 25) return;
            }
            else
            {
                findingAttempts = 0;
                
                oldX = x;
                oldY = y;
                
                x += OFFSET;
                y += OFFSET;
                
                Mouse.SetMousePos(x, y);
                
                if (!wasSquareFound)
                {
                    Roblox.FocusRoblox();
                    Mouse.LeftClick();
                    wasSquareFound = true;
                }
            }
        }
    }
}