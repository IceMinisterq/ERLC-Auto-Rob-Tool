using System;
using System.Collections.Generic;
using System.Diagnostics;
using ERLC;
using ERLC.Robberies;

class Program
{
    static void Main(string[] args)
    {
        Console.Title = "ER:LC AutoRob Tool";
        Console.TreatControlCAsInput = false;

        string startMessage = String.Join(Environment.NewLine,
            $"\t// Choose a Robbery Method:",
            "\n\t\t[1] LockPick        [3] Auto ATM",
            "\t\t[2] Glass Cutting   [4] Car Crowbar",
            "\t\t[5] Exit",

            "\n\tDISCLAIMER: Use this tool responsibly and in accordance with ER:LC's terms of service.",
            "\tWe, the developers and contributors of this tool are not responsible",
            "\tfor any consequences resulting from the misuse of the tool.",

            "\n\tMake sure you downloaded the program from the original github link available below.",
            "\tThis program is free - if you bought it, you got scammed.",
            "\thttps://github.com/IceMinisterq/ERLC-Auto-Rob-Tool",

            "\n\t> Last Update: 16/03/24",
            "\t> Version    : 1.1.1",
            "\t> By Ketami & Liker",
            $"\t> Screen Scale Factor : {Screen.SystemScaleMultiplier}"
        );
        
        if (!Roblox.IsRobloxRunning())
        {
            Console.WriteLine("i ~ Waiting for Roblox to open...");

            while (!Roblox.IsRobloxRunning()) // --> Feel free to make a PR if you guys know how to wait more efficiently
            {
                Thread.Sleep(500);
            }
        }

        while (true)
        {
            Console.Clear();
            Console.Write(startMessage + "\n\n$ ~ Choice: ");

            string option = Console.ReadKey().KeyChar.ToString();
            Console.Write("\n\n");

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
                    Crowbar.StartProcess();
                    break;
                case "5":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine($"! ~ Choose a correct option!");
                    break;
            }

            Thread.Sleep(1500);
        }
    }
}
