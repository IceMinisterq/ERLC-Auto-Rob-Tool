using System.Drawing;
using System.Runtime.CompilerServices;

#pragma warning disable CA1416

namespace ERLC.Robberies;
public class ATM
{
    private const int StartTime = 2;
    
    private static Color BorderColor = ColorTranslator.FromHtml("#1B2A35");
    
    const int borderSizeX = 822;
    const int borderSizeY = 548;

    private static int centerX =  0, centerY = 0;
    private static int borderX =  0, borderY = 0;
    
    static int ScreenWidth = Screen.ScreenWidth, ScreenHeight = Screen.ScreenHeight;

    private static Color GetColorToFind()
    {
        int fromX = (centerX) + 10;
        int toX = fromX + 210;
        int fromY = borderY + 80;
        int toY = borderY + 100;
    
        Bitmap screen = Screen.TakeScreenshot();
        Color highestColor = Color.Black;

        for (int x = fromX; x < toX; x++)
        {
            for (int y = fromY; y < toY; y++)
            {
                Color pColor = screen.GetPixel(x, y);
                if (pColor.R > highestColor.R & pColor.G > highestColor.G & pColor.B > highestColor.B)
                {
                    highestColor = pColor;
                }
            }
        }

        return highestColor;
    }

    public static void StartProcess()
    {
        Console.WriteLine($"i ~ Starting process in {StartTime}");
        Roblox.FocusRoblox();

        Thread.Sleep(StartTime * 1000);

        (borderX, borderY) = Screen.LocateColor(BorderColor);
        if (borderX == 0 && borderY == 0)
        {
            Console.WriteLine("! ~ Could not find ATM Firewall's Frame!");
            Thread.Sleep(1000);
            return;
        }
        
        centerX =  borderX + (borderSizeX / 2);
        centerY =  borderY + (borderSizeY / 2);

        // Dimension of the frame with the codes
        int fromX = borderX + 82;
        int toX   = (borderX + borderSizeX) - 84;

        int fromY = centerY - 128;
        int toY   = (borderY + borderSizeY) - 73;
        
        while (true)
        {
            try
            {
                (borderX, borderY) = Screen.LocateColor(BorderColor);

                if (borderX == 0 && borderY == 0)
                {
                    Console.WriteLine("! ~ Could not find ATM Firewall's Frame!");
                    Thread.Sleep(1000);
                    break;
                }

                Color colorToFind = GetColorToFind();
                var (foundColorPosX, foundColorPosY) = Screen.FindColorInArea(
                    colorToFind, colorToFind,
                    2,
                    fromX, toX,
                    fromY, toY
                );

                if (foundColorPosX == 0 && foundColorPosY == 0)
                {
                    Console.WriteLine("! ~ Could not find Color position in ATM's Firewall Frame!");
                    break;
                }

                Console.WriteLine($"i ~ Color to to detect : {colorToFind.R},{colorToFind.G},{colorToFind.B}");
                Mouse.SetMousePos(foundColorPosX, foundColorPosY);

                while (true)
                {
                    Color textColor = Screen.GetColorAtPixel(foundColorPosX, foundColorPosY);

                    if (!Screen.AreColorsClose(textColor, colorToFind, 5))
                    {
                        Mouse.LeftClick();
                        Console.WriteLine("i ~ Clicked...");
                        break;
                    }
                }

                Console.WriteLine("i ~ Switching to the new Color in 0.5 seconds...");
                Thread.Sleep(500);
            }
            catch (ArgumentOutOfRangeException Ex)
            {
                Console.WriteLine($"! ~ Exception during automation: {Ex.Message}\n\nDid you select the correct minigame?");
                return;
            }
        }

        Console.WriteLine("i ~ Robbing Finished!");
    }
}
