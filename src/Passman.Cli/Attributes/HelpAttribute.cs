namespace Passman.Cli.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class HelpAttribute : Attribute
{
    public string Name { get; }
    public string Description { get; }
    public string Usage { get; }
    public string[] Options { get; }

    public HelpAttribute(
        string name,
        string description,
        string usage,
        string[]? options = null
    )
    {
        Name = name;
        Description = description;
        Usage = usage;
        Options = options ?? Array.Empty<string>();
    }
}