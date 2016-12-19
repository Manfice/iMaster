using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Domen.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public ApplicationUser()
        {
            CreateDate = DateTime.Now;
            Member = new Member();
        }

        public string Nickname { get; set; }
        public Member Member { get; set; }
        public DateTime CreateDate { get; set; }
    }

    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole():base(){}

        public ApplicationRole(string name):base(name){}
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("iMaster")
        {
            Database.SetInitializer(new NullDatabaseInitializer<ApplicationDbContext>());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}