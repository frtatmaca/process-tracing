using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SakaryaBel.Data.UnitOfWork;
using SakaryaBel.Web.Identity;
using System.Web.Mvc;

namespace SakaryaBel.Web.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private UserManager<ApplicationUser> userManager;
        private RoleManager<ApplicationRole> roleManager;

        public HomeController(IUnitOfWork uow)
            : base(uow)
        {
            BlogContext db = new BlogContext();

            UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(db);
            userManager = new UserManager<ApplicationUser>(userStore);

            RoleStore<ApplicationRole> roleStore = new RoleStore<ApplicationRole>(db);
            roleManager = new RoleManager<ApplicationRole>(roleStore);
        }



        public ActionResult Index()
        {

            return View();
        }

    }
}
