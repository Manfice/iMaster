using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Domen.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Domen.infrastructure
{
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
        }

        public string Nickname { get; set; }
        public DateTime CreateDate { get; set; }
    }

    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }

        public ApplicationRole(string name) : base(name) { }
    }

    public class Context : IdentityDbContext<ApplicationUser>
    {
        public Context()
            : base("iMaster")
        {
            Database.SetInitializer(new NullDatabaseInitializer<Context>());
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Avatar> Avatars { get; set; }
        public DbSet<Specialists> Specialists { get; set; }
        public DbSet<Master> Masters { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<PublicMasterInfo> PublicMasterInfos { get; set; }

        public static Context Create()
        {
            return new Context();
        }
    }
}
