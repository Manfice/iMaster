using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domen.Abstract;
using Domen.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Web.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        private readonly IMember _member;
        public MemberController(HttpContext context, IMember member)
        {
            _member = member;
        }

        public ActionResult Master()
        {
            return View(_member.GetMemberByUserId(User.Identity.GetUserId()));
        }

        public ActionResult UserProfile()
        {
            var members = _member.GetMemberByUserId(User.Identity.GetUserId());
            return PartialView(members);
        }

        public ActionResult Settings()
        {
            var members = _member.GetMemberByUserId(User.Identity.GetUserId());

            return View(members);
        }

        // GET: Member
        //public ActionResult Index()
        //{
        //    return View();
        //}

    }
}