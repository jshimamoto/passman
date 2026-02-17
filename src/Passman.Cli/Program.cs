using Passman.Cli.Commands;

if (args.Length == 0)
{
    return;
}

var commandName = args[0];
var commandArgs = args.Skip(1).ToArray();

try
{
    switch (commandName)
    {
        case "init":
            var init = new InitCommand();
            init.Execute(commandArgs);
            break;

        case "add":
            var add = new AddCommand();
            add.Execute();
            break;
        
        default:
            Console.WriteLine($"Unknown command: {commandName}");
            break;
    }
} catch (Exception exception)
{
    Console.WriteLine($"Error: {exception.Message}");
}