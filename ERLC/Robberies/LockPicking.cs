using System.Drawing;

namespace ERLC.Robberies;
public class LockPicking
{
    private const int StartTime = 1;
    private static Color LineColor = ColorTranslator.FromHtml("#FFC903");

    public static void StartProcess()
    {
        Console.WriteLine($"i ~ Starting process in {StartTime}");
        Roblox.FocusRoblox();

        Thread.Sleep(StartTime * 1000);

        var (linePosX, linePosY) = Screen.LocateColor(LineColor, 0);
        if (linePosX == 0 && linePosY == 0)
        {
            Console.WriteLine("! ~ LockPicking line could not be found!");
            return;
        }

        Console.WriteLine($"i ~ Found Line at {linePosY}, {linePosY}");

        for (int rectI = 1; rectI < 7; rectI++)
        {
            int x = linePosX + (83 * rectI);

            while (true)
            {
                Color color1 = Screen.GetColorAtPixel(x, linePosY + 10);
                Color color2 = Screen.GetColorAtPixel(x, linePosY - 4);

                if (
                    color1.R > 140 & color1.G > 140 & color1.B > 140 &&
                    color2.R > 140 & color2.G > 140 & color2.B > 140
                )
                {
                    Mouse.LeftClick();

                    Mouse.SetMousePos(x, linePosY);
                    break;
                }
            }
        }

        Console.WriteLine("i ~ Robbing Finished!");
    }
}
