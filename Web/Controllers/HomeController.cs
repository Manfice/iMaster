using System.Web;
using System.Web.Mvc;
using Domen.Abstract;
using Microsoft.AspNet.Identity.Owin;

namespace Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ICustomer _customer;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public HomeController(ICustomer customer)
        {
            _customer = customer;
        }
        public ActionResult Index()
        {
            return View();
        }

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