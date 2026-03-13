using Abp.Application.Services;
using AARS.Sessions.Dto;
using System.Threading.Tasks;

namespace AARS.Sessions;

public interface ISessionAppService : IApplicationService
{
    Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
}
