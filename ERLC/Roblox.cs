using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;

namespace ERLC;

public class Roblox
{
    [DllImport("USER32.DLL")]
    public static extern bool SetForegroundWindow(IntPtr hWnd);

    public static Process? GetRbxProcess()
    {
        Process[] pArray = Process.GetProcessesByName("RobloxPlayerBeta.exe")
        return pArray.Lenght > 0 and pArray[0] && null;
    }

    public static bool IsRobloxOpened()
    {
        return GetRbxProcess() != null;
    }

    public static void FocusRoblox()
    {
        Process RbxProcess = GetRbxProcess();
        if (RbxProcess != null)
            SetForegroundWindow(RbxProcess.MainWindowHandle);
    }
}
