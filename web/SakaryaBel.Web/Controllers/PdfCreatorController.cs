using Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SakaryaBel.Data.UnitOfWork;
using SakaryaBel.Web.Enums;
using SakaryaBel.Web.Helpers;
using SakaryaBel.Web.Identity;
using SakaryaBel.Web.Models;
using SakaryaBel.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SakaryaBel.Web.Controllers
{
    public class PdfCreatorController : BaseController
    {
        private UserManager<ApplicationUser> userManager;
        private RoleManager<ApplicationRole> roleManager;

        readonly BlogContext db = new BlogContext();

        public PdfCreatorController(IUnitOfWork uow)
            : base(uow)
        {
            UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(db);
            userManager = new UserManager<ApplicationUser>(userStore);

            RoleStore<ApplicationRole> roleStore = new RoleStore<ApplicationRole>(db);
            roleManager = new RoleManager<ApplicationRole>(roleStore);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult testPdf(string id)
        {
            PdfModel user = new PdfModel();

            user = db.Users.Where(m => m.Id == id).Select(m => new PdfModel
            {
                UserName = m.UserName,
                Name = m.Name,
                SurName = m.Surname,
                CheifInfo = m.Cheif.UserName + " - " + m.Cheif.Name + " " + m.Cheif.Surname,
                UserType = m.UserType,
                ProfilImageUrl = m.FileId.HasValue ? "/Files/" + m.File.FileName : "/Files/user_unknown.png"
            }).FirstOrDefault();

            user.actList = db.Activity.Where(m => m.CreatedByUser.Id == id).Select(m => new ActivityListModel
            {
                ActivityId = m.ActivityId,
                Name = m.Name,
                Description = m.Description,
                ActionId = m.ActionId,
                ActivityStatus = m.ActivityStatus,
                ActivityType = m.ActivityType,
                CreatedDate = m.CreatedDate,
                CompletedDate = m.CompletedDate,
            }).ToList();

            foreach (var item in user.actList)
            {
                item.StringCreatedDate = item.CreatedDate.HasValue ? item.CreatedDate.ToFDate(true) : "";
                item.StringCompletedDate = item.CompletedDate.HasValue ? item.CompletedDate.ToFDate(true) : "";
            }

            List<PdfModel> model = new List<PdfModel>();
            model.Add(user);

            return View(model);
        }

        [Authorize]
        public ActionResult DownloadActionAsPDF(string id)
        {            
           //Code to get content
            return new Rotativa.ActionAsPdf("testPdf", new { id = id }) { FileName = "KullaniciRaporu.pdf" };
        }
    }
}
