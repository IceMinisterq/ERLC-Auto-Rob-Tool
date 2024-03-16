using System.Drawing;

namespace ERLC.Robberies;
public class GlassCutting
{
    private const int StartTime = 1;

    private static Color SquareColor  = Color.FromArgb(255, 85, 255, 0);
    private static Color SquareColor0 = Color.FromArgb(255, 255, 0, 0);

    private static int OFFSET = (int)Math.Floor(23 * Screen.SystemScaleMultiplier);
    private static int SEARCH_OFFSET = (int)Math.Floor(15 * Screen.SystemScaleMultiplier);

    public static void StartProcess()
    {
        Console.WriteLine($"i ~ Starting process in {StartTime}");
        Roblox.FocusRoblox();

        Thread.Sleep(StartTime * 1000);

        int screenWidth = Screen.ScreenWidth, screenHeight = Screen.ScreenHeight;

        int hightPer5 = screenHeight / 5;
        int widthPer3 = screenWidth / 3;

        int left = widthPer3, right = screenWidth - widthPer3;
        int top = hightPer5, bottom = screenHeight - hightPer5;

        bool wasSquareFound = false;
        int findingAttempts = 0;

        int oldX = 0, oldY = 0;

        while (true)
        {
            int x = 0, y = 0;
            if (!Roblox.IsRobloxRunning()) break;

            if (wasSquareFound)
            {
                (x, y) = Screen.FindColorInArea(
                    SquareColor, SquareColor0,
                    10,
                    oldX - SEARCH_OFFSET, oldX + SEARCH_OFFSET,
                    oldY - SEARCH_OFFSET, oldY + SEARCH_OFFSET
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

                if (findingAttempts > 25)
                {
                    Console.WriteLine("i ~ Robbing Finished / Could not find green square!");
                    break;
                }
            }
            else
            {
                findingAttempts = 0;

                oldX = x;
                oldY = y;

                x += OFFSET;
                y += OFFSET;

                Mouse.SetMousePos(x, y);

                if (!wasSquareFound) // --> Wiggle Mouse to trigger robbery
                {
                    Mouse.SetMousePos(x + 2, y + 2);
                    Mouse.SetMousePos(x - 2, y - 2);
                    wasSquareFound = true;
                }
            }
        }
    }
}
