using System.Drawing;

namespace ERLC.Robberies;

public class ATM
{
    const int StartTime = 2;
    private static Color BorderColor = ColorTranslator.FromHtml("#1B2A35");
    
    const int borderSizeX = 822;
    const int borderSizeY = 548;

    private static int centerX =  0, centerY = 0;
    private static int borderX =  0, borderY = 0;
    
    static int ScreenWidth = Screen.ScreenWidth, ScreenHeight = Screen.ScreenHeight;

    private static Color GetColorToFind()
    {
        var fromX = (centerX) + 10;
        var toX = fromX + 210;
        var fromY = borderY + 80;
        var toY = borderY + 100;
    
        var screen = new Bitmap(ScreenWidth, ScreenHeight);
        Graphics.FromImage(screen).CopyFromScreen(0, 0, 0, 0, screen.Size);
    
        var highestColor = Color.Black;

        for (var x = fromX; x < toX; x++)
        {
            for (var y = fromY; y < toY; y++)
            {
                var pColor = screen.GetPixel(x, y);
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
        Console.WriteLine($"Starting process in {StartTime.ToString()}");
        Roblox.FocusRoblox();
        Thread.Sleep(StartTime * 1000);
        
        (borderX, borderY) = Screen.LocateColorInScreen(BorderColor);

        if (borderX == 0 && borderY == 0)
        {
            Console.WriteLine("Could not determine ATM Firewall's Frame!");
            Thread.Sleep(1000);
            return;
        }
        
        centerX =  borderX + (borderSizeX / 2);
        centerY =  borderY + (borderSizeY / 2);
        
        // Dimension of the frame with the codes
        var fromX = borderX + 82;
        var toX   = (borderX + borderSizeX) - 84;

        var fromY = centerY - 128;
        var toY   = (borderY + borderSizeY) - 73;
        
        while (true)
        {
            (borderX, borderY) = Screen.LocateColorInScreen(BorderColor);
            
            if (borderX == 0 && borderY == 0)
            {
                Console.WriteLine("Could not determine ATM Firewall's Frame!");
                Thread.Sleep(1000);
                break;
            }
            
            var colorToFind = GetColorToFind();
            var (foundColorPosX, foundColorPosY) = Screen.FindColorInArea(
                colorToFind, colorToFind, 
                2, 
                fromX, toX,
                fromY, toY
            );
            
            if (foundColorPosX == 0 && foundColorPosY == 0)
            {
                Console.WriteLine("Could not determine Color position in ATM's Firewall Frame!");
                break;
            }

            Console.WriteLine($"Color to to detect : {colorToFind.R},{colorToFind.G},{colorToFind.B}");
            Mouse.SetMousePos(foundColorPosX, foundColorPosY);
            
            while (true)
            {
                var textColor = Screen.GetColorAtPixel(foundColorPosX, foundColorPosY);
                
                if (!Screen.AreColorsClose(textColor, colorToFind, 5)) {
                    Mouse.RightClick();
                    Console.WriteLine("Right Clicking...");
                    break;
                }
            }
            
            Console.WriteLine("Switching to the new Color in 1 seconds");
            Thread.Sleep(1000);
        }
        
        Thread.Sleep(1000);
    }
}