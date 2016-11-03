#region using
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using WitKeyDu.Site.Models;
using WitKeyDu.Component.Tools;
using WitKeyDu.Component.Tools.Extensions;
using WitKeyDu.Site.Helper.Logging;
using WitKeyDu.Site.Impl;
using WitKeyDu.Component.Tools.ThirdRegion;
using WitKeyDu.Core.Models.Account;
using Webdiyer.WebControls.Mvc;
using System.Text;
using System.Collections;
using WitKeyDu.Core.Models.Forums;
using System.Text.RegularExpressions;
using WitKeyDu.Core.Models.Tasks;
using WitKeyDu.Core.Models.Stores;
using WitKeyDu.Core.Models.Files;
using System.IO;
#endregion

namespace WitKeyDu.Site.Web.Controllers
{
    [Export]
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        #region 属性

        [Import]
        public IAccountSiteContract AccountContract { get; set; }
        private static Dictionary<String, IOAuthClient> m_oauthClients = null;
        [Import]
        IForumModuleSiteContract ForumModuleSiteContract { get; set; }
        [Import]
        ITaskModuleSiteContract TaskModuleSiteContract { get; set; }
        [Import]
        IStoreModuleSiteContract StoreModuleSiteContract { get; set; }
        [Import]
        IFileModuleSiteContract FileModuleSiteContract { get; set; }
        [Import]
        public ITaskPlanSiteContract TaskPlanSiteContract { get; set; }
        [Import]//最好换成autofac，不要  MEF
        public IPlanPaceSiteContract PlanPaceSiteContract { get; set; }
        //图片保存字段
        private static string imgurl = "";
        #endregion

        #region 登录视图功能

        public ActionResult Login()
        {
            GetValidateCode();
            string returnUrl = Request.Params["returnUrl"];
            returnUrl = returnUrl ?? Url.Action("Index", "Home", new { area = "" });
            LoginModel model = new LoginModel
            {
                ReturnUrl = returnUrl
            };
            return View(model);
        }

        public ActionResult GetValidateCode()
        {
            RandomCheckNum checkCode = new RandomCheckNum();
            string code = checkCode.CreateVerifyCode(4);
            Session["checkcode"] = code;
            byte[] bytes = checkCode.CreateImageOnPage(code);
            return File(bytes, @"image/jpeg");
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            try
            {
                OperationResult result = AccountContract.Login(model, Session["checkcode"].ToString());
                string msg = result.Message ?? result.ResultType.ToDescription();
                if (result.ResultType == OperationResultType.Success)
                {
                    return Redirect(model.ReturnUrl);
                }
                ModelState.AddModelError("", msg);
                return View(model);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View(model);
            }
        }

