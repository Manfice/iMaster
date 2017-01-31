using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Domen.Abstract;
using Domen.Models;
using Microsoft.AspNet.Identity;

namespace Web.Controllers
{
    public class ApiMemberController : ApiController
    {
        private readonly IMember _member;

        public ApiMemberController(IMember member)
        {
            _member = member;
        }

        [HttpGet]
        public IHttpActionResult GetMasterPublicInfo()
        {
            var info = _member.GetMemberByUserId(User.Identity.GetUserId());
            return info != null ? Ok(info) : (IHttpActionResult) BadRequest("Мастер не авторизован в системе!");
        }
        [HttpGet]
        public IHttpActionResult GetMember()
        {
            return Ok(_member.GetMemberByUserId(User.Identity.GetUserId()));
        }

        public async Task<IHttpActionResult> UpadteMemberInfo(SaveMemberPrimeData member)
        {
            var d = Request.Content;
            var myD = DateTime.Parse(member.Birthday);
            DateTime.TryParse(member.Birthday,out myD);
            var uId = User.Identity.GetUserId();
            var m = new Member
            {
                City = member.City,
                Country = member.Country,
                //AboutMe = member.AboutMe,
                Birthday = myD,
                Email = member.Email,
                PersonName = member.PersonName,
                Phone = member.Phone,
                UserId = uId
            };
            var result = await _member.UpdateMemberAsync(m);

            return result!=null? Ok(result):(IHttpActionResult) BadRequest("Something Wrong");
        }

        public async Task<IHttpActionResult> UpdatePublicInfo(PublicMasterInfo model)
        {
            if (User.Identity.IsAuthenticated)
            {
                var result = await _member.UpdatePublicMasterInfoAsync(model);
                return result!=null? Ok(result):(IHttpActionResult)BadRequest($"Error 4000-{User.Identity.Name}");
            }
            else
            {
                return (IHttpActionResult)BadRequest("Ошибка авторизации");
            }
        }
        [HttpPost]
        public async Task<IHttpActionResult> UploadAvatar()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return (IHttpActionResult) BadRequest("Auth fail");
            }
            var userId = User.Identity.GetUserId();
            var cnt = HttpContext.Current.Request.Files["avatar"];
            if (cnt==null || cnt.ContentLength<=0)
            {
                return (IHttpActionResult) BadRequest("Ошибка передачи файла.");
            }
            var root = HttpContext.Current.Server.MapPath("~/MasterUploads/"+userId);
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }
            var fileName = $"avatar-{User.Identity.GetUserId()}-{DateTime.Now.Millisecond}.png";
            var mA = new Avatar
            {
                FullPath = root+"/"+fileName,
                Path = $"/MasterUploads/{User.Identity.GetUserId()}/{fileName}"
            };
            var fl = _member.GetAvatarFilePath(userId);
            if (System.IO.File.Exists(fl))
            {
                File.Delete(fl);
            }
            cnt.SaveAs(root+"/"+fileName);
            var result = await _member.SaveAvatarAsync(userId, mA);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IHttpActionResult> UpdateContacts(List<Contact> model)
        {
            if (!model.Any())
            {
                return (IHttpActionResult) BadRequest();
            }

            var user = User.Identity.GetUserId();
            var result = await _member.UpdateContactsAsync(user, new ContactsViewModel {Contacts = model});
            return Ok(result.Contacts.Select(c=>new {c.Id, c.Title, c.Value}));
        }
        [HttpPost]
        public async Task<IHttpActionResult> DeleteContact(int id=0)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return (IHttpActionResult) BadRequest();
            }
            if (id==0)
            {
                return (IHttpActionResult) BadRequest();
            }
            var user = User.Identity.GetUserId();
            var result = await _member.DeleteContact(id,user);
            return Ok(result.Contacts.Select(c => new { c.Id, c.Title, c.Value }));
        } 
    }
}
