using Abp.Modules;
using Abp.Reflection.Extensions;
using AARS.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace AARS.Web.Host.Startup
{
    [DependsOn(
       typeof(AARSWebCoreModule))]
    public class AARSWebHostModule : AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public AARSWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AARSWebHostModule).GetAssembly());
        }
    }
}
