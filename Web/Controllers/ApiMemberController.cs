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
            DateTime myD;
            DateTime.TryParse(member.Birthday,out myD);
            return Ok("Cool");
        }
    }
}
