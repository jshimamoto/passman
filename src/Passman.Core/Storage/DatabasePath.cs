namespace Passman.Core.Storage;

public static class DatabasePath
{
    private const string AppFolder = "passman";
    private const string FileName = "passwords.db";

    public static string GetDefaultPath()
    {
        string baseDir;

        if (OperatingSystem.IsLinux())
        {
            baseDir = Environment.GetEnvironmentVariable("XDG_DATA_HOME")
                ?? Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                    ".local",
                    "share");
        }
        else
        {
            baseDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }
        
        var appDir = Path.Combine(baseDir, AppFolder);
        Directory.CreateDirectory(appDir);
        return Path.Combine(appDir, FileName);
    }
}