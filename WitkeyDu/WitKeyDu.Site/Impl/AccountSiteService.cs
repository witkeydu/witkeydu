using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.Security;
using WitKeyDu.Component.Tools;
using WitKeyDu.Core.Impl;
using WitKeyDu.Core.Models;
using WitKeyDu.Core.Models.Account;
using WitKeyDu.Site.Models;


namespace WitKeyDu.Site.Impl
{
    /// <summary>
    ///     账户模块站点业务实现
    /// </summary>
    [Export(typeof(IAccountSiteContract))]
    internal class AccountSiteService : AccountService, IAccountSiteContract
    {
        HttpSessionState Session = HttpContext.Current.Session;
        HttpServerUtility Server = HttpContext.Current.Server;
        HttpRequest Request = HttpContext.Current.Request;
        HttpResponse Response = HttpContext.Current.Response;
        SystemUser user = new SystemUser();
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="model">注册模型信息</param>
        /// <returns>业务操作结果</returns>
        public OperationResult Login(LoginModel model,string CheckCode)
        {
            PublicHelper.CheckArgument(model, "model");
            LoginInfo loginInfo = new LoginInfo
            {
                Account = model.Account,
                Password = model.Password,
                CheckCode= model.CheckCode,
                IpAddress = HttpContext.Current.Request.UserHostAddress
            };
            OperationResult result = base.Login(loginInfo,CheckCode,ref user);

            if (result.ResultType == OperationResultType.Success)
            {
                Session["SystemUser"] = user;
            }
            return result;
        }
        /// <summary>
        ///     用户退出
        /// </summary>
        public void Logout()
        {
            FormsAuthentication.SignOut();
        }
        /// <summary>
        ///     用户登录
        /// </summary>
        /// <param name="model">登录模型信息</param>
        /// <returns>业务操作结果</returns>
        public OperationResult Register(RegisterModel model)
        {
            PublicHelper.CheckArgument(model, "model");
            SystemUser registerInfo = new SystemUser
            {
                UserName = model.Account,
                Password = model.Password,
                ContactNumber = model.ContectNumber,
                NickName = model.NickName,
                Email = model.Email,
            };
            OperationResult result = base.Register(registerInfo);
            return result;
        }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="model">修改模型信息</param>
        /// <returns>业务操作结果</returns>
        public OperationResult Update(MemberView model)
        {

            if (Session["SystemUser"] == "")
            {
                return new OperationResult(OperationResultType.PurviewLack, "用户尚未登录！");
            }
            PublicHelper.CheckArgument(model, "model");
            SystemUser registerInfo = new SystemUser
            {
                Id=model.Id,
                UserName = model.UserName,
                Password = model.Password,
                ContactNumber = model.ContactNumber,
                NickName = model.NickName,
                Email = model.Email,
                HeadImage= model.HeadImage,
                IsDeleted=model.IsDeleted,
                AddDate=model.AddDate,
                IsStartUse = model.IsStartUse
            };
            OperationResult result = base.UpdateSysUser(registerInfo);
            return result;
        }
    }
}