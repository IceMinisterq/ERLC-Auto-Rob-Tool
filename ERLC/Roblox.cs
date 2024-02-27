using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;

namespace ERLC;

public class Roblox
{
    [DllImport("user32.dll")]
    public static extern bool SetForegroundWindow(IntPtr hWnd);

    public static Process? GetRbxProcess()
    {
        Process[] pArray = Process.GetProcessesByName("RobloxPlayerBeta.exe");
        
        if (pArray.Length > 0)
        {
            return pArray[0];
        }

        return null;
    }

    public static bool IsRobloxOpened()
    {
        return GetRbxProcess() != null;
    }

    public static void FocusRoblox()
    {
        Process? RbxProcess = GetRbxProcess();
        if (RbxProcess != null)
            SetForegroundWindow(RbxProcess.MainWindowHandle);
    }
}
