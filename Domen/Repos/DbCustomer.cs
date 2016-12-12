using Domen.Abstract;
using Domen.infrastructure;

namespace Domen.Repos
{
    public class DbCustomer:ICustomer
    {
         private readonly Context _context = new Context();
    }
}