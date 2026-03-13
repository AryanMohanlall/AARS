using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using AARS.EntityFrameworkCore;
using AARS.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace AARS.Web.Tests;

[DependsOn(
    typeof(AARSWebMvcModule),
    typeof(AbpAspNetCoreTestBaseModule)
)]
public class AARSWebTestModule : AbpModule
{
    public AARSWebTestModule(AARSEntityFrameworkModule abpProjectNameEntityFrameworkModule)
    {
        abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
    }

    public override void PreInitialize()
    {
        Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(AARSWebTestModule).GetAssembly());
    }

    public override void PostInitialize()
    {
        IocManager.Resolve<ApplicationPartManager>()
            .AddApplicationPartsIfNotAddedBefore(typeof(AARSWebMvcModule).Assembly);
    }
}