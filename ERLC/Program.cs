using System;

using ERLC;
using ERLC.Robberies;

class Program
{
    static void Main(string[] args)
    {
        Console.Title = "ER:LC AutoRob Tool";
        Console.TreatControlCAsInput = false;

        const string startMessage = @"// Choose a Robbery Method:

            [1] LockPick        [3] Auto ATM
            [2] Glass Cutting   [4] Exit

        DISCLAIMER: Use this tool responsibly and in accordance with ER:LC's terms of service.
        We, the developers and contributors of this tool are not responsible
        for any consequences resulting from the misuse of the tool.

        Make sure you downloaded the program from the original github link available below.
        This program is free - if you bought it, you got scammed.

        > Last Update : 27/02/24
        > Version     : 1.0.0
        > By Ketami & Liker (https://github.com/IceMinisterq/ERLC-Auto-Rob-Tool)
        ";

        if (!ERLC.Roblox.IsRobloxOpened())
        {
            Console.WriteLine("! ~ Roblox is not opened! Please join ER:LC in order to use the AutoRob.");
            Console.ReadKey(true);

            System.Environment.Exit(0);
        }

        while (true)
        {
            Console.Clear();
            Console.WriteLine(startMessage);
            string option = Console.ReadKey(true).KeyChar.ToString();

            switch (option)
            {
                case "1":
                    LockPicking.StartProcess();
                    break;
                case "2":
                    GlassCutting.StartProcess();
                    break;
                case "3":
                    ATM.StartProcess();
                    break;
                case "4":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine($"! ~ Choose a correct option! (Got option \"{option}\")");
                    break;
            }

            Thread.Sleep(1500);
        }
    }
}
