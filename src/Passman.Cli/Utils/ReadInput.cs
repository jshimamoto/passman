

using System.Text;

public static class ReadInput
{
    public static string Execute(string prompt, bool hidden = false)
    {
        Console.Write(prompt);
        var sb = new StringBuilder();

        while (true)
        {
            var key = Console.ReadKey(intercept: hidden);

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