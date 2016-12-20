using Domen.infrastructure;
using Domen.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Web.Models;

namespace Web.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Web.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context)); //new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            
            if (!roleManager.RoleExists("Admin"))
            {
                roleManager.Create(new ApplicationRole("Admin"));
            }

            const string userName = "manfice@yandex.ru";
            const string pass = "1q2w3eOP";
            const string mail = "manfice@yandex.ru";

            var user = userManager.FindByName(userName);
            if (user != null) return;
            var result = userManager.Create(new ApplicationUser { Email = mail, UserName = userName,Nickname = "Manfice"}, pass);
            if (!result.Succeeded) return;
            user = userManager.FindByName(userName);
            using (var db = new Context())
            {
                var member = new Member
                {
                    UserId = user.Id,
                    PersonName = user.Nickname
                };
                db.Members.Add(member);
                db.SaveChanges();
            }
            if (!userManager.IsInRole(user.Id, "Admin"))
            {
                userManager.AddToRole(user.Id, "Admin");
            }

        }
    }
}
