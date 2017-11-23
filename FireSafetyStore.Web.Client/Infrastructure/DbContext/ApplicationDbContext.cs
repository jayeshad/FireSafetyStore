using FireSafetyStore.Web.Client.Core;
using FireSafetyStore.Web.Client.Infrastructure.Security;
using FireSafetyStore.Web.Client.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace FireSafetyStore.Web.Client.Infrastructure.DbContext
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("FireSafetyDB", throwIfV1Schema: false)
        {
        }

        static ApplicationDbContext()
        {
            // Set the database intializer which is run once during application start
            // This seeds the database with admin user credentials and admin role
            Database.SetInitializer(new ApplicationDbInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}