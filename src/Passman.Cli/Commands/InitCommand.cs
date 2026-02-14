using Passman.Core.Models;
using Passman.Core.Utils;

namespace Passman.Cli.Commands;


public class InitCommand
{
    private readonly string _dbPath;

    public InitCommand(string dbPath = "passwords.db")
    {
        _dbPath = dbPath;
    }

    public void Execute(string[] args)
    {
        if (File.Exists(_dbPath))
        {
            Console.Write("Credential database already exists. Overwrite? [y/n]: ");
            while (true)
            {
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Y)
                {
                    break;
                } else if (key.Key == ConsoleKey.N)
                {
                    return;
                }
            }
        }

        string? csvPath = null;
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "--csv" && args[i + 1].Length != 0)
            {
                csvPath = args[i + 1];
            }
        }

        // var password = ReadHiddenInput("Password: ");

        var credentials = new List<Credential>();
        if (!string.IsNullOrWhiteSpace(csvPath))
        {
            if (!File.Exists(csvPath))
            {
                Console.WriteLine("CSV File not found");
                return;
            }

            credentials = CsvImporter.ImportCsv(csvPath);
            
        }
    }
}