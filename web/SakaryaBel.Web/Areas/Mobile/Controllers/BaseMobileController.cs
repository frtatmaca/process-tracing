using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using SakaryaBel.Web.Identity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace SakaryaBel.Web.Areas.Mobile.Controllers
{
    public class BaseMobileController : ApiController
    {
        public readonly BlogContext db = new BlogContext();

        private UserManager<ApplicationUser> userManager;
        private RoleManager<ApplicationRole> roleManager;

        public BaseMobileController()
        {
            UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(db);
            userManager = new UserManager<ApplicationUser>(userStore);

            RoleStore<ApplicationRole> roleStore = new RoleStore<ApplicationRole>(db);
            roleManager = new RoleManager<ApplicationRole>(roleStore);
        }

        private string ThreadName { get { return (System.Threading.Thread.CurrentPrincipal.Identity).Name; } }

        public string GenerateToken(string userName)
        {
            CultureInfo ci = new CultureInfo("en-US");
            string tokenKey = System.Configuration.ConfigurationManager.AppSettings["MobileAuthTokenKey"];
            byte[] keyByte = System.Text.Encoding.UTF8.GetBytes((tokenKey ?? "MobileAuthTokenKey:"));
            var hmacsha256 = new System.Security.Cryptography.HMACSHA256(keyByte);

            byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes(userName);
            byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);

            string sbinary = "";
            for (int i = 0; i < hashmessage.Length; i++) sbinary += hashmessage[i].ToString("X2");
            return userName + "-" + sbinary;
        }

        public bool ValidateToken(string token, out string userName)
        {
            userName = "";
            if (string.IsNullOrEmpty(token) || !token.Contains("-")) return false;
            userName = token.Split('-')[0];

            bool isValid = GenerateToken(userName) == token;
            //if (isValid) // GetIndrectRef session fix
            //    HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(new System.Security.Principal.GenericIdentity(userName), new string[] { "MobileUser" });

            bool IsAuthenticated = User.Identity.IsAuthenticated;

            string userId = User.Identity.GetUserId();

            if (!IsAuthenticated && (User == null || User.Identity == null || string.IsNullOrEmpty(userId)))
            {
                string uName = userName;
                //string id = "5c10b827-e99e-4610-ae53-9755fa6874e9";
                //var user = userManager.FindById(id);

                var user = db.Users.Where(m => m.UserName == uName).FirstOrDefault(); //userManager.FindById(id);

                if (user != null)
                {
                    IAuthenticationManager authManager = HttpContext.Current.GetOwinContext().Authentication;


                    ClaimsIdentity identity = userManager.CreateIdentity(user, "ApplicationCookie");
                    AuthenticationProperties authProps = new AuthenticationProperties();
                    authProps.IsPersistent = false;
                    authManager.SignIn(authProps, identity);
                }
                else
                    return false;
            }

            return isValid;
        }
        public bool ValidateToken(string token, out string userName, out HttpResponseMessage response)
        {
            response = new HttpResponseMessage();
            bool isValid = ValidateToken(token, out userName);
            if (!isValid)
                response = Request.CreateResponse(HttpStatusCode.Unauthorized);

            return isValid;
        }
    }
}
