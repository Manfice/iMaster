using System.Data.Entity;
using Domen.Models;

namespace Domen.infrastructure
{
    public class Context:DbContext
    {
        public Context():base("iMaster")
        {
            Database.SetInitializer(new NullDatabaseInitializer<Context>());
        }

        public static Context Create()
        {
            return new Context();
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Avatar> Avatars { get; set; }
    }
}