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

    public void Update(Credential target, string newSite, string newUser, string newPass)
    {
        target.Site = newSite;
        target.Username = newUser;
        target.Password = newPass;
    }

    public void Remove(Credential credential)
    {
        Credentials.Remove(credential);
    }

    public IEnumerable<Credential> QuerySites(string query)
    {
        return Credentials.Where(c => 
            c.Site != null && 
            c.Site.Contains(query, StringComparison.OrdinalIgnoreCase)
        );
    }
}