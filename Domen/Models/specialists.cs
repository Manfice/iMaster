using System.Collections.Generic;

namespace Domen.Models
{
    public class Specialists
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<Master> Masters { get; set; }
    }

    public class Master
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual Specialists Specialist { get; set; }
    }
}