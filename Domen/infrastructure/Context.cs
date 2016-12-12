using System.Data.Entity;

namespace Domen.infrastructure
{
    public class Context:DbContext
    {
        public Context():base("iMaster")
        {
        }

    }
}