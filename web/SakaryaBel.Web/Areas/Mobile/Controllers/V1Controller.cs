using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using SakaryaBel.Web.Areas.Mobile.Models;
using SakaryaBel.Web.DomainModel.Entities;
using SakaryaBel.Web.Enums;
using SakaryaBel.Web.Identity;
using SakaryaBel.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SakaryaBel.Web.Areas.Mobile.Controllers
{
    [AllowAnonymous]
    public class V1Controller : BaseMobileController
    {
        private UserManager<ApplicationUser> userManager;
        private RoleManager<ApplicationRole> roleManager;

        public V1Controller()
        {
            UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(db);
            userManager = new UserManager<ApplicationUser>(userStore);

            RoleStore<ApplicationRole> roleStore = new RoleStore<ApplicationRole>(db);
            roleManager = new RoleManager<ApplicationRole>(roleStore);
        }

        #region Faaliyet Operations

        //bu metod kullanıcının faaliyet listesini almak için. id kullanıcının userId'si,userid alanı boş ise id ye bakıp kulanıcının faaliyetleini getiricek,
        //userid dolu ise de userId ye girilen kullanıcının faaliyetleini getiricek. filtre faaliyet adına göre filtreleme için, 
        //skip,take de sayfalama için bi kerede bütün faaliyetleri çekmesin. 
        //userList de şef ve süper Şef için userList'e kullanıcı id'si gönderdiğinde o kullanıcının faaliyetlerini getiricek
        //gereksiz yere bi daha bi daha gelmemesi için toplam faliyet bilgisini de gönderiyorum. gelen faaliyet toplamı toplam a eşitse bi daha request çekmezsin.
        public async Task<HttpResponseMessage> GetActionList(string id, string userList, string filtre, int skip,
            int top, string token)
        {
            string username = "";
            HttpResponseMessage response;
            if (ValidateToken(token, out username, out response))
            {

                bool hasPermission = false;
                var cheifUserList = new List<string>();
                if (!string.IsNullOrEmpty(userList))
                {
                    foreach (var item in userList.Split(','))
                        cheifUserList.Add(item);
                }
                else
                {
                    string identityUserId = id;
                    cheifUserList.Add(identityUserId);
                }

                var activityList = new List<mAction>();
                int totalCount = 0;
                activityList = db.Activity
                    .Where(m =>
                        (cheifUserList.Contains(m.CreatedByUser.Id)) && m.ActivityType == ActivityType.Action &&
                        (string.IsNullOrEmpty(filtre) || m.Name.Contains(filtre))
                    )
                    .Select(m => new mAction
                    {
                        ActionId = m.ActivityId,
                        Name = m.Name,
                        Description = m.Description,
                        SubActionCount =
                            db.Activity.Where(l => l.ActionId == m.ActivityId && l.ActiveStatus == ActiveStatus.Active)
                                .Count()

                    }).OrderBy(m => m.Name).Skip(skip).Take(top).ToList();


                totalCount = db.Activity
                    .Where(m =>
                        (hasPermission || cheifUserList.Contains(m.CreatedByUser.Id)) &&
                        m.ActivityType == ActivityType.Action &&
                        (string.IsNullOrEmpty(filtre) || m.Name.Contains(filtre))
                    ).Count();


                response = Request.CreateResponse(HttpStatusCode.OK, activityList);
            }
            return response;
        }

        //Yeni faaliyet eklemek için bu metod
        [HttpPost]
        public object NewAction([FromBody] mNewAction model, string token)
        {
            string username = "";
            HttpResponseMessage response;
            if (ValidateToken(token, out username, out response))
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
                }

                response = Request.CreateResponse(HttpStatusCode.OK, model);
            }
            return response;
        }

        //ediletnmek istenen faaliyeti çeker
        public async Task<HttpResponseMessage> GetEditAction(int id, string token)
        {
            string username = "";
            HttpResponseMessage response;
            if (ValidateToken(token, out username, out response))
            {

                mNewAction act = db.Activity.Where(m => m.ActivityId == id)
                    .Select(m => new mNewAction {ActionId = m.ActivityId, Name = m.Name, Description = m.Description})
                    .FirstOrDefault();

                response = Request.CreateResponse(HttpStatusCode.OK, act);
            }
            return response;
        }

        //editlenen faliyeti kaydeder.
        [HttpPost]
        public object EditAction([FromBody] mNewAction model, string token)
        {
            string username = "";
            HttpResponseMessage response;
            if (ValidateToken(token, out username, out response))
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

                response = Request.CreateResponse(HttpStatusCode.OK, model);
            }
            return response;
        }

        #endregion


        #region AltFaliyet Operations

        //faliyete baglı Alt faaliyetleri almak için. actionId faaliyet id'si. digerleri aynı
        public async Task<HttpResponseMessage> GetSubActionList(int actionId, string filtre, int skip, int top,
            string token)
        {
            string username = "";
            HttpResponseMessage response;
            if (ValidateToken(token, out username, out response))
            {
                List<mAction> activityList = new List<mAction>();
                int totalCount = 0;
                activityList = db.Activity
                    .Where(m =>
                        m.ActivityType != ActivityType.Action && m.ActionId == actionId &&
                        m.ActiveStatus == ActiveStatus.Active &&
                        (string.IsNullOrEmpty(filtre) || m.Name.Contains(filtre))
                    )
                    .Select(m => new mAction
                    {
                        ActionId = m.ActivityId,
                        Name = m.Name,
                        Description = m.Description,
                        SubActionCount = 0,
                        CreatedDate = m.CreatedDate,
                        SubActionType = (int) m.ActivityType
                    }).OrderBy(m => m.Name).Skip(skip).Take(top).ToList();


                foreach (var item in activityList)
                    item.StringCreatedDate = item.CreatedDate.ToFDateTime(true);

                totalCount = db.Activity
                    .Where(m =>
                        m.ActivityType != ActivityType.Action && m.ActionId == actionId &&
                        m.ActiveStatus == ActiveStatus.Active &&
                        (string.IsNullOrEmpty(filtre) || m.Name.Contains(filtre))
                    ).Count();

                response = Request.CreateResponse(HttpStatusCode.OK, activityList);
            }
            return response;
        }


        //Yeni Alt faaliyet eklemek için bu metod
        [HttpPost]
        public object NewSubAction([FromBody] mNewAction model, string token)
        {
            string username = "";
            HttpResponseMessage response;
            if (ValidateToken(token, out username, out response))
            {
                if (ModelState.IsValid)
                {
                    Activity activity = new Activity();
                    activity.Name = model.Name;
                    activity.Description = model.Description;
                    activity.ActionId = model.ActionId;
                    activity.ActivityType = ActivityType.SubAction;
                    activity.IsCommon = false;
                    activity.ActiveStatus = ActiveStatus.Active;
                    activity.ActivityStatus = ActivityStatus.New;
                    activity.CreatedDate = DateTime.Now;
                    string userId = User.Identity.GetUserId();
                    activity.CreatedByUser = db.Users.FirstOrDefault(x => x.Id == userId);

                    db.Activity.Add(activity);
                    db.SaveChanges();
                }

                response = Request.CreateResponse(HttpStatusCode.OK, model);
            }
            return response;
        }


        #endregion


        #region Problem Operations

        public async Task<HttpResponseMessage> GetProblemList(string id, string userList, string filtre, int skip,
            int top, string token)
        {
            string username = "";
            HttpResponseMessage response;
            if (ValidateToken(token, out username, out response))
            {
                bool hasPermission = false;
                var cheifUserList = new List<string>();
                if (!string.IsNullOrEmpty(userList))
                {
                    foreach (var item in userList.Split(','))
                        cheifUserList.Add(item);
                }
                else
                {
                    string identityUserId = id;
                    cheifUserList.Add(identityUserId);
                }

                List<mProblemListModel> problemList = new List<mProblemListModel>();
                int totalCount = 0;
                problemList = db.Activity
                    .Where(m =>
                        (cheifUserList.Contains(m.CreatedByUser.Id)) && m.ActivityType == ActivityType.Problem &&
                        m.ActiveStatus == ActiveStatus.Active &&
                        //(problemStatus == -1 || m.ActivityStatus == (ActivityStatus)problemStatus) &&
                        (string.IsNullOrEmpty(filtre) || m.Name.Contains(filtre))
                    )
                    .Select(m => new mProblemListModel
                    {
                        ActionId = m.ActivityId,
                        Name = m.Name,
                        Description = m.Description,
                        BackGroundColor =
                            m.ActivityStatus == ActivityStatus.New
                                ? "kirmizi"
                                : m.ActivityStatus == ActivityStatus.Complete ? "yesil" : "mavi",
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
                        (hasPermission || cheifUserList.Contains(m.CreatedByUser.Id)) &&
                        m.ActivityType == ActivityType.Problem && m.ActiveStatus == ActiveStatus.Active &&
                        //(problemStatus == -1 || m.ActivityStatus == (ActivityStatus)problemStatus) &&
                        (string.IsNullOrEmpty(filtre) || m.Name.Contains(filtre))
                    ).Count();


                response = Request.CreateResponse(HttpStatusCode.OK, problemList);
            }
            return response;
        }

        [HttpPost]
        public object NewProblem([FromBody] mProblem model, string token)
        {
            string username = "";
            HttpResponseMessage response;
            if (ValidateToken(token, out username, out response))
            {
                if (ModelState.IsValid)
                {
                    //var directory = Server.MapPath(_filePathFormat);
                    File fileModel = null;
                    try
                    {
                        fileModel = new File();
                        //dosya yükleme için burası                                                                                 
                    }
                    catch (Exception ex)
                    {
                    }

                    Activity activity = new Activity();
                    string userId = "";
                    userId = User.Identity.GetUserId();
                    activity.CreatedByUser = db.Users.FirstOrDefault(x => x.Id == userId);

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
                }

                response = Request.CreateResponse(HttpStatusCode.OK, model);
            }
            return response;
        }

        //editlenmek istenen faaliyeti çeker
        public async Task<HttpResponseMessage> GetEditProblem(int id, string token)
        {
            string username = "";
            HttpResponseMessage response;
            if (ValidateToken(token, out username, out response))
            {

                mProblem model = db.Activity.Where(m => m.ActivityId == id).Select(m => new mProblem
                {
                    ProblemId = m.ActivityId,
                    Name = m.Name,
                    Description = m.Description,
                    FileUrl =
                        m.Files.Where(l => l.ActiveStatus == ActiveStatus.Active).FirstOrDefault() != null
                            ? m.Files.Where(l => l.ActiveStatus == ActiveStatus.Active).FirstOrDefault().FileName
                            : "",
                    FileId =
                        m.Files.Where(l => l.ActiveStatus == ActiveStatus.Active).FirstOrDefault() != null
                            ? m.Files.Where(l => l.ActiveStatus == ActiveStatus.Active).FirstOrDefault().FileId
                            : 0,
                    UserId = m.CreatedByUser.Id,
                    UserName = m.CreatedByUser.UserName + " - " + m.CreatedByUser.Name + " " + m.CreatedByUser.Surname,
                }).FirstOrDefault();

                response = Request.CreateResponse(HttpStatusCode.OK, model);
            }
            return response;
        }

        //editlenen problemi kaydeder.
        [HttpPost]
        public object EditProblem([FromBody] mProblem model, string token)
        {
            string username = "";
            HttpResponseMessage response;
            if (ValidateToken(token, out username, out response))
            {
                if (ModelState.IsValid)
                {
                    File fileModel = null;
                    try
                    {
                        //yeni dosya ekleme kısmı
                        fileModel = new File();


                    }
                    catch (Exception ex)
                    {

                    }

                    var activity = (from s in db.Activity.Include("Files")
                        where s.ActivityId == model.ProblemId.Value
                        select s).FirstOrDefault<Activity>();

                    string userId = "";
                    userId = User.Identity.GetUserId();
                    ApplicationUser user = db.Users.FirstOrDefault(x => x.Id == userId);
                    activity.LastUpdatedByUser = user;
                    activity.LastUpdatedDate = DateTime.Now;

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

                }

                response = Request.CreateResponse(HttpStatusCode.OK, model);
            }
            return response;
        }

        //problemin detayını alır.
        public async Task<HttpResponseMessage> GetProblemDetail(int id, string token)
        {
            string username = "";
            HttpResponseMessage response;
            if (ValidateToken(token, out username, out response))
            {
                mProblemDetailListModel act =
                    db.Activity.Where(m => m.ActivityId == id).Select(m => new mProblemDetailListModel
                    {
                        ActionId = m.ActivityId,
                        Name = m.Name,
                        Description = m.Description,
                        BackGroundColor =
                            m.ActivityStatus == ActivityStatus.New
                                ? "kirmizi"
                                : m.ActivityStatus == ActivityStatus.Complete ? "yesil" : "mavi",
                        CreatedDate = m.CreatedDate,
                        CompletedDate = m.CompletedDate,
                        FileType =
                            m.Files.Where(l => l.ActiveStatus == ActiveStatus.Active).FirstOrDefault() != null
                                ? m.Files.Where(l => l.ActiveStatus == ActiveStatus.Active).FirstOrDefault().FileType
                                : FileType.Image,
                        FileName =
                            m.Files.Where(l => l.ActiveStatus == ActiveStatus.Active).FirstOrDefault() != null
                                ? m.Files.Where(l => l.ActiveStatus == ActiveStatus.Active).FirstOrDefault().FileName
                                : "/Files/user_unknown.png",
                        ProblemOfActId = m.ActionId,
                        CreatedUserId = m.CreatedByUser.Id,
                        messages = m.Messages.Select(l => new mMessageDetailListModel
                        {
                            MessageId = l.MessageId,
                            Body = l.Body,
                            SenderUserId = l.CreatedByUser.Id,
                            SenderUserName =
                                l.CreatedByUser.UserName + " - " + l.CreatedByUser.Name + " " + l.CreatedByUser.Surname,
                            SenderProfilImage =
                                l.CreatedByUser.File != null
                                    ? "/Files/" + l.CreatedByUser.File.FileName
                                    : "/Files/user_unknown.png",
                            CreatedDate = l.CreatedDate
                        }).ToList()
                    }).FirstOrDefault();

                if (act.ProblemOfActId.HasValue)
                {
                    act.ActId = act.ProblemOfActId.ToString();
                    act.ActionName = db.Activity.Find(act.ProblemOfActId.Value).Name;
                }

                response = Request.CreateResponse(HttpStatusCode.OK, act);
            }
            return response;
        }

        [HttpPost]
        public object NewProblemMesaj(int problemId, string mesaj, string token)
        {
            string username = "";
            HttpResponseMessage response;
            if (ValidateToken(token, out username, out response))
            {
                string decodeMesaj = mesaj;

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

                response = Request.CreateResponse(HttpStatusCode.OK, createdDate);
            }
            return response;
        }

        //problemin durumunu yeni, tamamlandı, şef devret yapmak için kullanılır.
        [HttpPost]
        public object ChangeProblemStatus(int problemId, int status, string token)
        {
            string username = "";
            HttpResponseMessage response;
            if (ValidateToken(token, out username, out response))
            {
                var p = db.Activity.Find(problemId);
                p.ActivityStatus = (ActivityStatus) status;
                if ((ActivityStatus) status == ActivityStatus.Complete)
                    p.CompletedDate = DateTime.Now;

                string userId = User.Identity.GetUserId();
                p.LastUpdatedByUser = db.Users.FirstOrDefault(x => x.Id == userId);
                p.LastUpdatedDate = DateTime.Now;
                db.Activity.Attach(p);
                db.Entry(p).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                response = Request.CreateResponse(HttpStatusCode.OK, status);
            }
            return response;
        }


        //verilen faaliyetId(actionId)'ye göre problemi o faaliyeyin altına alt faaliyet olarak atar.
        [HttpPost]
        public object ChangeProblemOfAction(int problemId, string actionId, string token)
        {
            string username = "";
            HttpResponseMessage response;
            if (ValidateToken(token, out username, out response))
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

                response = Request.CreateResponse(HttpStatusCode.OK, actionId);
            }
            return response;
        }

        //userid si verilen kullanıcının faaliyetleini getirir. buradaki amaç gelen faaliyet listesinden bi faliyet seçip problemi o faaliyetin altına atmak
        public async Task<HttpResponseMessage> GetActionList(string userId, string token)
        {
            string username = "";
            HttpResponseMessage response;
            if (ValidateToken(token, out username, out response))
            {
                List<selectList> actionList =
                    db.Activity.Where(
                        m =>
                            m.CreatedByUser.Id == userId && m.ActiveStatus == ActiveStatus.Active &&
                            m.ActivityType == ActivityType.Action).Select(m => new
                            {
                                Id = m.ActivityId,
                                Text = m.Name
                            }).ToList().Select(m => new selectList
                            {
                                Id = m.Id.ToString(),
                                Text = m.Text
                            }).ToList();

                response = Request.CreateResponse(HttpStatusCode.OK, actionList);
            }
            return response;
        }

        #endregion


        #region User Operations

        public async Task<HttpResponseMessage> GetUserById(string id, string token)
        {
            string username = "";
            HttpResponseMessage response;
            if (ValidateToken(token, out username, out response))
            {
                if (string.IsNullOrEmpty(username)) response = new HttpResponseMessage(HttpStatusCode.BadRequest);

                var result = await userManager.FindByIdAsync(id);

                object model = null;
                if (result != null)
                {
                    mUser user = new mUser();
                    user.UserId = result.Id;
                    user.FirstName = result.Name;
                    user.LastName = result.Surname;
                    user.UserName = result.UserName;
                    user.Email = result.Email;
                    user.UserType = (int) result.UserType;
                    user.IsOnline = true;
                    user.ProfileImage = "/Files/" + result.File.FileName;
                    model = user;
                }
                response = Request.CreateResponse(HttpStatusCode.OK, model);
            }
            return response;
        }

        public async Task<HttpResponseMessage> GetUserByName(string username, string token)
        {
            string caller = "";
            HttpResponseMessage response;
            if (ValidateToken(token, out caller, out response))
            {
                if (string.IsNullOrEmpty(username)) response = new HttpResponseMessage(HttpStatusCode.BadRequest);

                var result = await userManager.FindByNameAsync(username);

                object model = null;
                if (result != null)
                {
                    mUser user = new mUser();
                    user.UserId = result.Id;
                    user.FirstName = result.Name;
                    user.LastName = result.Surname;
                    user.UserName = result.UserName;
                    user.Email = result.Email;
                    user.UserType = (int) result.UserType;
                    user.IsOnline = true;
                    user.ProfileImage = "/Files/" + result.File.FileName;
                    model = user;

                }

                response = Request.CreateResponse(HttpStatusCode.OK, model);
            }
            return response;
        }

        public async Task<HttpResponseMessage> GetAuthToken(string username, string password)
        {
            HttpResponseMessage response;

            Login loginModel = new Login()
            {
                Username = username,
                Password = password,
                RememberMe = false,
            };

            object model = null;

            ApplicationUser user = userManager.Find(loginModel.Username, loginModel.Password);

            if (user != null)
            {

                IAuthenticationManager authManager = HttpContext.Current.GetOwinContext().Authentication;


                ClaimsIdentity identity = userManager.CreateIdentity(user, "ApplicationCookie");
                AuthenticationProperties authProps = new AuthenticationProperties();
                authProps.IsPersistent = loginModel.RememberMe;
                authManager.SignIn(authProps, identity);

                var returnUser = new mUser();
                returnUser.UserId = user.Id;
                returnUser.UserName = user.UserName;
                returnUser.UserType = (int) user.UserType;
                returnUser.FirstName = user.Name;
                returnUser.LastName = user.Surname;
                returnUser.Email = user.Email;
                returnUser.IsOnline = true;

                model = new {UserId = user.Id, Token = GenerateToken(username), User = returnUser};

                response = Request.CreateResponse(HttpStatusCode.OK, model);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.Unauthorized, model);
            }

            return response;
        }

        //yeni kullanıcı
        [HttpPost]
        public object Register(mRegister model)
        {
            string username = "";
            HttpResponseMessage response;
            //if (ValidateToken(token, out username, out response))
            //{
                ApplicationUser user = new ApplicationUser();
                if (ModelState.IsValid)
                {

                    File fileModel = null;
                    try
                    {
                        fileModel = new File();
                    }
                    catch (Exception ex)
                    {
                    }

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
                    user.Cheif = db.Users.Where(x => x.Id == model.CheifId).FirstOrDefault();
                    UserType? userType = null;
                    if (model.UserType != null && Convert.ToInt32(model.UserType) > -1)
                    {
                        userType = (UserType) Convert.ToInt32(model.UserType);

                        if (userType == UserType.SuperCheif)
                            user.UserType = UserType.SuperCheif;
                        else if (userType == UserType.Cheif)
                            user.UserType = UserType.Cheif;
                        else if (userType == UserType.User)
                            user.UserType = UserType.User;
                    }
                    IdentityResult iResult;
                    try
                    {
                        iResult = userManager.Create(user, model.Password);

                    }
                    catch (DbEntityValidationException e)
                    {
                        
                        throw;
                    }


                    if (iResult.Succeeded)
                    {
                        // User isminde bir Role ataması yapacağız. Bu rolü ilerleyen kısımda oluşturacağız                    
                        if (model.UserType != null && Convert.ToInt32(model.UserType) > -1)
                        {
                            userType = (UserType) Convert.ToInt32(model.UserType);

                            if (userType == UserType.SuperCheif)
                                userManager.AddToRole(user.Id, "SuperCheif");
                            else if (userType == UserType.Cheif)
                                userManager.AddToRole(user.Id, "Cheif");
                            else if (userType == UserType.User)
                                userManager.AddToRole(user.Id, "User");
                        }

                    }


                    db.SaveChanges();
                }

                response = Request.CreateResponse(HttpStatusCode.OK, user);
            //}
            return response;
        }

        //yeni kullanıcı eklenir iken veya kullanıcı düzenlenir iken gelecen şef listesi
        public async Task<HttpResponseMessage> GetCheif(string searchTerm, string token)
        {
            string username = "";
            HttpResponseMessage response;
            if (ValidateToken(token, out username, out response))
            {

                List<selectList> userList =
                    userManager.Users.Where(
                        m =>
                            m.ActiveStatus == ActiveStatus.Active &&
                            (m.UserType == UserType.Cheif || m.UserType == UserType.SuperCheif))
                        .Select(m => new selectList
                        {
                            Id = m.Id,
                            Text = m.UserName + " - " + m.Name + " " + m.Surname
                        })
                        .ToList();

                response = Request.CreateResponse(HttpStatusCode.OK, userList);
            }
            return response;
        }

        //kullanıcıyı silmeki pasif e çekmek veya actif etmek için metod.
        public async Task<HttpResponseMessage> ChangeUserStatus(string userId, int status, string token)
        {
            string username = "";
            HttpResponseMessage response;
            if (ValidateToken(token, out username, out response))
            {

                var p = db.Users.Find(userId);
                p.ActiveStatus = (ActiveStatus) status;
                string updaterUserId = User.Identity.GetUserId();
                p.LastUpdatedByUser = db.Users.FirstOrDefault(x => x.Id == updaterUserId);
                p.LastUpdatedDate = DateTime.Now;
                db.Users.Attach(p);
                db.Entry(p).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                response = Request.CreateResponse(HttpStatusCode.OK, status);
            }
            return response;
        }

        //şef ve yöneticilerin kendilerine baglı kullanıcları görecekleri listeyi dolduran metod.
        public async Task<HttpResponseMessage> GetUserList(string filtre, int skip, int top, string token)
        {
            string username = "";
            HttpResponseMessage response;
            if (ValidateToken(token, out username, out response))
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

                List<mUser> users = new List<mUser>();
                int totalCount = 0;
                users = userManager.Users
                    .Where(m =>
                        (hasPermission || cheifUserList.Contains(m.Id)) && m.UserType != UserType.SuperCheif &&
                        (string.IsNullOrEmpty(filtre) || m.UserName.Contains(filtre) || m.Name.Contains(filtre))
                    )
                    .Select(m => new
                    {
                        UserId = m.Id,
                        UserName = m.UserName,
                        FirstName = m.Name,
                        LastName = m.Surname,
                        ImageUrl = m.FileId.HasValue ? "/Files/" + m.File.FileName : "/Files/user_unknown.png",
                        Cheif = m.Cheif.UserName + " - " + m.Cheif.Name + " " + m.Cheif.Surname,
                        Status = m.ActiveStatus
                    }).OrderBy(m => m.UserName).Skip(skip).Take(top).ToList().Select(m => new mUser
                    {
                        UserId = m.UserId,
                        UserName = m.UserName,
                        FirstName = m.FirstName,
                        LastName = m.LastName,
                        ProfileImage = m.ImageUrl,
                        Cheif = m.Cheif,
                        Status = m.Status.ToString()
                    }).ToList();


                totalCount = userManager.Users.Where(m =>
                    (hasPermission || cheifUserList.Contains(m.Id)) && m.UserType != UserType.SuperCheif &&
                    (string.IsNullOrEmpty(filtre) || m.UserName.Contains(filtre) || m.Name.Contains(filtre))
                    ).Count();

                response = Request.CreateResponse(HttpStatusCode.OK, users);
            }
            return response;
        }

        [HttpPost]
        public async Task<object> ChangePassword(mChangePasswordViewModel model, string token)
        {
            string username = "";
            HttpResponseMessage response;
            if (ValidateToken(token, out username, out response))
            {
                if (!ModelState.IsValid)
                {
                    var result = await userManager.ChangePasswordAsync(model.Id, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        response = Request.CreateResponse(HttpStatusCode.OK, username);
                    }
                }
            }
            return response;
        }


        public async Task<HttpResponseMessage> GetUserEdit(string id, string token)
        {
            string username = "";
            HttpResponseMessage response;
            if (ValidateToken(token, out username, out response))
            {
                var user = db.Users.Where(m => m.Id == id).Select(m => new mUserEdit
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

                response = Request.CreateResponse(HttpStatusCode.OK, user);
            }
            return response;
        }

        [HttpPost]
        public object UserEdit(mUserEdit model, string token)
        {
            string username = "";
            HttpResponseMessage response;
            if (ValidateToken(token, out username, out response))
            {
                if (ModelState.IsValid)
                {
                    File fileModel = null;
                    try
                    {
                        fileModel = new File();
                    }
                    catch (Exception ex)
                    {

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
                    user.Cheif = db.Users.Where(m => m.Id == model.CheifId).FirstOrDefault();
                    UserType? userType = null;

                    if (model.UserType != null && Convert.ToInt32(model.UserType) > -1)
                    {
                        userType = model.UserType;

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
                        //user rol ekleme çıakrma işi burada yapılacak             

                    }

                    response = Request.CreateResponse(HttpStatusCode.OK, user);
                }                
            }
            return response;
        }   

#endregion

    }
}