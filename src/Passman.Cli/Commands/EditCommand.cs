using Passman.Cli.Utils;
using Passman.Core.Models;
using Passman.Core.Storage;

namespace Passman.Cli.Commands;

public class EditCommand
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

        int indexToEdit = DisplayResults.DisplayCredentialsAndChooseOne(matches);

        if (indexToEdit == -1) return;
        
        bool confirmation = ConfirmAction.Execute($"\nEdit credential for {matches[indexToEdit].Site}?");
        if (confirmation) 
        {
            var newSite = ReadInput.Prepopulated("\nSite: ", matches[indexToEdit].Site);
            var newUsername = ReadInput.Prepopulated("Username: ", matches[indexToEdit].Username);
            var newPassword = ReadInput.Prepopulated("Password: ", matches[indexToEdit].Password);

            db.Update(matches[indexToEdit], newSite, newUsername, newPassword);
            // var cred = matches[indexToEdit];
            // cred.Site = newSite;
            // cred.Username = newUsername;
            // cred.Password = newPassword;
        }
        DatabaseFileService.SaveFile(DatabasePath.GetDefaultPath(), masterPassword, db);

        Console.WriteLine("\nCredential updated");
        return;
    }
}