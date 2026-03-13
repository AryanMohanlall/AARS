using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using AARS.Configuration;
using AARS.EntityFrameworkCore;
using AARS.Migrator.DependencyInjection;
using Castle.MicroKernel.Registration;
using Microsoft.Extensions.Configuration;

namespace AARS.Migrator;

[DependsOn(typeof(AARSEntityFrameworkModule))]
public class AARSMigratorModule : AbpModule
{
    private readonly IConfigurationRoot _appConfiguration;

    public AARSMigratorModule(AARSEntityFrameworkModule abpProjectNameEntityFrameworkModule)
    {
        abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

        _appConfiguration = AppConfigurations.Get(
            typeof(AARSMigratorModule).GetAssembly().GetDirectoryPathOrNull()
        );
    }

    public override void PreInitialize()
    {
        Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
            AARSConsts.ConnectionStringName
        );

        Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        Configuration.ReplaceService(
            typeof(IEventBus),
            () => IocManager.IocContainer.Register(
                Component.For<IEventBus>().Instance(NullEventBus.Instance)
            )
        );
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(AARSMigratorModule).GetAssembly());
        ServiceCollectionRegistrar.Register(IocManager);
    }
}
