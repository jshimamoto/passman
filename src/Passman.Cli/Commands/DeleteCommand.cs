using System.Text.RegularExpressions;
using Passman.Cli.Utils;
using Passman.Core.Storage;

namespace Passman.Cli.Commands;

public class DeleteCommand
{
    public void Execute(string arg)
    {
        if (string.IsNullOrWhiteSpace(arg))
        {
            Console.WriteLine("Please input a site name to search");
            return;
        }
        var masterPassword = ReadInput.Execute("Enter master password: ", hidden: true);
        
        var db = DatabaseFileService.LoadFile(DatabasePath.GetDefaultPath(), masterPassword);
        var matches = db.QuerySites(arg).ToList();

        int indexToDelete;

        if (matches.Count == 0) 
        {
            Console.WriteLine("No matching sites found");
            return;
        } else if (matches.Count == 1)
        {
            indexToDelete = 0;
            Console.WriteLine("\nOne matching site found: ");
            Console.WriteLine($"Site: {matches[0].Site}");
            Console.WriteLine($"Username: {matches[0].Username}");
            Console.WriteLine($"Password: {matches[0].Password}");
        } else
        {
            Console.WriteLine("Multiple matching sites found\n");
            for (int i = 0; i < matches.Count; i++)
            {
                Console.WriteLine($"Key: {i} -------------------------------------------");
                Console.WriteLine($"Site: {matches[i].Site}");
                Console.WriteLine($"Username: {matches[i].Username}");
                Console.WriteLine($"Password: {matches[i].Password}\n");
            }

            var indexString = ReadInput.Execute("\nEnter the key of the credential you wish to delete: ");
            if (!int.TryParse(indexString, out indexToDelete) || indexToDelete < 0 || indexToDelete >= matches.Count)
            {
                Console.WriteLine("Invalid index entered");
                return;
            }
        }
        
        bool confirmation = ConfirmAction.Execute($"\nDelete credential for {matches[indexToDelete].Site}?");
        if (confirmation) db.Remove(matches[indexToDelete]);
        DatabaseFileService.SaveFile(DatabasePath.GetDefaultPath(), masterPassword, db);
        return;
    }
}