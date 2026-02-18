namespace Passman.Cli.Utils;

public static class ConfirmAction
{
    public static bool Execute(string prompt)
    {
        Console.Write($"{prompt} [y/n]: ");
        bool shouldProceed;
        while (true)
        {
            var key = Console.ReadKey();
            if (key.Key == ConsoleKey.Y)
            {
                shouldProceed = true;
                break;
            } else if (key.Key == ConsoleKey.N)
            {
                shouldProceed = false;
                break;
            }
        }
        Console.WriteLine();
        return shouldProceed;
    }
}