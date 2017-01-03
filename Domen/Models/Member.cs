using System;
using System.Collections.Generic;

namespace Domen.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string PersonName { get; set; }
        public string AboutMe { get; set; }
        public string UserId { get; set; }
        public DateTime Birthday { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public virtual Avatar Avatar { get; set; }
        public virtual ICollection<Contact> MemberContacts { get; set; }
        public virtual Master Master { get; set; }
        public virtual ICollection<Brand> Brands { get; set; }
    }

    public class SaveMemberPrimeData
    {
        public string PersonName { get; set; }
        public string AboutMe { get; set; }
        public string Birthday { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

    }


    public class Contact
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
    }

    public class Avatar
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string FullPath { get; set; }
    }

    public class Brand
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual Avatar Avatar { get; set; }
        public virtual Member Member { get; set; }
    }
}