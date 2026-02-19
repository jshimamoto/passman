using Passman.Core.Storage;

namespace Passman.Cli.Commands;

public class SearchCommand
{
    public void Execute(string arg)
    {
        if (string.IsNullOrWhiteSpace(arg))
        {
            Console.WriteLine("Please input a site name to search");
        }
        var masterPassword = ReadInput.NonPrepopulated("Enter master password: ", hidden: true);
        
        var db = DatabaseFileService.LoadFile(DatabasePath.GetDefaultPath(), masterPassword);
        var matches = db.QuerySites(arg).ToList();
        
        if (matches.Count == 0) 
        {
            Console.WriteLine("No matching sites found");
        } else
        {
            Console.WriteLine();
        }

        foreach (var cred in matches)
        {
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine($"Site: {cred.Site}");
            Console.WriteLine($"Username: {cred.Username}");
            Console.WriteLine($"Password: {cred.Password}");
        }
    }
}