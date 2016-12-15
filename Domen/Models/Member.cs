using System.Collections.Generic;

namespace Domen.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string PersonName { get; set; }
        public virtual ICollection<Contact> MemberContacts { get; set; }
    }

    public class Contact
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
    }
}