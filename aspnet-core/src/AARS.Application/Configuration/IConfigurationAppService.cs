using AARS.Configuration.Dto;
using System.Threading.Tasks;

namespace AARS.Configuration;

public interface IConfigurationAppService
{
    Task ChangeUiTheme(ChangeUiThemeInput input);
}
