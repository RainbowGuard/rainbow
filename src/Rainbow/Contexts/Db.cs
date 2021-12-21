using Microsoft.EntityFrameworkCore;
using System;

namespace Rainbow.Contexts;

public static class Db
{
    public const string ConnectionString = "Database=rainbow;Server=localhost;Port=3306;UID=rainbow;PWD=rainbow;";

    public static readonly MariaDbServerVersion Version = new MariaDbServerVersion(new Version(10, 5, 9));
}