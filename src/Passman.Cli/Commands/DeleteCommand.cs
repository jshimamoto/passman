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
        var masterPassword = ReadInput.NonPrepopulated("Enter master password: ", hidden: true);
        
        var db = DatabaseFileService.LoadFile(DatabasePath.GetDefaultPath(), masterPassword);
        var matches = db.QuerySites(arg).ToList();

        int indexToDelete = DisplayResults.DisplayCredentialsAndChooseOne(matches);

        if (indexToDelete == -1) return;
        
        bool confirmation = ConfirmAction.Execute($"\nDelete credential for {matches[indexToDelete].Site}?");
        if (confirmation) db.Remove(matches[indexToDelete]);
        DatabaseFileService.SaveFile(DatabasePath.GetDefaultPath(), masterPassword, db);
        return;
    }
}