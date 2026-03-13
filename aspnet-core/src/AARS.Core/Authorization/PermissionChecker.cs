using Abp.Authorization;
using AARS.Authorization.Roles;
using AARS.Authorization.Users;

namespace AARS.Authorization;

public class PermissionChecker : PermissionChecker<Role, User>
{
    public PermissionChecker(UserManager userManager)
        : base(userManager)
    {
    }
}
