using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using AARS.Authorization;

namespace AARS;

[DependsOn(
    typeof(AARSCoreModule),
    typeof(AbpAutoMapperModule))]
public class AARSApplicationModule : AbpModule
{
    public override void PreInitialize()
    {
        Configuration.Authorization.Providers.Add<AARSAuthorizationProvider>();
    }

    public override void Initialize()
    {
        var thisAssembly = typeof(AARSApplicationModule).GetAssembly();

        IocManager.RegisterAssemblyByConvention(thisAssembly);

        Configuration.Modules.AbpAutoMapper().Configurators.Add(
            // Scan the assembly for classes which inherit from AutoMapper.Profile
            cfg => cfg.AddMaps(thisAssembly)
        );
    }
}
