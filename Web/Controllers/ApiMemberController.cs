using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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
        public IHttpActionResult GetMember()
        {
            var uId = User.Identity.GetUserId();
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
                AboutMe = member.AboutMe,
                Birthday = myD,
                Email = member.Email,
                PersonName = member.PersonName,
                Phone = member.Phone,
                UserId = uId
            };
            var result = await _member.UpdateMemberAsync(m);

            return result!=null? Ok(result):(IHttpActionResult) BadRequest("Something Wrong");
        }
    }
}
