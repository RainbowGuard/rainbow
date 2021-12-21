using System.ComponentModel.DataAnnotations;

namespace Rainbow.Entities;

public class FlaggedUser
{
    [Key]
    public ulong Id { get; set; }

    public string Reason { get; set; }
}