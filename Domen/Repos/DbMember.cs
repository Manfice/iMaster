using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public Task<Member> UpdateMemberAsync(Member member)
        {
            throw new NotImplementedException();
        }
    }
}