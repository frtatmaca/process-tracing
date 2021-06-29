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
using Action = SakaryaBel.Web.Models.Action;

namespace SakaryaBel.Web.Controllers
{
    [Authorize]
    public class ActionController : BaseController
    {
        private UserManager<ApplicationUser> userManager;
        private RoleManager<ApplicationRole> roleManager;

        readonly BlogContext db = new BlogContext();
        public ActionController(IUnitOfWork uow)
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

        public ActionResult New()
        {

            return View();
        }

        public ActionResult Edit(int id)
        {
            Action act = db.Activity.Where(m => m.ActivityId == id).Select(m => new Action { ActionId = m.ActivityId, Name = m.Name, Description = m.Description }).FirstOrDefault();
            return View(act);
        }

        [HttpPost]
        public ActionResult Edit(Action model)
        {
            if (ModelState.IsValid)
            {
                Activity act = db.Activity.Where(m => m.ActivityId == model.ActionId).FirstOrDefault();
                act.Name = model.Name;
                act.Description = model.Description;
                act.LastUpdatedDate = DateTime.Now;
                string userId = User.Identity.GetUserId();
                act.LastUpdatedByUser = db.Users.FirstOrDefault(x => x.Id == userId);

                db.Activity.Attach(act);
                db.Entry(act).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

            }


            return View(model);
        }

        [HttpPost]
        public ActionResult New(Action model)
        {
            if (ModelState.IsValid)
            {
                Activity activity = new Activity();
                activity.Name = model.Name;
                activity.Description = model.Description;
                activity.ActivityType = ActivityType.Action;
                activity.IsCommon = false;
                activity.ActiveStatus = ActiveStatus.Active;
                activity.ActivityStatus = ActivityStatus.New;
                activity.CreatedDate = DateTime.Now;
                string userId = User.Identity.GetUserId();
                activity.CreatedByUser = db.Users.FirstOrDefault(x => x.Id == userId);

                db.Activity.Add(activity);
                db.SaveChanges();

                return RedirectToAction("Index", "Action");
            }

            return View(model);
        }


        public JsonResult GetActionList(string actionName, string userList, string startDate, string endDate, int skip, int top)
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
            if (!string.IsNullOrEmpty(userList))
            {
                foreach (var item in userList.Split(','))
                    cheifUserList.Add(item);
            }
            else
            {
                string identityUserId = User.Identity.GetUserId();
                cheifUserList.Add(identityUserId);

                if (User.IsInRole("SuperCheif"))
                    hasPermission = true;
                else if (User.IsInRole("Cheif"))
                {
                    string refUserId = User.Identity.GetUserId();
                    cheifUserList = userManager.Users.Where(m => m.Cheif.Id == refUserId).Select(m => m.Id).ToList();
                }
            }

            List<ActionListModel> activityList = new List<ActionListModel>();
            int totalCount = 0;
            activityList = db.Activity
                .Where(m =>
                    (hasPermission || cheifUserList.Contains(m.CreatedByUser.Id)) && m.ActivityType == ActivityType.Action && m.ActiveStatus == ActiveStatus.Active &&
                    (string.IsNullOrEmpty(actionName) || m.Name.Contains(actionName))
                    && (string.IsNullOrEmpty(startDate) || m.CreatedDate >= refStartDate)
                    && (string.IsNullOrEmpty(endDate) || m.CreatedDate <= refEndDate)
                    )
                .Select(m => new ActionListModel
                {
                    ActionId = m.ActivityId,
                    Name = m.Name,
                    Description = m.Description,
                    SubActionCount = db.Activity.Where(l => l.ActionId == m.ActivityId && l.ActiveStatus == ActiveStatus.Active).Count()

                }).OrderBy(m => m.Name).Skip(skip).Take(top).ToList();


            totalCount = db.Activity
                .Where(m =>
                    (hasPermission || cheifUserList.Contains(m.CreatedByUser.Id)) && m.ActivityType == ActivityType.Action
                    && (string.IsNullOrEmpty(actionName) || m.Name.Contains(actionName))
                    && (string.IsNullOrEmpty(startDate) || m.CreatedDate >= refStartDate)
                    && (string.IsNullOrEmpty(endDate) || m.CreatedDate <= refEndDate)
                    ).Count();
            var model = activityList;
            int pageInCount = activityList.Count;

            Object[] result = { model, totalCount, pageInCount };
            return Json(result, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

    }
}
