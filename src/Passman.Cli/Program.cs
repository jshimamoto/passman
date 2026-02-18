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

        case "search":
            var search = new SearchCommand();
            search.Execute(commandArgs[0]);
            break;

        case "delete":
            var delete = new DeleteCommand();
            delete.Execute(commandArgs[0]);
            break;
        
        default:
            Console.WriteLine($"Unknown command: {commandName}");
            break;
    }
} catch (Exception exception)
{
    Console.WriteLine($"Error: {exception.Message}");
}