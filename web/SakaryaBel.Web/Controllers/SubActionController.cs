using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SakaryaBel.Data.UnitOfWork;
using SakaryaBel.Web.DomainModel.Entities;
using SakaryaBel.Web.Enums;
using SakaryaBel.Web.Identity;
using SakaryaBel.Web.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace SakaryaBel.Web.Controllers
{
    [Authorize]
    public class SubActionController : BaseController
    {
        private UserManager<ApplicationUser> userManager;
        private RoleManager<ApplicationRole> roleManager;

        readonly BlogContext db = new BlogContext();
        public SubActionController(IUnitOfWork uow)
            : base(uow)
        {
            UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(db);
            userManager = new UserManager<ApplicationUser>(userStore);

            RoleStore<ApplicationRole> roleStore = new RoleStore<ApplicationRole>(db);
            roleManager = new RoleManager<ApplicationRole>(roleStore);
        }

        public ActionResult Index(int id)
        {
            ViewBag.ActionId = id;
            return View();
        }

        [HttpGet]
        public ActionResult New(int id)
        {
            ViewBag.ActionId = id;
            return View();
        }

        [HttpPost]
        public ActionResult New(SubAction model)
        {
            if (ModelState.IsValid)
            {
                Activity activity = new Activity();
                activity.Name = model.Name;
                activity.Description = model.Description;
                activity.ActionId = model.ActionId;
                activity.ActivityType = ActivityType.SubAction;
                activity.IsCommon = false;
                if (model.Number.HasValue)
                    activity.Number = model.Number.Value;
                else
                    activity.Number = 0;

                activity.ActiveStatus = ActiveStatus.Active;
                activity.ActivityStatus = ActivityStatus.New;
                activity.CreatedDate = DateTime.Now;
                string userId = User.Identity.GetUserId();
                activity.CreatedByUser = db.Users.FirstOrDefault(x => x.Id == userId);

                db.Activity.Add(activity);
                db.SaveChanges();

            }
            return View(model);
        }

        public JsonResult GetSubActionList(int actionId, string subActionName, string startDate, string endDate, int skip, int top)
        {
            DateTime refStartDate;
            DateTime refEndDate;

            string[] formats = { "dd/MM/yyyy", "dd/M/yyyy", "d/M/yyyy", "d/MM/yyyy",
                    "dd/MM/yy", "dd/M/yy", "d/M/yy", "d/MM/yy","MM/dd/yyyy"};

            if (!DateTime.TryParseExact(startDate, formats, System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out refStartDate))
            {
            }

            if (!DateTime.TryParseExact(endDate, formats, System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out refEndDate))
            {
            }

            bool hasPermission = false;
            List<string> cheifUserList = new List<string>();
            if (User.IsInRole("SuperCheif"))
                hasPermission = true;
            else if (User.IsInRole("Cheif"))
            {
                string refUserId = User.Identity.GetUserId();
                //cheifUserList = userManager.Users.Where(m => m.Cheif.Id == refUserId).Select(m => m.Id).ToList();
            }

            List<ActionListModel> activityList = new List<ActionListModel>();
            int totalCount = 0;
            activityList = db.Activity
                .Where(m =>
                    (hasPermission || cheifUserList.Contains(m.CreatedByUser.Id)) && m.ActivityType != ActivityType.Action && m.ActionId == actionId && m.ActiveStatus == ActiveStatus.Active &&
                    (string.IsNullOrEmpty(subActionName) || m.Name.Contains(subActionName))
                     && (string.IsNullOrEmpty(startDate) || m.CreatedDate >= refStartDate)
                    && (string.IsNullOrEmpty(endDate) || m.CreatedDate <= refEndDate)
                    )
                .Select(m => new ActionListModel
                {
                    ActionId = m.ActivityId,
                    Name = m.Name,
                    Description = m.Description,
                    SubActionCount = 0,
                    CreatedDate = m.CreatedDate,
                    SubActionType = (int)m.ActivityType,
                    Number = m.Number
                }).OrderBy(m => m.Name).Skip(skip).Take(top).ToList();


            foreach (var item in activityList)
                item.StringCreatedDate = item.CreatedDate.ToFDateTime(true);

            totalCount = db.Activity
                .Where(m =>
                    (hasPermission || cheifUserList.Contains(m.CreatedByUser.Id)) && m.ActivityType != ActivityType.Action && m.ActionId == actionId && m.ActiveStatus == ActiveStatus.Active &&
                    (string.IsNullOrEmpty(subActionName) || m.Name.Contains(subActionName))
                     && (string.IsNullOrEmpty(startDate) || m.CreatedDate >= refStartDate)
                    && (string.IsNullOrEmpty(endDate) || m.CreatedDate <= refEndDate)
                    ).Count();
            var model = activityList;
            int pageInCount = activityList.Count;

            int totalNumber = activityList.Sum(m => m.Number);
            Object[] result = { model, totalCount, pageInCount, totalNumber };
            return Json(result, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        public ActionResult test()
        {
            List<StudentInformation> help = new List<StudentInformation>();
            return View(help);
        }

        public ActionResult testCreate()
        {
            return View();
        }
    }
}
