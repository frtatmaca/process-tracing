using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using SakaryaBel.Web.Enums;
using SakaryaBel.Web.Identity;
using SakaryaBel.Web.Models;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace SakaryaBel.Web.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        private UserManager<ApplicationUser> userManager;
        private RoleManager<ApplicationRole> roleManager;
        readonly BlogContext db = new BlogContext();
        public AccountController()
        {
            UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(db);
            userManager = new UserManager<ApplicationUser>(userStore);

            RoleStore<ApplicationRole> roleStore = new RoleStore<ApplicationRole>(db);
            roleManager = new RoleManager<ApplicationRole>(roleStore);
        }

        public ActionResult Login()
        {
            return View();
        }

        [Authorize]
        public ActionResult Logout()
        {
            IAuthenticationManager authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = userManager.Find(model.Username, model.Password);

                if (user != null)
                {
                    //if (user.UserType != UserType.User)
                    //{
                        IAuthenticationManager authManager = HttpContext.GetOwinContext().Authentication;


                        ClaimsIdentity identity = userManager.CreateIdentity(user, "ApplicationCookie");
                        AuthenticationProperties authProps = new AuthenticationProperties();
                        authProps.IsPersistent = model.RememberMe;
                        authManager.SignIn(authProps, identity);

                        return RedirectToAction("Index", "Home");
                    //}

                    ModelState.AddModelError("LoginUser", "Yetkisiz giriş");

                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("LoginUser", "Böyle bir kullanıcı bulunamadı");
                }
            }
            return View(model);
        }

    }
}
