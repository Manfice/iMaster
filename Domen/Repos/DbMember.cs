using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Domen.Abstract;
using Domen.infrastructure;
using Domen.Models;

namespace Domen.Repos
{
    public class DbMember : IMember
    {
        private readonly Context _context = Context.Create();

        public IEnumerable<Member> GetMembers => _context.Members;

        public async Task<ContactsViewModel> DeleteContact(int id, string userId)
        {
            var s = _context.Members.FirstOrDefault(member => member.UserId.Equals(userId, StringComparison.CurrentCultureIgnoreCase));
            if (s==null)
            {
                return null;
            }
            var c = await _context.Contacts.FindAsync(id);
            if (c==null)
            {
                return null;
            }
            _context.Contacts.Remove(c);
            await _context.SaveChangesAsync();
            return new ContactsViewModel {Contacts = s.MemberContacts.ToList()};
        }

        public Task<Member> DeleteMemberAsync(int id)
        {
            throw new NotImplementedException();
        }

        public string GetAvatarFilePath(string id)
        {
            var s =
                _context.Members.FirstOrDefault(
                    member => member.UserId.Equals(id, StringComparison.CurrentCultureIgnoreCase));
            return s?.Avatar.FullPath;
        }

        public Member GetMemberById(int id)
        {
            return _context.Members.Find(id);
        }

        public async Task<Member> GetMemberByIdAsync(int id)
        {
            return await _context.Members.FindAsync(id);
        }

        public Member GetMemberByUserId(string id)
        {
            return _context.Members.FirstOrDefault(m => m.UserId.Equals(id, StringComparison.CurrentCultureIgnoreCase));
        }

        public async Task<Member> GetMemberByUserIdAsync(string id)
        {
            return await 
                _context.Members.FirstOrDefaultAsync(
                    member => member.UserId.Equals(id, StringComparison.CurrentCultureIgnoreCase));
        }

        public PublicMasterInfo GetPublicMasterInfo(string id)
        {
            return
                _context.Members.FirstOrDefault(
                    master => master.UserId.Equals(id, StringComparison.CurrentCultureIgnoreCase))?.PublicMasterInfo;
        }

        public async Task<PublicMasterInfo> GetPublicMasterInfoAsync(string id)
        {
            var m = await
                _context.Members.FirstOrDefaultAsync(
                    member => member.UserId.Equals(id, StringComparison.CurrentCultureIgnoreCase));
            return m.PublicMasterInfo;

        }

        public Avatar SaveAvatar(string id, Avatar model)
        {
            var db = _context.Members.FirstOrDefault(member => member.UserId.Equals(id, StringComparison.CurrentCultureIgnoreCase));
            if (db == null)
            {
                return null;
            }
            if (db.Avatar != null)
            {
                _context.Avatars.Remove(db.Avatar);
            }
            db.Avatar = model;
            _context.SaveChanges();
            return db.Avatar;
        }

        public async Task<Avatar> SaveAvatarAsync(string id, Avatar model)
        {
            var db = _context.Members.FirstOrDefault(member => member.UserId.Equals(id,StringComparison.CurrentCultureIgnoreCase));
            if (db==null)
            {
                return null;
            }
            db.Avatar = model;
            await _context.SaveChangesAsync();
            return db.Avatar;
        }

        public async Task<ContactsViewModel> UpdateContactsAsync(string id, ContactsViewModel moedl)
        {
            var m = await 
                _context.Members.FirstOrDefaultAsync(
                    member => member.UserId.Equals(id, StringComparison.CurrentCultureIgnoreCase));
            foreach (var contact in moedl.Contacts)
            {
                if (contact.Id==0)
                {
                    m.MemberContacts.Add(contact);
                }
                else
                {
                    var c = _context.Contacts.Find(contact.Id);
                    if (c == null) continue;
                    c.Value = contact.Value;
                    _context.SaveChanges();
                }
            }
            await _context.SaveChangesAsync();
            var result = new ContactsViewModel
            {
                Contacts = m.MemberContacts.ToList()
            };
            return result;
        }

        public async Task<Member> UpdateMemberAsync(Member member)
        {
            var db = await _context.Members.FirstOrDefaultAsync(member1 => member1.UserId==member.UserId);
            if (db==null)
            {
                return null;
            }
            //db.AboutMe = member.AboutMe;
            db.PersonName = member.PersonName;
            db.Birthday = member.Birthday;
            db.City = member.City;
            db.Country = member.Country;
            db.Email = member.Email;
            db.Phone = member.Phone;
            await _context.SaveChangesAsync();
            return db;
        }

        public PublicMasterInfo UpdatePublicMasterInfo(PublicMasterInfo model)
        {
            var i = _context.PublicMasterInfos.Find(model.Id);
            if (i==null)
            {
                return null;
            }
            i.AboutMe = model.AboutMe;
            i.City = model.City;
            i.Country = model.Country;
            i.Nikname = model.Nikname;
            i.Facebook = model.Facebook;
            i.Vkontakte = model.Vkontakte;
            i.Instagram = model.Instagram;
            _context.SaveChanges();
            return i;
        }

        public async Task<PublicMasterInfo> UpdatePublicMasterInfoAsync(PublicMasterInfo model)
        {
            var i = await _context.PublicMasterInfos.FindAsync(model.Id);
            if (i == null)
            {
                return null;
            }
            i.AboutMe = model.AboutMe;
            i.City = model.City;
            i.Country = model.Country;
            i.Nikname = model.Nikname;
            i.Facebook = model.Facebook;
            i.Vkontakte = model.Vkontakte;
            i.Instagram = model.Instagram;
             await _context.SaveChangesAsync();
            return i;
        }
    }
}