using Abp.Application.Services;
using AARS.MultiTenancy.Dto;

namespace AARS.MultiTenancy;

public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
{
}

