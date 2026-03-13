using Abp.Zero.EntityFrameworkCore;
using AARS.Authorization.Roles;
using AARS.Authorization.Users;
using AARS.MultiTenancy;
using Microsoft.EntityFrameworkCore;

namespace AARS.EntityFrameworkCore;

public class AARSDbContext : AbpZeroDbContext<Tenant, Role, User, AARSDbContext>
{
    /* Define a DbSet for each entity of the application */

    public AARSDbContext(DbContextOptions<AARSDbContext> options)
        : base(options)
    {
    }
}
