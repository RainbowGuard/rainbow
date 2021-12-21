using System.ComponentModel.DataAnnotations;

namespace Rainbow.Entities;

public class GuildConfiguration
{
    public const string DefaultPrefix = "!";

    [Key]
    public ulong Id { get; set; }

    public string Prefix { get; set; } = DefaultPrefix;
}