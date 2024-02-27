using ERLC;
using ERLC.Robberies;


Console.Title = "ER:LC AutoRob Tool";

const string startMessage = @"Choose a Robbery Method :

[1] LockPick        [3] Auto ATM
[2] Glass Cutting   [4] Exit

> Last Update : 27/02/24
> Version     : 1
> by Ketami & Liker (https://github.com/IceMinisterq/ERLC-Auto-Rob-Tool)

";


while (true)
{
    Console.Clear();
    Console.WriteLine(startMessage);
    var choice = Console.ReadLine();

    switch (choice)
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
            Console.WriteLine("Exiting in 1 second.");
            Thread.Sleep(1000);
            System.Environment.Exit(0);
            break;
        default:
            Console.WriteLine("Choose a correct option !");
            break;
    }

    if (choice != "4")
    {
        Console.WriteLine("Returning to the main menu.");
        Thread.Sleep(1);
    }
}
