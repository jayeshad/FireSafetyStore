using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using System.Web;
using FireSafetyStore.Web.Client.Infrastructure.Security;
using FireSafetyStore.Web.Client.Infrastructure.DbContext;
using FireSafetyStore.Web.Client.Models;
using System.Linq;

namespace FireSafetyStore.Web.Client.Infrastructure.Common
{
    // This is useful if you do not want to tear down the database each time you run the application.
    // public class ApplicationDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    // This example shows you how to create a new database if the Model changes
    public class ApplicationDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext> 
    {
        protected override void Seed(ApplicationDbContext context)
        {
            InitializeIdentityForEF(context);
            base.Seed(context);
            
        }

        //Create User=Admin@Admin.com with password=Admin@123456 in the Admin role        
        public static void InitializeIdentityForEF(ApplicationDbContext db)
        {
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var roleManager = HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
            var systemRoles = new[] { "Admin", "Employee", "Customer" };
            const string name = "admin@firesafe.com";
            const string password = "Admin@123";
            
            //Create Role Admin if it does not exist
            systemRoles.ToList().ForEach(x => {
                var role = roleManager.FindByName(x);
                if (role == null)
                {
                    role = new IdentityRole(x);
                    var roleresult = roleManager.Create(role);
                }
            });
            
            

            var user = userManager.FindByName(name);
            if (user == null) {
                user = new ApplicationUser { UserName = name, Email = name };
                var result = userManager.Create(user, password);
                result = userManager.SetLockoutEnabled(user.Id, false);
            }

            // Add user admin to Role Admin if not already added
            IdentityRole adminRole = roleManager.FindByName(FireSafetyAppConstants.AdminRoleName);            
            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(adminRole.Name)) {
                var result = userManager.AddToRole(user.Id, adminRole.Name);
            }
        }
    }
}