        #endregion
        #region  第三方登录
        [HttpGet]
        public ActionResult ThirdPartyTBLogin()
        {
            //初始化开放平台客户端（请替换成自己的ClientId，ClientScrert，CallbackUrl）
            m_oauthClients = new Dictionary<string, IOAuthClient>();
            m_oauthClients["taobao"] = new TaoBaoClient("You ClientId", "You ClientScrert", "You Callback Url");
            //OAuthTest("taobao");

            return null;
        }
        [HttpGet]
        public ActionResult ThirdPartyWBLogin()
        {
            m_oauthClients = new Dictionary<string, IOAuthClient>();
            m_oauthClients["sinaweibo"] = new SinaWeiBoClient("You ClientId", "You ClientScrert", "You Callback Url");
            OAuthTest("sinaweibo");
            return null;
        }
        [HttpGet]
        public ActionResult ThirdPartyQQLogin()
        {
            //初始化开放平台客户端（请替换成自己的ClientId，ClientScrert，CallbackUrl）
            m_oauthClients = new Dictionary<string, IOAuthClient>();
            m_oauthClients["qq"] = new TencentQQClient("1105076318", "XTW18lsh0f06SEwi", "http://192.168.1.168:5025");
            OAuthTest("qq");
            return null;
        }
        private  void OAuthTest(String platformCode)
        {
            String authorizeUrl = String.Empty;
            if (String.IsNullOrEmpty(platformCode)) platformCode = "qq";

            //Console.WriteLine("OpenPlatform Request For " + platformCode);
            //Console.WriteLine("");

            IOAuthClient oauthClient = m_oauthClients[platformCode];
            oauthClient.Option.State = platformCode;

            //第一步：获取开放平台授权地址
            authorizeUrl = m_oauthClients[platformCode].GetAuthorizeUrl(ResponseType.Code);
            //Console.WriteLine("Step 1 - OAuth2.0 for Redirect AuthorizeUrl: ");
            //Console.WriteLine(authorizeUrl);

            //第二步：打开IE浏览器获取Code
            Process p = new Process();
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.Arguments = authorizeUrl;
            psi.FileName = "C:\\Program Files\\Internet Explorer\\iexplore.exe";
            p.StartInfo = psi;
            p.Start();

            //Console.WriteLine("");
            //Console.WriteLine("OAuth2.0 Input Server Response Code");
            String code = Console.ReadLine();

            //第三步：获取开放平台授权令牌
            oauthClient = m_oauthClients[platformCode];
            AuthToken accessToken = oauthClient.GetAccessTokenByAuthorizationCode(code);
            if (accessToken != null)
            {
                //Console.WriteLine("");
                //Console.WriteLine("Step 2 - OAuth2.0 for AccessToken: " + accessToken.AccessToken);
                ////输出原始响应数据
                //Console.WriteLine("GetAccessToken Raw Response : ");
                //Console.WriteLine(oauthClient.Token.TraceInfo);

                //第四步：调用开放平台API，获取开放平台用户信息
                dynamic oauthProfile = oauthClient.User.GetUserInfo();

                ////输出解析出来的用户昵称
                //Console.WriteLine("");
                //Console.WriteLine("Step 3 - Call Open API UserInfo: ");
                //Console.WriteLine("UserInfo Nickname: " + oauthClient.Token.User.Nickname);
                ////输出原始响应数据
                //Console.WriteLine("GetUserInfo Raw Response : ");
                //Console.WriteLine(oauthClient.Token.TraceInfo);
            }
        }
        #endregion
        #region 退出请求
        [HttpPost]
        public ActionResult Logout()
        {
            string returnUrl = Request.Params["returnUrl"];

            returnUrl = returnUrl ?? Url.Action("Index", "Home", new { area = "" });

            if (User.Identity.IsAuthenticated)
            {

                AccountContract.Logout();

            }

            return Redirect(returnUrl);
        }
        #endregion
        #region 注册视图功能
        [HttpGet]
        public ActionResult Register()
        {
            string returnUrl = Request.Params["returnUrl"];
            returnUrl = returnUrl ?? Url.Action("Login", "Account");
            RegisterModel model = new RegisterModel
            {
                ReturnUrl = returnUrl
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            try
            {
                OperationResult result = AccountContract.Register(model);

                string msg = result.Message ?? result.ResultType.ToDescription();

                if (result.ResultType == OperationResultType.Success)
                {
                    return Redirect(model.ReturnUrl);
                }

                ModelState.AddModelError("", msg);

                return View(model);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View(model);
            }
        }

         [HttpGet]
        public ActionResult UserList(int? id)
        {
            int pageIndex = id ?? 1;
            const int pageSize = 20;
            PropertySortCondition[] sortConditions = new[] { new PropertySortCondition("Id") };
            int total;
            var memberViews = AccountContract.SysUsers.Where<SystemUser, int>(m => true, pageIndex, pageSize, out total, sortConditions).Select(m => new MemberView
            {
                UserName = m.UserName,
                NickName = m.NickName,
                Email = m.Email,
                IsDeleted = m.IsDeleted,
                AddDate = m.AddDate,
                LoginLogCount = m.LoginLogs.Count,
                RoleNames = m.Roles.Select(n => n.Name)
            });
            PagedList<MemberView> model = new PagedList<MemberView>(memberViews, pageIndex, pageSize, total);
            return View(model);
        }
        
        [HttpGet]
        public ActionResult HeadImage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadAction()
        {
            string fileName = Guid.NewGuid().ToString();

            //遍历所有文件域
            foreach (string fieldName in Request.Files.AllKeys)
            {
                HttpPostedFileBase file = Request.Files[fieldName];
                string virtualPath = string.Format("~/Images/HeadImage/{0}.jpg", fileName);
                string imagePath = string.Format("../../Images/HeadImage/{0}.jpg", fileName);
                file.SaveAs(Server.MapPath(virtualPath));
                MemberView updateInfo=new MemberView();
                updateInfo.HeadImage = imagePath;
                updateInfo.UserName = ((SystemUser)Session["SystemUser"]).UserName;
                updateInfo.IsDeleted = ((SystemUser)Session["SystemUser"]).IsDeleted;
                updateInfo.IsStartUse = ((SystemUser)Session["SystemUser"]).IsStartUse;
                updateInfo.Id = ((SystemUser)Session["SystemUser"]).Id;
                updateInfo.NickName = ((SystemUser)Session["SystemUser"]).NickName;
                updateInfo.Password = ((SystemUser)Session["SystemUser"]).Password;
                updateInfo.AddDate = ((SystemUser)Session["SystemUser"]).AddDate;
                updateInfo.ContactNumber = ((SystemUser)Session["SystemUser"]).ContactNumber;
                updateInfo.Email = ((SystemUser)Session["SystemUser"]).Email;
                ((SystemUser)Session["SystemUser"]).HeadImage = imagePath;
                AccountContract.Update(updateInfo);
            }
            OperationResultType result = OperationResultType.Success;
            return Json(result);
        }
        #endregion
        #region 个人中心
        [HttpGet]
        public ActionResult UserInfo(int? id)
        {
            if (Session["SystemUser"] != null)
            {
                int UserID = ((SystemUser)Session["SystemUser"]).Id;
                //个人信息
                SystemUser SystemUser = AccountContract.SysUsers.FirstOrDefault(m => m.Id == UserID);
                MemberView userInfo = new MemberView
                {
                    Id=SystemUser.Id,
                    UserName = SystemUser.UserName,
                    Email = SystemUser.Email,
                    Password=SystemUser.Password,
                    ContactNumber = SystemUser.ContactNumber,
                    HeadImage = SystemUser.HeadImage,
                    NickName = SystemUser.NickName,
                };
                ViewData["userInfo"] = userInfo;
                //文章信息
                int pageIndex = id ?? 1;
                const int pageSize = 20;
                PropertySortCondition[] sortConditions = new[] { new PropertySortCondition("Id") };
                int total;
                IQueryable forumList = ForumModuleSiteContract.Forums.Where<Forum, int>(m => m.SystemUserID == UserID, pageIndex, pageSize, out total, sortConditions);
                List<ForumListView> forumListViews = new List<ForumListView>();
                foreach (Forum m in forumList)
                {
                    ForumListView forum = new ForumListView
                    {
                        Id = m.Id,
                        ForumLogo = m.ForumLogo,
                        ForumContent = GetContentSummary(m.ForumContent, 200, true),
                        ForumName = m.ForumName,
                        AddDate = m.AddDate
                    };
                    forumListViews.Add(forum);
                }
                PagedList<ForumListView> forummodel = new PagedList<ForumListView>(forumListViews, pageIndex, pageSize, total);
                ViewData["ForumListView"] = forummodel;
                //需求信息
                var taskListViews = TaskModuleSiteContract.Tasks.Where<Task, int>(m => m.TaskState != "已完成" && m.SystemUserID == UserID, pageIndex, pageSize, out total, sortConditions).OrderByDescending(m => m.AddDate).Select(m => new TaskListView
                {
                    Id = m.Id,
                    TaskLogo = m.TaskLogo,
                    TaskName = m.TaskName,
                    TaskReward = m.TaskReward,
                    TaskStartTime = m.TaskStartTime,
                    TaskEndTime = m.TaskEndTime,
                    DayNum = m.DayNum,
                    TaskState = m.TaskState
                });
                PagedList<TaskListView> taskmodel = new PagedList<TaskListView>(taskListViews, pageIndex, pageSize, total);
                ViewData["TaskListView"] = taskmodel;
                //需求信息
                var mytasklist = TaskModuleSiteContract.Tasks.Where<Task, int>(m => m.TaskState == "开发中" && m.Employer == UserID, pageIndex, pageSize, out total, sortConditions).OrderByDescending(m => m.AddDate).Select(m => new TaskListView
                {
                    Id = m.Id,
                    TaskLogo = m.TaskLogo,
                    TaskName = m.TaskName,
                    TaskReward = m.TaskReward,
                    TaskStartTime = m.TaskStartTime,
                    TaskEndTime = m.TaskEndTime,
                    DayNum = m.DayNum,
                    TaskState = m.TaskState
                });
                PagedList<TaskListView> mytaskmodel = new PagedList<TaskListView>(mytasklist, pageIndex, pageSize, total);
                ViewData["MyTaskList"] = mytaskmodel;
                //店铺信息
                IQueryable Store = StoreModuleSiteContract.Stores.Where(m => m.SystemUserID == UserID);
                string StoreID = null;
                foreach (WitKeyDu.Core.Models.Stores.Store m in Store)
                {
                    StoreID = m.Id.ToString();
                }
                if (StoreID != null)
                {
                    ViewBag.StoreUrl = " ../StoreModule/StoreDetail?StoreID=" + StoreID + "";
                }
                else
                {
                    ViewBag.StoreUrl = "../StoreModule/CreateStore";
                }
                //资源信息
                IQueryable fileModel = FileModuleSiteContract.Files.Where<WitKeyDu.Core.Models.Files.File, int>(m => m.SystemUserID == UserID, pageIndex, pageSize, out total, sortConditions);
                List<FileView> fileList = new List<FileView>();
                foreach (WitKeyDu.Core.Models.Files.File m in fileModel)
                {
                    SystemUser SysUser = AccountContract.SysUsers.FirstOrDefault(user => user.Id == m.SystemUserID);
                    FileView file = new FileView
                    {
                        FileCode = m.FileCode,
                        FileName = m.FileName,
                        FileIntroduction = m.FileIntroduction,
                        HeadImg = SysUser.HeadImage,
                        UserName = SysUser.UserName,
                        UserKey = SysUser.Id
                    };
                    fileList.Add(file);
                }
                PagedList<FileView> filelist = new PagedList<FileView>(fileList, pageIndex, pageSize, total);
                ViewData["FileView"] = filelist;
                return View();
            }
            else
            {
                return View("Login");
            }
        }
        [HttpPost]
        public ActionResult updateUserInfo(MemberView user)
        {
            SystemUser SystemUser = AccountContract.SysUsers.FirstOrDefault(m => m.Id == user.Id);
            user.AddDate = SystemUser.AddDate;
            user.HeadImage = SystemUser.HeadImage;
            AccountContract.Update(user);
            return View();
        }

        [HttpGet]
        public ActionResult CommitTask()
        {
            int taskId = int.Parse(Request["TaskID"]);
            TaskPlan taskPlan = TaskPlanSiteContract.TaskPlans.SingleOrDefault(m => m.TaskID == taskId);
            ViewBag.ID = taskPlan.Id;
            ViewBag.PlanCode = taskPlan.PlanCode;
            ViewBag.SystemUserID = taskPlan.SystemUserID;
            ViewBag.PlanContent = taskPlan.PlanContent;
            ViewBag.PlanPrice = taskPlan.PlanPrice;
            IQueryable<PlanPaceView> PlanPaceList = PlanPaceSiteContract.PlanPaces.Where(m => m.PlanCode == taskPlan.PlanCode).Select(m => new PlanPaceView
            {
                PlanID=m.Id,  
                PlanStartTime = m.PlanStartTime,
                PlanEndTime = m.PlanEndTime,
                CompleteContent = m.CompleteContent,
                Remark = m.Remark,
                FileSrc=m.FileSrc
            });
            return View(PlanPaceList);
        }

        [HttpPost]
        public ActionResult uploadtask(PlanPaceView planpace, HttpPostedFileBase myfile)
        {
            if (myfile == null)
            {
                return View();
            }
            var fileName = Path.Combine(Request.MapPath("~/Content/UpLoadFile/CommitFile/"), Path.GetFileName(myfile.FileName));
            try
            {
                myfile.SaveAs(fileName);
                planpace.FileSrc = "../Content/UpLoadFile/CommitFile//" + Path.GetFileName(myfile.FileName);
                return RedirectToAction("CommitTask");
            }
            catch
            {
                return Content("上传异常 ！", "text/plain");
            }
            return View();
        }

        /// <summary>
        /// 使用OutputStream.Write分块下载文件  
        /// </summary>
        /// <param name="filePath"></param>
        public ActionResult DownFile(string filePath, string fileName)
        {
            filePath = Server.MapPath(filePath);
            if (!System.IO.File.Exists(filePath))
            {
                return View();
            }
            FileInfo info = new FileInfo(filePath);
            //指定块大小   
            long chunkSize = 4096;
            //建立一个4K的缓冲区   
            byte[] buffer = new byte[chunkSize];
            //剩余的字节数   
            long dataToRead = 0;
            FileStream stream = null;
            try
            {
                //打开文件   
                stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);

                dataToRead = stream.Length;

                //添加Http头   
                System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
                System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachement;filename=" + Server.UrlEncode(fileName));
                System.Web.HttpContext.Current.Response.AddHeader("Content-Length", dataToRead.ToString());

                while (dataToRead > 0)
                {
                    if (System.Web.HttpContext.Current.Response.IsClientConnected)
                    {
                        int length = stream.Read(buffer, 0, Convert.ToInt32(chunkSize));
                        System.Web.HttpContext.Current.Response.OutputStream.Write(buffer, 0, length);
                        System.Web.HttpContext.Current.Response.Flush();
                        System.Web.HttpContext.Current.Response.Clear();
                        dataToRead -= length;
                    }
                    else
                    {
                        //防止client失去连接   
                        dataToRead = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Web.HttpContext.Current.Response.Write("Error:" + ex.Message);
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
                System.Web.HttpContext.Current.Response.Close();
            }
            return View();

        }
        #endregion

        #region 私有方法
        
        /// <summary>

        /// 提取摘要，是否清除HTML代码

        /// </summary>

        /// <param name="content"></param>

        /// <param name="length"></param>

        /// <param name="StripHTML">所返回摘要是否不包含Html标签：true不包含标签，false包含标签</param>

        /// <returns></returns>

        public string GetContentSummary(string content, int length, bool StripHTML)
        {
            if (string.IsNullOrEmpty(content) || length == 0)
                return "";
            if (StripHTML)
            {
                Regex re = new Regex("<[^>]*>");
                content = re.Replace(content, "");
                content = content.Replace("　", "").Replace(" ", "");
                if (content.Length <= length)
                    return content;
                else
                    return content.Substring(0, length) + "……";
            }
            else
            {
                if (content.Length <= length)

                    return content;
                int pos = 0, npos = 0, size = 0;
                bool firststop = false, notr = false, noli = false;
                StringBuilder sb = new StringBuilder();
                while (true)
                {
                    if (pos >= content.Length)
                        break;
                    string cur = content.Substring(pos, 1);
                    if (cur == "<")
                    {
                        string next = content.Substring(pos + 1, 3).ToLower();
                        if (next.IndexOf("p") == 0 && next.IndexOf("pre") != 0)
                        {
                            npos = content.IndexOf(">", pos) + 1;
                            sb.Append("    ");
                        }
                        else if (next.IndexOf("/p") == 0 && next.IndexOf("/pr") != 0)
                        {
                            npos = content.IndexOf(">", pos) + 1;
                            if (size < length)
                                sb.Append("<br/>");
                        }
                        else if (next.IndexOf("br") == 0)
                        {
                            npos = content.IndexOf(">", pos) + 1;
                            if (size < length)
                                sb.Append("<br/>");
                        }
                        else if (next.IndexOf("img") == 0)
                        {
                            npos = content.IndexOf(">", pos) + 1;
                            if (size < length)
                            {
                                sb.Append(content.Substring(pos, npos - pos));
                                size += npos - pos + 1;
                            }
                        }
                        else if (next.IndexOf("li") == 0 || next.IndexOf("/li") == 0)
                        {
                            npos = content.IndexOf(">", pos) + 1;
                            if (size < length)
                            {
                                sb.Append(content.Substring(pos, npos - pos));
                            }
                            else
                            {
                                if (!noli && next.IndexOf("/li") == 0)
                                {
                                    sb.Append(content.Substring(pos, npos - pos));
                                    noli = true;
                                }
                            }
                        }
                        else if (next.IndexOf("tr") == 0 || next.IndexOf("/tr") == 0)
                        {
                            npos = content.IndexOf(">", pos) + 1;
                            if (size < length)
                            {
                                sb.Append(content.Substring(pos, npos - pos));
                            }
                            else
                            {
                                if (!notr && next.IndexOf("/tr") == 0)
                                {
                                    sb.Append(content.Substring(pos, npos - pos));
                                    notr = true;
                                }
                            }
                        }
                        else if (next.IndexOf("td") == 0 || next.IndexOf("/td") == 0)
                        {

                            npos = content.IndexOf(">", pos) + 1;
                            if (size < length)
                            {
                                sb.Append(content.Substring(pos, npos - pos));

                            }
                            else
                            {
                                if (!notr)
                                {
                                    sb.Append(content.Substring(pos, npos - pos));
                                }
                            }
                        }
                        else
                        {
                            npos = content.IndexOf(">", pos) + 1;
                            sb.Append(content.Substring(pos, npos - pos));
                        }
                        if (npos <= pos)
                            npos = pos + 1;
                        pos = npos;
                    }
                    else
                    {
                        if (size < length)
                        {
                            sb.Append(cur);
                            size++;
                        }
                        else
                        {
                            if (!firststop)
                            {
                                sb.Append("……");
                                firststop = true;
                            }
                        }
                        pos++;
                    }
                }
                return sb.ToString();
            }

        }
        #endregion

    }
}
