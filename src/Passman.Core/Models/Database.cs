namespace Passman.Core.Models;

public class Database
{
    public List<Credential> Credentials { get; set; } = new();

    public void Add(Credential credential)
    {
        Credentials.Add(credential);
    }

    public void Add(List<Credential> credentials)
    {
        Credentials.AddRange(credentials);
    }
}