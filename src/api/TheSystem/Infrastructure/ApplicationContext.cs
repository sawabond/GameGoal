using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class ApplicationContext : IdentityDbContext
        <
        AppUser,
        AppRole,
        string,
        IdentityUserClaim<string>,
        AppUserRole,
        IdentityUserLogin<string>,
        IdentityRoleClaim<string>,
        IdentityUserToken<string>
        >
    {
        public ApplicationContext(DbContextOptions options)
            : base(options)
        {

        }
    }
}