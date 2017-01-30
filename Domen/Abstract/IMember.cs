using System.Collections.Generic;
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
        Task<Member> GetMemberByUserIdAsync(string id);
        Member GetMemberByUserId(string id);
        PublicMasterInfo GetPublicMasterInfo(string id);
        Task<PublicMasterInfo> GetPublicMasterInfoAsync(string id);
        PublicMasterInfo UpdatePublicMasterInfo(PublicMasterInfo model);
        Task<PublicMasterInfo> UpdatePublicMasterInfoAsync(PublicMasterInfo model);
        Avatar SaveAvatar(string id,Avatar model);
        Task<Avatar> SaveAvatarAsync(string id, Avatar model);
        string GetAvatarFilePath(string id);
        Task<ContactsViewModel> UpdateContactsAsync(string id, ContactsViewModel moedl);
    }
}