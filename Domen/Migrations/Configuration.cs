using Domen.infrastructure;
using Domen.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Web;

namespace Domen.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Domen.infrastructure.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Domen.infrastructure.Context context)
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
            var result = userManager.Create(new ApplicationUser { Email = mail, UserName = userName, Nickname = "Manfice" }, pass);
            if (!result.Succeeded) return;
            user = userManager.FindByName(userName);
            using (var db = new Context())
            {
                var dbMember = db.Members.Where(member1 => member1.UserId.Equals(user.Id)).ToList();
                if (!dbMember.Any())
                {
                    var member = new Member
                    {
                        UserId = user.Id,
                        PersonName = user.Nickname
                    };
                    db.Members.Add(member);
                    db.SaveChanges();
                }
            }
            if (!userManager.IsInRole(user.Id, "Admin"))
            {
                userManager.AddToRole(user.Id, "Admin");
            }

        }
    }
}

