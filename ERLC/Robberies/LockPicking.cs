using System.Drawing;

namespace ERLC.Robberies;

public class LockPicking
{
    const int StartTime = 1;
    private static Color LineColor = ColorTranslator.FromHtml("#FFC903");
    
    public static void StartProcess()
    {
        Console.WriteLine($"Starting process in {StartTime.ToString()}");
        Roblox.FocusRoblox();
        Thread.Sleep(StartTime * 1000);
        
        var (linePosX, linePosY) = Screen.LocateColorInScreen(LineColor, 0);
        var mLinePosX = linePosX + 570;

        if (linePosX == 0 && linePosY == 0)
        {
            Console.WriteLine("LockPicking line could not be found !");
            return;
        }
        
        Console.WriteLine($"Found Line at {linePosY},{linePosY}");
        
        for (var rectI = 1; rectI < 7; rectI ++) {
            var x = linePosX + (83 * rectI);
            
            while (true)
            {
                var color1 = Screen.GetColorAtPixel(x, linePosY + 10);
                var color2 = Screen.GetColorAtPixel(x, linePosY - 4);

                if (
                    color1.R > 140 & color1.G > 140 & color1.B > 140 &&
                    color2.R > 140 & color2.G > 140 & color2.B > 140
                ) {
                    Mouse.Mouse1Down();
                    Mouse.Mouse1Down();
                    
                    Mouse.SetMousePos(x, linePosY);
                    break;
                }
            }
        }
    }
}