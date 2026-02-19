using System.Security.Cryptography;
using Passman.Cli.Attributes;
using Passman.Core.Crypto;

namespace Passman.Cli.Commands;

[Help(
    "generate",
    "Generate a random strong password",
    "passman generate"
)]
public class GenerateCommand
{
    public void Execute()
    {
        var newPassword = PasswordGenerator.Generate();
        Console.WriteLine(newPassword);
    }
}