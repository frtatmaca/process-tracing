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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SakaryaBel.Web.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        private UserManager<ApplicationUser> userManager;
        private RoleManager<ApplicationRole> roleManager;

        readonly BlogContext db = new BlogContext();

        public UserController(IUnitOfWork uow)
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

        public ActionResult ChangePassword(string id)
        {
            ViewBag.UserId = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await userManager.ChangePasswordAsync(model.Id, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {

                return View();
            }
            //AddErrors(result);
            return View(model);
        }


        public ActionResult Edit(string id)
        {
            var user = db.Users.Where(m => m.Id == id).Select(m => new UserEdit
            {
                Id = m.Id,
                Username = m.UserName,
                Name = m.Name,
                Surname = m.Surname,
                Email = m.Email,
                ProfileImage = m.FileId.HasValue ? "/Files/" + m.File.FileName : "/Files/user_unknown.png",
                UserType = m.UserType,
                CheifId = m.Cheif.Id,
                CheifName = m.Cheif.UserName + " - " + m.Cheif.Name + " " + m.Cheif.Surname,
                ActiveStatus = m.ActiveStatus
            }).FirstOrDefault();

            return View(user);
        }

        [HttpPost]
        public ActionResult GetCheif(string searchTerm, int pageSize, int pageNum)
        {
            List<selectList> userList = userManager.Users.Where(m => m.ActiveStatus == ActiveStatus.Active && (m.UserType == UserType.Cheif || m.UserType == UserType.SuperCheif)).Select(m => new selectList
                {
                    Id = m.Id,
                    Text = m.UserName + " - " + m.Name + " " + m.Surname
                })
                .ToList();

            int rolesCount = userManager.Users.Where(m => m.ActiveStatus == ActiveStatus.Active && (m.UserType == UserType.Cheif || m.UserType == UserType.SuperCheif)).Count();

            Select2PagedResult pagedAttendees = Common.ConvertToSelect2Format(userList, rolesCount);
            return new JsonpResult
            {
                Data = pagedAttendees,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [HttpPost]
        public JsonResult ChangeUserStatus(string userId, int status)
        {
            var p = db.Users.Find(userId);
            p.ActiveStatus = (ActiveStatus)status;
            string updaterUserId = User.Identity.GetUserId();
            p.LastUpdatedByUser = db.Users.FirstOrDefault(x => x.Id == updaterUserId);
            p.LastUpdatedDate = DateTime.Now;
            db.Users.Attach(p);
            db.Entry(p).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return Json(p.ActiveStatus.ToString(), "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUserList(string userName, string startDate, string endDate, int skip, int top)
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
                cheifUserList = userManager.Users.Where(m => m.Cheif.Id == refUserId).Select(m => m.Id).ToList();
            }

            List<UserModel> users = new List<UserModel>();
            int totalCount = 0;
            users = userManager.Users
                .Where(m =>
                    (hasPermission || cheifUserList.Contains(m.Id)) && m.UserType != UserType.SuperCheif &&
                    (string.IsNullOrEmpty(userName) || m.UserName.Contains(userName) || m.Name.Contains(userName))
                    )
                .Select(m => new
                {
                    UserId = m.Id,
                    Name = m.UserName,
                    Departman = "Departman",
                    ImageUrl = m.File != null ? "/Files/" + m.File.FileName : "/Files/user_unknown.png",
                    Cheif = m.Cheif.UserName + " - " + m.Cheif.Name + " " + m.Cheif.Surname,
                    Status = m.ActiveStatus
                }).OrderBy(m => m.Name).Skip(skip).Take(top).ToList().Select(m => new UserModel
                {
                    UserId = m.UserId,
                    Name = m.Name,
                    Departman = "Departman",
                    ImageUrl = m.ImageUrl,
                    Cheif = m.Cheif,
                    Status = m.Status.ToString()
                }).ToList();


            totalCount = userManager.Users.Where(m =>
                 (hasPermission || cheifUserList.Contains(m.Id)) && m.UserType != UserType.SuperCheif &&
                    (string.IsNullOrEmpty(userName) || m.UserName.Contains(userName) || m.Name.Contains(userName))
                    ).Count();
            var model = users;
            int pageInCount = users.Count;

            Object[] result = { model, totalCount, pageInCount };
            return Json(result, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Register()
        {
            return View();
        }

        private static readonly string _filePathFormat = "~/Files/";

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserEdit model)
        {
            var cheifId = Request.Form["role"];

            var refUserType = Request.Form["ddlUserType"];
            if (ModelState.IsValid || Convert.ToInt32(Request.Form["ddlUserType"]) > -1 || string.IsNullOrEmpty(cheifId))
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
                            fileModel.FileType = FileType.Image;
                            fileModel.OwnerType = FileOwnerType.User;
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
                    //if (model.FileId > 0)
                    //{
                    //    model.Deleted = true;
                    //    Db.Files.Attach(model);
                    //    Db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    //    Db.SaveChanges();
                    //}

                    //return Json(new { status = "error", message = ex.Message }, JsonRequestBehavior.AllowGet);
                }

                ApplicationUser user = db.Users.Where(m => m.Id == model.Id).FirstOrDefault();

                user.Name = model.Name;
                user.Surname = model.Surname;
                user.Email = model.Email;
                user.UserName = model.Username;
                if (fileModel != null)
                {
                    user.File = fileModel;
                    user.FileId = fileModel.FileId;
                }
                string identityUserId = User.Identity.GetUserId();
                user.LastUpdatedByUser = db.Users.FirstOrDefault(x => x.Id == identityUserId);
                user.LastUpdatedDate = DateTime.Now;
                user.ActiveStatus = ActiveStatus.Active;
                user.Cheif = db.Users.FirstOrDefault(x => x.Id == cheifId);
                UserType? userType = null;

                if (refUserType != null && refUserType != "" && Convert.ToInt32(Request.Form["ddlUserType"]) > -1)
                {
                    userType = (UserType)Convert.ToInt32(Request.Form["ddlUserType"]);

                    if (userType == UserType.SuperCheif)
                        user.UserType = UserType.SuperCheif;
                    else if (userType == UserType.Cheif)
                        user.UserType = UserType.Cheif;
                    else if (userType == UserType.User)
                        user.UserType = UserType.User;
                }

                IdentityResult iResult = IdentityResult.Success; //userManager.Update(user);
                db.Users.Attach(user);
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();


                if (iResult.Succeeded)
                {
                    //    // User isminde bir Role ataması yapacağız. Bu rolü ilerleyen kısımda oluşturacağız                    
                    //    if (refUserType != null && refUserType != "" && Convert.ToInt32(Request.Form["ddlUserType"]) > -1)
                    //    {
                    //        userType = (UserType)Convert.ToInt32(Request.Form["ddlUserType"]);

                    //        if (userType == UserType.SuperCheif)
                    //            userManager.AddToRole(user.Id, "SuperCheif");
                    //        else if (userType == UserType.Cheif)
                    //            userManager.AddToRole(user.Id, "Cheif");
                    //        else if (userType == UserType.User)
                    //            userManager.AddToRole(user.Id, "User");
                    //    }



                    return RedirectToAction("Index", "User");
                    //return Json(new { ftype = fileModel.FileType, fid = fileModel.FileId, savedresult = true, status = "success" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    ModelState.AddModelError("RegisterUser", "Kullanıcı ekleme işleminde hata!");
                }


                //db.SaveChanges();
            }

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register model)
        {
            var cheifId = Request.Form["role"];

            var refUserType = Request.Form["ddlUserType"];
            if (ModelState.IsValid || Convert.ToInt32(Request.Form["ddlUserType"]) > -1 || string.IsNullOrEmpty(cheifId))
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
                            fileModel.FileType = FileType.Image;
                            fileModel.OwnerType = FileOwnerType.User;
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
                    //if (model.FileId > 0)
                    //{
                    //    model.Deleted = true;
                    //    Db.Files.Attach(model);
                    //    Db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    //    Db.SaveChanges();
                    //}

                    //return Json(new { status = "error", message = ex.Message }, JsonRequestBehavior.AllowGet);
                }

                ApplicationUser user = new ApplicationUser();

                user.Name = model.Name;
                user.Surname = model.Surname;
                user.Email = model.Email;
                user.UserName = model.Username;
                if (fileModel != null)
                {
                    user.File = fileModel;
                    user.FileId = fileModel.FileId;
                }
                string userId = User.Identity.GetUserId();
                user.CreatedByUser = db.Users.FirstOrDefault(x => x.Id == userId);
                user.CreatedDate = DateTime.Now;
                user.ActiveStatus = ActiveStatus.Active;
                user.Cheif = db.Users.FirstOrDefault(x => x.Id == cheifId);
                UserType? userType = null;
                if (refUserType != null && refUserType != "" && Convert.ToInt32(Request.Form["ddlUserType"]) > -1)
                {
                    userType = (UserType)Convert.ToInt32(Request.Form["ddlUserType"]);

                    if (userType == UserType.SuperCheif)
                        user.UserType = UserType.SuperCheif;
                    else if (userType == UserType.Cheif)
                        user.UserType = UserType.Cheif;
                    else if (userType == UserType.User)
                        user.UserType = UserType.User;
                }

                IdentityResult iResult = userManager.Create(user, model.Password);

                if (iResult.Succeeded)
                {
                    // User isminde bir Role ataması yapacağız. Bu rolü ilerleyen kısımda oluşturacağız                    
                    if (refUserType != null && refUserType != "" && Convert.ToInt32(Request.Form["ddlUserType"]) > -1)
                    {
                        userType = (UserType)Convert.ToInt32(Request.Form["ddlUserType"]);

                        if (userType == UserType.SuperCheif)
                            userManager.AddToRole(user.Id, "SuperCheif");
                        else if (userType == UserType.Cheif)
                            userManager.AddToRole(user.Id, "Cheif");
                        else if (userType == UserType.User)
                            userManager.AddToRole(user.Id, "User");
                    }

                    return RedirectToAction("Index", "User");
                    //return Json(new { ftype = FileType.Image, fid = 1, savedresult = true, status = "success" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    ModelState.AddModelError("RegisterUser", "Kullanıcı ekleme işleminde hata!");
                }

                //var model2 = db.File.Where(s => s.FileId == fileModel.FileId).FirstOrDefault();                                
                //model2.UserId = user.Id;                
                db.SaveChanges();
            }
            //return Json(new { status = "error", message = "Error" }, JsonRequestBehavior.AllowGet);
            return View(model);
        }
    }
}
