namespace Rainbow.Entities;

public class GuildConfiguration
{
    public const string DefaultPrefix = "!";

    public ulong Id { get; set; }

    public string Prefix { get; set; } = DefaultPrefix;
}