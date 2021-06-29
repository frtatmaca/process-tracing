using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SakaryaBel.IOC;
using SakaryaBel.Web.Identity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SakaryaBel.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfig.CustomizeConfig(GlobalConfiguration.Configuration);

            Bootstrapper.Initialise();

            // Rol tanımlama adımları
            BlogContext db = new BlogContext();
            RoleStore<ApplicationRole> roleStore = new RoleStore<ApplicationRole>(db);
            RoleManager<ApplicationRole> roleManager = new RoleManager<ApplicationRole>(roleStore);

            if (!roleManager.RoleExists("SuperCheif"))
            {
                ApplicationRole adminRole = new ApplicationRole("SuperCheif", "Süper Şef");
                roleManager.Create(adminRole);
            }

            if (!roleManager.RoleExists("Cheif"))
            {
                ApplicationRole adminRole = new ApplicationRole("Cheif", "Şef");
                roleManager.Create(adminRole);
            }

            if (!roleManager.RoleExists("User"))
            {
                ApplicationRole userRole = new ApplicationRole("User", "Kulllanıcı");
                roleManager.Create(userRole);
            }
            // Rol tanımlama adımları
        }
    }
}