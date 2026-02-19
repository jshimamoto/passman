using System.Reflection;
using System.Runtime.InteropServices;
using Passman.Cli.Attributes;
using Passman.Cli.Reflection;

namespace Passman.Cli.Commands;

[Help(
    "help",
    "Displays help information about available commands",
    "passman help",
    options: new[] {"<command name> : Name of command (Ex: passman help init)"}
)]
public class HelpCommand
{
    public void Execute(string[] args)
    {
        var commands = CommandLoader.GetHelpMessages();
        string? commandName = null;

        if (args.Length > 0) commandName = args[0];

        if (string.IsNullOrWhiteSpace(commandName))
        {
            Console.WriteLine("Available commands:");
            foreach (var kvp in commands)
            {
                var attr = kvp.Value.GetCustomAttribute<HelpAttribute>()!;
                Console.WriteLine($"    {attr.Name, -10} {attr.Description}");
            }

            return;
        }

        if (commands.TryGetValue(commandName, out var type))
        {
            var attr = type.GetCustomAttribute<HelpAttribute>()!;
            Console.WriteLine($"{attr.Name}");
            Console.WriteLine(new string('-', attr.Name.Length));
            Console.WriteLine(attr.Description);
            if (attr.Options.Length > 0)
            {
                Console.WriteLine();
                foreach (var option in attr.Options)
                {
                    Console.WriteLine(option);
                }
            }
        }
    }
}