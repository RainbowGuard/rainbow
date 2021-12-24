using System.ComponentModel.DataAnnotations;

namespace Rainbow.Entities;

public class Guild
{
    [Key]
    public ulong Id { get; set; }
}