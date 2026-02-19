using Passman.Core.Models;

namespace Passman.Cli.Utils;

public static class DisplayResults
{
    public static int DisplayCredentialsAndChooseOne(List<Credential> credentials)
    {
        int targetIndex;
        if (credentials.Count == 0) 
        {
            Console.WriteLine("No matching sites found");
            return -1;
        } else if (credentials.Count == 1)
        {
            targetIndex = 0;
            Console.WriteLine("\nOne matching site found: ");
            Console.WriteLine($"Site: {credentials[0].Site}");
            Console.WriteLine($"Username: {credentials[0].Username}");
            Console.WriteLine($"Password: {credentials[0].Password}");
            return targetIndex;
        } else
        {
            Console.WriteLine("Multiple matching sites found\n");
            for (int i = 0; i < credentials.Count; i++)
            {
                Console.WriteLine($"Key: {i} -------------------------------------------");
                Console.WriteLine($"Site: {credentials[i].Site}");
                Console.WriteLine($"Username: {credentials[i].Username}");
                Console.WriteLine($"Password: {credentials[i].Password}\n");
            }

            var indexString = ReadInput.NonPrepopulated("\nEnter the key of the credential you wish to target: ");
            if (!int.TryParse(indexString, out targetIndex) || targetIndex < 0 || targetIndex >= credentials.Count)
            {
                Console.WriteLine("Invalid index entered");
                return -1;
            }

            return targetIndex;
        }
    }
}