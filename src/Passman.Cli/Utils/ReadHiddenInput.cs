

using System.Text;

public static class ReadHiddenInput
{
    public static string Execute(string prompt)
    {
        Console.Write(prompt);
        var sb = new StringBuilder();

        while (true)
        {
            var key = Console.ReadKey(intercept: true);

            if (key.Key == ConsoleKey.Enter) break;
            if (key.Key == ConsoleKey.Backspace) 
            {
                sb.Length--;
                continue;
            }

            sb.Append(key.Key);
        }
        Console.WriteLine();

        return sb.ToString();
    }
}