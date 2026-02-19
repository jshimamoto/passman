using System.Reflection;
using Passman.Cli.Attributes;

namespace Passman.Cli.Reflection;

public static class CommandLoader
{
    public static Dictionary<string, Type> GetHelpMessages()
    {
        var commands = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase);

        var assembly = Assembly.GetExecutingAssembly();

        foreach (var type in assembly.GetTypes())
        {
            var attr = type.GetCustomAttribute<HelpAttribute>();
            if (attr != null) commands[attr.Name] = type;
        }

        return commands;
    }
}