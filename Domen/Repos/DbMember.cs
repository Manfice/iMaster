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

        public Task<Member> DeleteMemberAsync(int id)
        {
            throw new NotImplementedException();
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

        public Task<Member> GetMemberByUserIdAsync(string id)
        {
            return
                _context.Members.FirstOrDefaultAsync(
                    member => member.UserId.Equals(id, StringComparison.CurrentCultureIgnoreCase));
        }

        public async Task<Member> UpdateMemberAsync(Member member)
        {
            var db = await _context.Members.FirstOrDefaultAsync(member1 => member1.UserId==member.UserId);
            if (db==null)
            {
                return null;
            }
            db.AboutMe = member.AboutMe;
            db.PersonName = member.PersonName;
            db.Birthday = member.Birthday;
            db.City = member.City;
            db.Country = member.Country;
            db.Email = member.Email;
            db.Phone = member.Phone;
            await _context.SaveChangesAsync();
            return db;
        }
    }
}