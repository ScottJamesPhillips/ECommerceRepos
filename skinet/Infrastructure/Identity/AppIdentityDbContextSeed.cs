using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Scott",
                    UserName = "ScottJPhill",
                    Email = "sjp0110@hotmail.com",
                    Address = new Address
                    {
                        FirstName = "Scott",
                        LastName = "Phillips",
                        Street = "Louiz Rd",
                        City = "Lent",
                        State = "N/A",
                        ZipCode = "GW3 NTS",

                    }
                };
                await userManager.CreateAsync(user, "P4$$w0rd");
            }
        }
    }
}