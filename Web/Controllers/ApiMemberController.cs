using System;
using System.Collections.Generic;
using System.Globalization;
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
            var info = _member.GetPublicMasterInfo(User.Identity.GetUserId());
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

        public async Task<IHttpActionResult> UploadAvatar(HttpPostedFileBase ava)
        {
            return Ok();
        } 
    }
}
