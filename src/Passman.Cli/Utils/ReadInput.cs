

using System.Text;

public static class ReadInput
{
    public static string NonPrepopulated(string prompt, bool hidden = false)
    {
        Console.Write(prompt);
        var sb = new StringBuilder();

        while (true)
        {
            var key = Console.ReadKey(intercept: hidden);

            if (key.Key == ConsoleKey.Enter) break;
            if (key.Key == ConsoleKey.Backspace)
            {
                if (sb.Length == 0)
                    continue;

                sb.Length--;

                Console.CursorLeft--;
                Console.Write(' ');
                Console.CursorLeft--;
                continue;
            }

            sb.Append(key.KeyChar);
        }
        Console.WriteLine();

        return sb.ToString();
    }

    public static string Prepopulated(string prompt, string initialValue = "", bool hidden = false)
    {
        Console.Write(prompt);

        var buffer = new StringBuilder(initialValue);

        if (hidden)
            Console.Write(new string('*', buffer.Length));
        else
            Console.Write(initialValue);

        int cursorLeft = Console.CursorLeft;

        while (true)
        {
            var key = Console.ReadKey(intercept: true);

            if (key.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();
                return buffer.ToString();
            }

            if (key.Key == ConsoleKey.Backspace)
            {
                if (buffer.Length == 0)
                    continue;

                buffer.Length--;

                Console.CursorLeft--;
                Console.Write(' ');
                Console.CursorLeft--;
                continue;
            }

            if (!char.IsControl(key.KeyChar))
            {
                buffer.Append(key.KeyChar);

                if (hidden)
                    Console.Write('*');
                else
                    Console.Write(key.KeyChar);
            }
        }
    }
}