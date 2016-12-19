using System.Web;
using System.Web.Mvc;
using Domen.Abstract;
using Domen.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Web.Controllers
{
    public class MemberController : Controller
    {
        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;
        private readonly IMember _member;
        public MemberController(IMember member)
        {
            _member = member;
        }

        public ActionResult UserProfile()
        {
            var user = UserManager.FindByEmail(User.Identity.Name);
            Member member = null;
            if (user!=null)
            {
                member = _member.GetMemberById(user.Member.Id);
            }
            return PartialView(member);
        }

        // GET: Member
        //public ActionResult Index()
        //{
        //    return View();
        //}

        #region support
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        #endregion
    }
}