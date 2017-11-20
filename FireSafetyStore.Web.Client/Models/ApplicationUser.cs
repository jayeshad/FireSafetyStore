using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentitySample.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public string Address { get; set; }
        //public string State { get; set; }
        //public string City { get; set; }

        //[Display(Name = "Postal Code")]
        //public string PostalCode { get; set; }
        //public string DisplayAddress
        //{
        //    get
        //    {
        //        string displayAddress = string.IsNullOrWhiteSpace(this.Address) ? "" : this.Address;
        //        string city = string.IsNullOrWhiteSpace(this.City) ? "" : this.City;
        //        string state = string.IsNullOrWhiteSpace(this.State) ? "" : this.State;
        //        string postalcode = string.IsNullOrWhiteSpace(this.PostalCode) ? "" : this.PostalCode;
        //        return string.Format("{0} {1} {2} {3}", displayAddress, city, state, postalcode);
        //    }
        //}


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}