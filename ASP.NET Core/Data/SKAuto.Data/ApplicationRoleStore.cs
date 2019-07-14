namespace SKAuto.Data
{
    using System.Security.Claims;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    using SKAuto.Data.Models;

    public class ApplicationRoleStore : RoleStore<
        ApplicationRole,
        SKAutoDbContext,
        string,
        IdentityUserRole<string>,
        IdentityRoleClaim<string>>
    {
        public ApplicationRoleStore(SKAutoDbContext context, IdentityErrorDescriber describer = null)
            : base(context, describer)
        {
        }

        protected override IdentityRoleClaim<string> CreateRoleClaim(ApplicationRole role, Claim claim) =>
            new IdentityRoleClaim<string>
            {
                RoleId = role.Id,
                ClaimType = claim.Type,
                ClaimValue = claim.Value,
            };
    }
}
