using Passman.Core.Models;

namespace Passman.Core.Utils;

public class CsvImporter
{
    public static List<Credential> ImportCsv (string path)
    {
        var lines = File.ReadAllLines(path);
        if (lines.Length == 0) return new List<Credential>();

        var results = new List<Credential>();
        foreach (string line in lines.Skip(1))
        {
            var parts = line.Split(",");
            if (parts.Length != 3) continue;

            results.Add(new Credential
            {
                Site = parts[0].Trim(),
                Username = parts[1].Trim(),
                Password = parts[2].Trim()
            });
        } 

        return results;
    }
}