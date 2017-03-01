using System;
using System.Collections.Generic;

namespace Domen.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string PersonName { get; set; }
        //public string AboutMe { get; set; }
        public string UserId { get; set; }
        public DateTime? Birthday { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public virtual Avatar Avatar { get; set; }
        public virtual ICollection<Contact> MemberContacts { get; set; }
        public virtual ICollection<Master> Skills { get; set; }
        public virtual ICollection<Brand> Brands { get; set; }
        public virtual PublicMasterInfo PublicMasterInfo { get; set; }
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

    public class ContactsViewModel
    {
        public List<Contact> Contacts { get; set; }
    }

    public class SkillViewModel
    {
        public IEnumerable<Specialists> Specialistses { get; set; }
        public IEnumerable<Master> Masters { get; set; }
    }
    public class PublicMasterInfo
    {
        public int Id { get; set; }
        public string Nikname { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string AboutMe { get; set; }
        public string Facebook { get; set; }
        public string Vkontakte { get; set; }
        public string Instagram { get; set; }
    }

    public class Contact
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
        public virtual Member Master { get; set; }
    }

    public class Avatar
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string FullPath { get; set; }
    }

    public class Requisits
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Inn { get; set; }
        public string Kpp { get; set; }
        public string FullReq { get; set; }
    }
    public class Brand
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual Avatar Avatar { get; set; }
        public virtual Member Member { get; set; }
    }
}