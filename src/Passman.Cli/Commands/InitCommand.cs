using System.Text;
using System.Text.Json;
using Passman.Cli.Utils;
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

        var pass1 = ReadInput.Execute("Set master password for database: ", hidden: true);
        var pass2 = ReadInput.Execute("Confirm password: ", hidden: true);

        if (pass1 != pass2)
        {
            Console.WriteLine("Passwords do not match");
            return;
        }

        if (File.Exists(_dbPath))
        {
            bool confirmation = ConfirmAction.Execute("Credential database already exists. Overwrite?");
            if (!confirmation) return;
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

        Database db = new Database();
        db.Add(credentials);
        DatabaseFileService.SaveFile(_dbPath, pass1, db);
        Console.WriteLine($"Database initialized at {_dbPath}");
    }
}