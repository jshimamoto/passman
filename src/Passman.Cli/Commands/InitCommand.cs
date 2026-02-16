using System.Text;
using System.Text.Json;
using Passman.Core.Models;
using Passman.Core.Storage;
using Passman.Core.Utils;

namespace Passman.Cli.Commands;


public class InitCommand
{
    private readonly string _dbPath;

    public InitCommand(string? dbPath = null)
    {
        _dbPath = dbPath ?? DatabasePath.GetDefaultPath();
    }

    public void Execute(string[] args)
    {

        var pass1 = ReadHiddenInput.Execute("Set master password for database: ");
        var pass2 = ReadHiddenInput.Execute("Confirm password: ");

        if (pass1 != pass2)
        {
            Console.WriteLine("Passwords do not match");
            return;
        }

        Console.WriteLine(_dbPath);
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
            Console.WriteLine();
        }

        string? csvPath = null;
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "--csv" && args[i + 1].Length != 0)
            {
                csvPath = args[i + 1];
            }
        }

        var credentials = new List<Credential>();
        if (!string.IsNullOrWhiteSpace(csvPath))
        {
            if (string.IsNullOrWhiteSpace(csvPath))
            {
                Console.WriteLine("No CSV provided. Creating empty database");
            }
            else
            {
                if (!File.Exists(csvPath))
                {
                    Console.WriteLine("CSV File not found");
                    return;
                }

                credentials = CsvImporter.ImportCsv(csvPath);
                Console.WriteLine($"Imported {credentials.Count} credentials");
            }
        }

        var json = JsonSerializer.Serialize(credentials, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        var bytes = Encoding.UTF8.GetBytes(json);
        DatbaseFileService.SaveFile(_dbPath, pass1, bytes);
        Console.WriteLine($"Database initialized at {_dbPath}");
    }
}