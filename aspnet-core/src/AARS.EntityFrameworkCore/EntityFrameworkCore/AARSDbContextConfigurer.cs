using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace AARS.EntityFrameworkCore;

public static class AARSDbContextConfigurer
{
    public static void Configure(DbContextOptionsBuilder<AARSDbContext> builder, string connectionString)
    {
        builder.UseNpgsql(connectionString);
    }

    public static void Configure(DbContextOptionsBuilder<AARSDbContext> builder, DbConnection connection)
    {
        builder.UseNpgsql(connection);
    }
}
