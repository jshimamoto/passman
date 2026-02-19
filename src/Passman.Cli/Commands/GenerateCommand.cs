using System.Security.Cryptography;
using Passman.Core.Crypto;

namespace Passman.Cli.Commands;

public class GenerateCommand
{
    public void Execute()
    {
        var newPassword = PasswordGenerator.Generate();
        Console.WriteLine(newPassword);
    }
}