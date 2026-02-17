using System.Text;
using System.Text.Json;
using Passman.Core.Models;
using Passman.Core.Storage;

namespace Passman.Cli.Commands;

public class AddCommand
{
    public void Execute()
    {
        var site = ReadInput.Execute("Enter site name: ");
        var username = ReadInput.Execute("Enter username: ");
        var password = ReadInput.Execute("Enter password: ");

        Credential cred = new Credential
        {
            Site = site,
            Username = username,
            Password = password
        };

        var masterPassword = ReadInput.Execute("Enter master password: ", hidden: true);
        var database = DatabaseFileService.LoadFile(DatabasePath.GetDefaultPath(), masterPassword);
        database.Add(cred);
        DatabaseFileService.SaveFile(DatabasePath.GetDefaultPath(), masterPassword, database);
    }
}