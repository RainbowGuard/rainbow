using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rainbow.Entities;

public class FlaggedUser
{
    [Key]
    public ulong Id { get; set; }

    public List<Guild> FlagGuilds { get; set; }

    public string FlagReason { get; set; }
}