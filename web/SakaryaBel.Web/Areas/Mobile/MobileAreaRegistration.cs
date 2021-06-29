using System.Web.Mvc;

namespace SakaryaBel.Web.Areas.Mobile
{
    public class MobileAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Mobile";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            //context.MapRoute(
            //    "Mobile_default",
            //    "mobile/{controller}/{action}/{id}/{fields}",
            //    new { id = UrlParameter.Optional, fields = UrlParameter.Optional },
            //    namespaces: new[] { "Blms.Web.Areas.Mobile.Controllers" }
            //);
        }
    }
}
