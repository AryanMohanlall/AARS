using Abp.Authorization;
using Abp.Runtime.Session;
using AARS.Configuration.Dto;
using System.Threading.Tasks;

namespace AARS.Configuration;

[AbpAuthorize]
public class ConfigurationAppService : AARSAppServiceBase, IConfigurationAppService
{
    public async Task ChangeUiTheme(ChangeUiThemeInput input)
    {
        await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
    }
}
