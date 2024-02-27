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
        return Process.GetProcessesByName("RobloxPlayerBeta")[0];
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