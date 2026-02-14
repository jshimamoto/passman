using Passman.Cli.Commands;

if (args.Length == 0)
{
    return;
}

var commandName = args[0];
var commandArgs = args.Skip(1).ToArray();

switch (commandName)
{
    case "init":
        var init = new InitCommand();
        init.Execute(commandArgs);
        break;
    
    default:
        Console.WriteLine($"Unknown command: {commandName}");
        break;
}