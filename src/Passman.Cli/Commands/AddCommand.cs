using System.Text;
using System.Text.Json;
using Passman.Core.Models;
using Passman.Core.Storage;

namespace Passman.Cli.Commands;

public class AddCommand
{
    public void Execute()
    {
        var site = ReadInput.NonPrepopulated("Enter site name: ");
        var username = ReadInput.NonPrepopulated("Enter username: ");
        var password = ReadInput.NonPrepopulated("Enter password: ");

        Credential cred = new Credential
        {
            Site = site,
            Username = username,
            Password = password
        };

        var masterPassword = ReadInput.NonPrepopulated("Enter master password: ", hidden: true);
        var database = DatabaseFileService.LoadFile(DatabasePath.GetDefaultPath(), masterPassword);
        database.Add(cred);
        DatabaseFileService.SaveFile(DatabasePath.GetDefaultPath(), masterPassword, database);
    }
}