﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Domen.Models;

namespace Domen.Abstract
{
    public interface IMember
    {
        IEnumerable<Member> GetMembers { get; }
        Task<Member> UpdateMemberAsync(Member member);
        Task<Member> DeleteMemberAsync(int id);
        Task<Member> GetMemberByIdAsync(int id);
        Member GetMemberById(int id);
    }
}