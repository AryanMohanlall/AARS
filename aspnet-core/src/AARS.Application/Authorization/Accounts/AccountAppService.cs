using Abp.Configuration;
using Abp.Zero.Configuration;
using AARS.Authorization.Accounts.Dto;
using AARS.Authorization.Users;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace AARS.Authorization.Accounts;

public class AccountAppService : AARSAppServiceBase, IAccountAppService
{
    // from: http://regexlib.com/REDetails.aspx?regexp_id=1923
    public const string PasswordRegex = "(?=^.{8,}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\\s)[0-9a-zA-Z!@#$%^&*()]*$";

    private readonly UserRegistrationManager _userRegistrationManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AccountAppService(
        UserRegistrationManager userRegistrationManager,
        IHttpContextAccessor httpContextAccessor)
    {
        _userRegistrationManager = userRegistrationManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input)
    {
        var tenant = await TenantManager.FindByTenancyNameAsync(input.TenancyName);
        if (tenant == null)
        {
            return new IsTenantAvailableOutput(TenantAvailabilityState.NotFound);
        }

        if (!tenant.IsActive)
        {
            return new IsTenantAvailableOutput(TenantAvailabilityState.InActive);
        }

        return new IsTenantAvailableOutput(TenantAvailabilityState.Available, tenant.Id);
    }

    public async Task<RegisterOutput> Register(RegisterInput input)
    {
        // AbpSession.TenantId is null for anonymous requests (no JWT claims).
        // Resolve tenant from request context for anonymous register calls.
        int? tenantId = AbpSession.TenantId;
        if (!tenantId.HasValue)
        {
            var ctx = _httpContextAccessor.HttpContext;
            var headerVal = ctx?.Request.Headers["Abp.TenantId"].ToString();
            if (string.IsNullOrEmpty(headerVal))
            {
                headerVal = ctx?.Request.Headers["Abp-TenantId"].ToString();
            }

            if (!string.IsNullOrEmpty(headerVal) && int.TryParse(headerVal, out int hid))
            {
                tenantId = hid;
            }
            else
            {
                var queryTenantId = ctx?.Request.Query["tenantId"].ToString();
                if (!string.IsNullOrEmpty(queryTenantId) && int.TryParse(queryTenantId, out int qid))
                {
                    tenantId = qid;
                }
                else
                {
                    var queryTenancyName = ctx?.Request.Query["__tenant"].ToString();
                    if (!string.IsNullOrWhiteSpace(queryTenancyName))
                    {
                        var tenant = await TenantManager.FindByTenancyNameAsync(queryTenancyName);
                        if (tenant != null)
                        {
                            tenantId = tenant.Id;
                        }
                    }

                    if (!tenantId.HasValue)
                    {
                        var cookieVal = ctx?.Request.Cookies["Abp.TenantId"];
                        if (!string.IsNullOrEmpty(cookieVal) && int.TryParse(cookieVal, out int cid))
                        {
                            tenantId = cid;
                        }
                    }
                }
            }
        }

        using (tenantId.HasValue ? AbpSession.Use(tenantId, null) : null)
        {
            var user = await _userRegistrationManager.RegisterAsync(
                input.Name,
                input.Surname,
                input.EmailAddress,
                input.UserName,
                input.Password,
                true // Assumed email address is always confirmed. Change this if you want to implement email confirmation.
            );

            var isEmailConfirmationRequiredForLogin = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin);

            return new RegisterOutput
            {
                CanLogin = user.IsActive && (user.IsEmailConfirmed || !isEmailConfirmationRequiredForLogin)
            };
        }
    }
}
