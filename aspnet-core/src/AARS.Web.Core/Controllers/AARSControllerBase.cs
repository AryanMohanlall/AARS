using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace AARS.Controllers
{
    public abstract class AARSControllerBase : AbpController
    {
        protected AARSControllerBase()
        {
            LocalizationSourceName = AARSConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
