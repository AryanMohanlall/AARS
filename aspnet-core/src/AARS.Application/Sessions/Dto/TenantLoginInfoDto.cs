using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using AARS.MultiTenancy;

namespace AARS.Sessions.Dto;

[AutoMapFrom(typeof(Tenant))]
public class TenantLoginInfoDto : EntityDto
{
    public string TenancyName { get; set; }

    public string Name { get; set; }
}
