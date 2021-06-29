using Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SakaryaBel.Data.UnitOfWork;
using SakaryaBel.Web.DomainModel.Entities;
using SakaryaBel.Web.Enums;
using SakaryaBel.Web.Helpers;
using SakaryaBel.Web.Identity;
using SakaryaBel.Web.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SakaryaBel.Web.Controllers
{
    [Authorize]
    public class ProblemController : BaseController
    {
        private UserManager<ApplicationUser> userManager;
        private RoleManager<ApplicationRole> roleManager;

        readonly BlogContext db = new BlogContext();
        public ProblemController(IUnitOfWork uow)
            : base(uow)
        {
            UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(db);
            userManager = new UserManager<ApplicationUser>(userStore);

            RoleStore<ApplicationRole> roleStore = new RoleStore<ApplicationRole>(db);
            roleManager = new RoleManager<ApplicationRole>(roleStore);
        }

        public ActionResult Index(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                List<selectList> roleList = db.Users.Where(m => m.Id == id)
                    .Select(m => new selectList
                    {
                        Id = m.Id,
                        Text = m.Name
                    })
                    .ToList();
                int rolesCount = 1;
                Select2PagedResult pagedRoles = Common.ConvertToSelect2Format(roleList, rolesCount);
                ViewBag.UserList = pagedRoles.Results;
            }
            return View();
        }

        public ActionResult ProblemDetail(int id)
        {
            ProblemDetailListModel act = db.Activity.Where(m => m.ActivityId == id).Select(m => new ProblemDetailListModel
            {
                ActionId = m.ActivityId,
                Name = m.Name,
                Description = m.Description,
                BackGroundColor = m.ActivityStatus == ActivityStatus.New ? "btn-danger" : m.ActivityStatus == ActivityStatus.Complete ? "btn-success" : m.ActivityStatus == ActivityStatus.Inspected ? "btn-warning" : m.ActivityStatus == ActivityStatus.CloseManager ? "#A523A7 !important" : "btn-info",
                CreatedDate = m.CreatedDate,
                CompletedDate = m.CompletedDate,
                FileType = m.Files.Where(l => l.ActiveStatus == ActiveStatus.Active).FirstOrDefault() != null ? m.Files.Where(l => l.ActiveStatus == ActiveStatus.Active).FirstOrDefault().FileType : FileType.Image,
                FileName = m.Files.Where(l => l.ActiveStatus == ActiveStatus.Active).FirstOrDefault() != null ? "/Files/" + m.Files.Where(l => l.ActiveStatus == ActiveStatus.Active).FirstOrDefault().FileName : "/Files/user_unknown.png",
                ProblemOfActId = m.ActionId,
                CreatedUserId = m.CreatedByUser.Id,
                messages = m.Messages.Select(l => new MessageDetailListModel
                {
                    MessageId = l.MessageId,
                    Body = l.Body,
                    SenderUserId = l.CreatedByUser.Id,
                    SenderUserName = l.CreatedByUser.UserName + " - " + l.CreatedByUser.Name + " " + l.CreatedByUser.Surname,
                    SenderProfilImage = l.CreatedByUser.File != null ? "/Files/" + l.CreatedByUser.File.FileName : "/Files/user_unknown.png",
                    CreatedDate = l.CreatedDate
                }).ToList()
            }).FirstOrDefault();


            if (act.BackGroundColor == "btn-danger" && (User.IsInRole("SuperCheif") || User.IsInRole("Cheif")))
            {
                Activity updateAct = db.Activity.Find(id);
                updateAct.ActivityStatus = ActivityStatus.Inspected;
                updateAct.LastUpdatedDate = DateTime.Now;

                db.Activity.Attach(updateAct);
                db.Entry(updateAct).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                act.BackGroundColor = "btn-warning";
            }

            if (act.ProblemOfActId.HasValue)
            {
                act.ActId = act.ProblemOfActId.ToString();
                act.ActionName = db.Activity.Find(act.ProblemOfActId.Value).Name;
            }

            return View(act);
        }

        [HttpPost]
        public JsonResult NewProblemMesaj(int problemId, string mesaj)
        {
            bool hasPermission = true;

            if (hasPermission == false)
                return Json("error", "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);

            string decodeMesaj = HttpUtility.UrlDecode(mesaj);

            Message answer = new Message();
            answer.ActivityId = problemId;
            answer.Body = decodeMesaj;
            answer.ActiveStatus = ActiveStatus.Active;
            answer.MessajeInfo = MessageProcessInfo.beAnswer;
            answer.SenderUserName = User.Identity.GetUserName();
            answer.SenderEmail = User.Identity.GetUserName();
            answer.TrackingGuid = Guid.NewGuid();
            string userId = User.Identity.GetUserId();
            answer.CreatedByUser = db.Users.FirstOrDefault(x => x.Id == userId);
            answer.CreatedDate = DateTime.Now;

            db.Message.Add(answer);
            db.SaveChanges();

            string createdDate = DateTime.Now.ToFDate(true);
            return Json(createdDate, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        public ActionResult New()
        {

            return View();
        }

        public ActionResult FilePreview(int id)
        {
            var file = db.File.Where(m => m.FileId == id).Select(m => new { FileType = m.FileType, Name = m.FileName }).FirstOrDefault();
            ViewBag.FileType = (int)file.FileType;
            ViewBag.FileName = file.Name;

            return View();
        }

        public ActionResult Edit(int id)
        {
            Problem model = db.Activity.Where(m => m.ActivityId == id).Select(m => new Problem
            {
                ProblemId = m.ActivityId,
                Name = m.Name,
                Description = m.Description,
                FileUrl = m.Files.Where(l => l.ActiveStatus == ActiveStatus.Active).FirstOrDefault() != null ? m.Files.Where(l => l.ActiveStatus == ActiveStatus.Active).FirstOrDefault().FileName : "",
                FileId = m.Files.Where(l => l.ActiveStatus == ActiveStatus.Active).FirstOrDefault() != null ? m.Files.Where(l => l.ActiveStatus == ActiveStatus.Active).FirstOrDefault().FileId : 0,
                UserId = m.CreatedByUser.Id,
                UserName = m.CreatedByUser.UserName + " - " + m.CreatedByUser.Name + " " + m.CreatedByUser.Surname,
            }).FirstOrDefault();

            return View(model);
        }

        private static readonly string _filePathFormat = "~/Files/";

        [HttpPost]
        public ActionResult Edit(Problem model)
        {
            if (ModelState.IsValid)
            {
                var directory = Server.MapPath(_filePathFormat);
                SakaryaBel.Web.DomainModel.Entities.File fileModel = null;
                try
                {
                    if (Request.Files[0].ContentLength > 0)
                    {
                        fileModel = new SakaryaBel.Web.DomainModel.Entities.File();

                        HttpPostedFileBase postedFile = Request.Files[0] as HttpPostedFileBase;
                        var postedFileType = ContentHelper.ControlFileType(postedFile.ContentType);
                        var diffentType = Path.GetExtension(postedFile.FileName);
                        if (diffentType == ".exe" || diffentType == ".asp" || diffentType == ".sql")
                        {
                            postedFileType = 0;
                        }
                        if (postedFileType == 0) // Tip izin verilen tiplerden herhangi biri değilse
                        {
                            return Json(new { ftype = 0 }, JsonRequestBehavior.AllowGet);
                        }
                        else if (postedFile.ContentLength > 0 && !string.IsNullOrEmpty(postedFile.FileName))
                        {
                            string newfilename = Path.GetFileName(postedFile.FileName).ToLower().Replace(' ', '_');

                            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(postedFile.FileName).Replace(' ', '_');

                            DirectoryInfo ftpFolder = new System.IO.DirectoryInfo(directory);

                            FileInfo[] fileInfos = ftpFolder.GetFiles();
                            int files = fileInfos.OrderBy(f => f.Name).Count();

                            if (System.IO.File.Exists(Path.Combine(directory, newfilename)))
                            {
                                for (int i = 0; i < files; i++)
                                {
                                    string str = Path.Combine(directory, fileNameWithoutExtension + "_" + (i + 1) + Path.GetExtension(newfilename));

                                    if (!System.IO.File.Exists(Path.Combine(directory, fileNameWithoutExtension + "_" + (i + 1) + Path.GetExtension(newfilename))))
                                    {
                                        newfilename = fileNameWithoutExtension + "_" + (i + 1) + Path.GetExtension(newfilename);

                                        break;
                                    }
                                }
                            }

                            fileModel.FileName = newfilename;
                            fileModel.Description = postedFile.FileName;
                            fileModel.Size = postedFile.ContentLength;
                            fileModel.ContentType = postedFile.ContentType;
                            fileModel.TrackingGuid = Guid.NewGuid();
                            fileModel.CreatedDate = DateTime.Now;
                            //fileModel.FileType = (int)ContentFileType.UserTicket;
                            fileModel.CreatedByUserId = User.Identity.GetUserId(); //LMSData.User.UserId.Value;                    
                            fileModel.LastUpdatedDate = DateTime.Now;
                            fileModel.LastUpdatedByUserId = User.Identity.GetUserId();
                            fileModel.ActiveStatus = ActiveStatus.Active;
                            fileModel.Size = postedFile.ContentLength / 1024;
                            fileModel.FileType = (FileType)postedFileType;
                            fileModel.OwnerType = FileOwnerType.Activity;


                            db.File.Add(fileModel);
                            db.SaveChanges();

                            #region FileUpload

                            if (!Directory.Exists(directory))
                                Directory.CreateDirectory(directory);

                            var filePath = directory; //Path.Combine(directory, Convert.ToString(model.TrackingGuid));                    

                            filePath = Path.Combine(filePath, Path.GetFileName(newfilename));
                            var fi = new FileInfo(filePath);
                            if (fi.Exists)
                            {
                                return Json(new { status = "fexist" }, JsonRequestBehavior.AllowGet);
                            }

                            postedFile.SaveAs(filePath);
                            #endregion
                        }

                    }
                    //return Json(new { ftype = filem.FileType, fid = model.FileId, savedresult = true, status = returnValue }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {

                }


                var activity = (from s in db.Activity.Include("Files")
                                where s.ActivityId == model.ProblemId.Value
                                select s).FirstOrDefault<Activity>();

                string userId = "";
                if (!string.IsNullOrEmpty(Request.Form["ddlUserList"]))
                {
                    userId = Convert.ToString(Request.Form["ddlUserList"]);
                    ApplicationUser user = db.Users.FirstOrDefault(x => x.Id == userId);
                    activity.CreatedByUser = user;
                    activity.LastUpdatedByUser = user;
                    activity.LastUpdatedDate = DateTime.Now;
                }
                else
                {
                    //userId = User.Identity.GetUserId();
                    //activity.CreatedByUser = db.Users.FirstOrDefault(x => x.Id == userId);
                }


                if (fileModel != null)
                {
                    if (activity.Files.Count > 0)
                    {
                        foreach (var item in activity.Files)
                            item.ActiveStatus = ActiveStatus.Deleted;
                    }

                    //activity.Files.Add(fileModel);
                }

                activity.Name = model.Name;
                activity.Description = model.Description;
                activity.ActivityType = ActivityType.Problem;
                activity.IsCommon = false;
                activity.ActiveStatus = ActiveStatus.Active;
                activity.ActivityStatus = ActivityStatus.New;
                activity.CreatedDate = DateTime.Now;

                db.Activity.Attach(activity);
                db.Entry(activity).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                if (fileModel != null)
                {
                    activity.Files.Add(fileModel);
                    db.SaveChanges();
                }

                return RedirectToAction("Index", "Problem");
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult New(Problem model)
        {
            if (ModelState.IsValid)
            {
                var directory = Server.MapPath(_filePathFormat);
                SakaryaBel.Web.DomainModel.Entities.File fileModel = null;
                try
                {
                    if (Request.Files[0].ContentLength > 0)
                    {
                        fileModel = new SakaryaBel.Web.DomainModel.Entities.File();

                        HttpPostedFileBase postedFile = Request.Files[0] as HttpPostedFileBase;
                        var postedFileType = ContentHelper.ControlFileType(postedFile.ContentType);
                        var diffentType = Path.GetExtension(postedFile.FileName);
                        if (diffentType == ".exe" || diffentType == ".asp" || diffentType == ".sql")
                        {
                            postedFileType = 0;
                        }
                        if (postedFileType == 0) // Tip izin verilen tiplerden herhangi biri değilse
                        {
                            return Json(new { ftype = 0 }, JsonRequestBehavior.AllowGet);
                        }
                        else if (postedFile.ContentLength > 0 && !string.IsNullOrEmpty(postedFile.FileName))
                        {
                            string newfilename = Path.GetFileName(postedFile.FileName).ToLower().Replace(' ', '_');

                            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(postedFile.FileName).Replace(' ', '_');

                            DirectoryInfo ftpFolder = new System.IO.DirectoryInfo(directory);

                            FileInfo[] fileInfos = ftpFolder.GetFiles();
                            int files = fileInfos.OrderBy(f => f.Name).Count();

                            if (System.IO.File.Exists(Path.Combine(directory, newfilename)))
                            {
                                for (int i = 0; i < files; i++)
                                {
                                    string str = Path.Combine(directory, fileNameWithoutExtension + "_" + (i + 1) + Path.GetExtension(newfilename));

                                    if (!System.IO.File.Exists(Path.Combine(directory, fileNameWithoutExtension + "_" + (i + 1) + Path.GetExtension(newfilename))))
                                    {
                                        newfilename = fileNameWithoutExtension + "_" + (i + 1) + Path.GetExtension(newfilename);

                                        break;
                                    }
                                }
                            }

                            fileModel.FileName = newfilename;
                            fileModel.Description = postedFile.FileName;
                            fileModel.Size = postedFile.ContentLength;
                            fileModel.ContentType = postedFile.ContentType;
                            fileModel.TrackingGuid = Guid.NewGuid();
                            fileModel.CreatedDate = DateTime.Now;
                            //fileModel.FileType = (int)ContentFileType.UserTicket;
                            fileModel.CreatedByUserId = User.Identity.GetUserId(); //LMSData.User.UserId.Value;                    
                            fileModel.LastUpdatedDate = DateTime.Now;
                            fileModel.LastUpdatedByUserId = User.Identity.GetUserId();
                            fileModel.ActiveStatus = ActiveStatus.Active;
                            fileModel.Size = postedFile.ContentLength / 1024;
                            fileModel.FileType = (FileType)postedFileType;
                            fileModel.OwnerType = FileOwnerType.Activity;
                            db.File.Add(fileModel);
                            db.SaveChanges();

                            #region FileUpload

                            if (!Directory.Exists(directory))
                                Directory.CreateDirectory(directory);

                            var filePath = directory; //Path.Combine(directory, Convert.ToString(model.TrackingGuid));                    

                            filePath = Path.Combine(filePath, Path.GetFileName(newfilename));
                            var fi = new FileInfo(filePath);
                            if (fi.Exists)
                            {
                                return Json(new { status = "fexist" }, JsonRequestBehavior.AllowGet);
                            }

                            postedFile.SaveAs(filePath);
                            #endregion
                        }

                    }
                    //return Json(new { ftype = filem.FileType, fid = model.FileId, savedresult = true, status = returnValue }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                }

                Activity activity = new Activity();
                string userId = "";
                if (!string.IsNullOrEmpty(Request.Form["ddlUserList"]))
                {
                    userId = Convert.ToString(Request.Form["ddlUserList"]);
                    ApplicationUser user = db.Users.FirstOrDefault(x => x.Id == userId);
                    activity.CreatedByUser = user;
                    activity.LastUpdatedByUser = user;
                    activity.LastUpdatedDate = DateTime.Now;
                }
                else
                {
                    userId = User.Identity.GetUserId();
                    activity.CreatedByUser = db.Users.FirstOrDefault(x => x.Id == userId);
                }


                if (fileModel != null)
                    activity.Files.Add(fileModel);

                activity.Name = model.Name;
                activity.Description = model.Description;
                activity.ActivityType = ActivityType.Problem;
                activity.IsCommon = false;
                activity.ActiveStatus = ActiveStatus.Active;
                activity.ActivityStatus = ActivityStatus.New;
                activity.CreatedDate = DateTime.Now;
                db.Activity.Add(activity);
                db.SaveChanges();

                return RedirectToAction("Index", "Problem");
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult ChangeProblemStatus(int problemId, int status)
        {
            var p = db.Activity.Find(problemId);
            p.ActivityStatus = (ActivityStatus)status;
            if ((ActivityStatus)status == ActivityStatus.Complete)
                p.CompletedDate = DateTime.Now;

            string userId = User.Identity.GetUserId();
            p.LastUpdatedByUser = db.Users.FirstOrDefault(x => x.Id == userId);
            p.LastUpdatedDate = DateTime.Now;
            db.Activity.Attach(p);
            db.Entry(p).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return Json("success", "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ChangeProblemOfAction(int problemId, string actionId)
        {
            if (!string.IsNullOrEmpty(actionId))
            {
                var p = db.Activity.Find(problemId);
                p.ActionId = Convert.ToInt32(actionId);
                string userId = User.Identity.GetUserId();
                p.LastUpdatedByUser = db.Users.FirstOrDefault(x => x.Id == userId);
                p.LastUpdatedDate = DateTime.Now;
                db.Activity.Attach(p);
                db.Entry(p).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return Json("success", "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetUserList(string searchTerm, int pageSize, int pageNum)
        {
            bool hasPermission = false;
            List<string> cheifUserList = new List<string>();
            if (User.IsInRole("SuperCheif"))
                hasPermission = true;
            else if (User.IsInRole("Cheif"))
            {
                string refUserId = User.Identity.GetUserId();
                cheifUserList = userManager.Users.Where(m => m.Cheif.Id == refUserId).Select(m => m.Id).ToList();
            }

            List<selectList> actionList = db.Users.Where(m => (hasPermission || cheifUserList.Contains(m.Cheif.Id))
                                                    && m.ActiveStatus == ActiveStatus.Active)
                .Select(m => new
                {
                    Id = m.Id,
                    Text = m.Name
                }).ToList().Select(m => new selectList
                {
                    Id = m.Id,
                    Text = m.Text
                }).ToList();

            int actionCount = db.Users.Where(m => (hasPermission || cheifUserList.Contains(m.Cheif.Id))
                                                    && m.ActiveStatus == ActiveStatus.Active).Count();

            Select2PagedResult pagedAttendees = Common.ConvertToSelect2Format(actionList, actionCount);
            return new JsonpResult
            {
                Data = pagedAttendees,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetProblemList(string problemName, int problemStatus, string userList, string startDate, string endDate, int skip, int top)
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
            List<ProblemListModel> problemList = new List<ProblemListModel>();
            int totalCount = 0;
            problemList = db.Activity
                .Where(m =>
                    (hasPermission || cheifUserList.Contains(m.CreatedByUser.Id)) && m.ActivityType == ActivityType.Problem && m.ActiveStatus == ActiveStatus.Active &&
                    (problemStatus == -1 || m.ActivityStatus == (ActivityStatus)problemStatus) &&
                    (string.IsNullOrEmpty(problemName) || m.Name.Contains(problemName))
                    && (string.IsNullOrEmpty(startDate) || m.CreatedDate >= refStartDate)
                    && (string.IsNullOrEmpty(endDate) || m.CreatedDate <= refEndDate)
                    )
                .Select(m => new ProblemListModel
                {
                    ActionId = m.ActivityId,
                    Name = m.Name,
                    Description = m.Description,
                    //SubActionCount = db.Activity.Where(l => l.ActionId == m.ActivityId).Count()
                    //BackGroundColor = m.ActivityStatus == ActivityStatus.New ? "btn-danger" : m.ActivityStatus == ActivityStatus.Complete ? "btn-success" : m.ActivityStatus == ActivityStatus.Inspected ? "btn-warning" : "btn-info",
                    BackGroundColor = m.ActivityStatus == ActivityStatus.New ? "#d64d49" : m.ActivityStatus == ActivityStatus.Complete
                                        ? "#5ab65a" : m.ActivityStatus == ActivityStatus.Inspected ? "#efaa46" : m.ActivityStatus == ActivityStatus.CloseManager ? "#A523A7" : "#3f86c4",
                    CreatedDate = m.CreatedDate,
                    CompletedDate = m.CompletedDate,
                    CreatedUserName = m.CreatedByUser.UserName
                }).OrderBy(m => m.Name).Skip(skip).Take(top).ToList();

            foreach (var item in problemList)
            {
                item.StringCompletedDate = item.CompletedDate != null ? item.CompletedDate.ToFDateTime(true) : "";
                item.StringCreatedDate = item.CreatedDate != null ? item.CreatedDate.ToFDateTime(true) : "";
            }
            totalCount = db.Activity
                .Where(m =>
                    (hasPermission || cheifUserList.Contains(m.CreatedByUser.Id)) && m.ActivityType == ActivityType.Problem && m.ActiveStatus == ActiveStatus.Active &&
                    (problemStatus == -1 || m.ActivityStatus == (ActivityStatus)problemStatus) &&
                    (string.IsNullOrEmpty(problemName) || m.Name.Contains(problemName))
                    && (string.IsNullOrEmpty(startDate) || m.CreatedDate >= refStartDate)
                    && (string.IsNullOrEmpty(endDate) || m.CreatedDate <= refEndDate)
                    ).Count();
            var model = problemList;
            int pageInCount = problemList.Count;

            Object[] result = { model, totalCount, pageInCount };
            return Json(result, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetActionList(string searchTerm, int pageSize, int pageNum, string CreatedUserId)
        {
            bool hasPermission = false;
            string identityUserId = User.Identity.GetUserId();
            List<string> cheifUserList = new List<string>();
            cheifUserList.Add(identityUserId);
            cheifUserList.Add(CreatedUserId);

            if (User.IsInRole("SuperCheif"))
                hasPermission = true;
            //else if (User.IsInRole("Cheif"))
            //{
            //    string refUserId = User.Identity.GetUserId();
            //    cheifUserList = userManager.Users.Where(m => m.Cheif.Id == refUserId).Select(m => m.Id).ToList();
            //}

            List<selectList> actionList = db.Activity.Where(m => (hasPermission || cheifUserList.Contains(m.CreatedByUser.Id)) && m.ActiveStatus == ActiveStatus.Active && m.ActivityType == ActivityType.Action).Select(m => new
            {
                Id = m.ActivityId,
                Text = m.Name
            }).ToList().Select(m => new selectList
            {
                Id = m.Id.ToString(),
                Text = m.Text
            }).ToList();

            int actionCount = db.Activity.Where(m => (hasPermission || cheifUserList.Contains(m.CreatedByUser.Id)) && m.ActiveStatus == ActiveStatus.Active && m.ActivityType == ActivityType.Action).Select(m => new
            {
                Id = m.ActivityId,
                Text = m.Name
            }).Count();

            Select2PagedResult pagedAttendees = Common.ConvertToSelect2Format(actionList, actionCount);
            return new JsonpResult
            {
                Data = pagedAttendees,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult ProblemStatusReplace(int problemId)
        {
            Activity act = db.Activity.Find(problemId);
            if (act != null)
            {
                act.ActiveStatus = ActiveStatus.Deleted;

                db.Activity.Attach(act);
                db.Entry(act).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return Json("success", "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
            }
            else
                return Json("failed", "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);

        }

        public JsonResult ActionStatusReplace(int actId, string status)
        {
            Activity act = db.Activity.Find(actId);

            if (act != null)
            {
                if (status == "aktif")
                    act.ActiveStatus = ActiveStatus.Active;
                else if (status == "pasif")
                    act.ActiveStatus = ActiveStatus.Passive;
                else
                    act.ActiveStatus = ActiveStatus.Deleted;

                db.Activity.Attach(act);
                db.Entry(act).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return Json("success", "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
            }
            else
                return Json("failed", "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);

        }
    }
}
