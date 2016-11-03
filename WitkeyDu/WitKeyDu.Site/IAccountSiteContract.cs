using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitKeyDu.Component.Tools;
using WitKeyDu.Core;
using WitKeyDu.Site.Models;

namespace WitKeyDu.Site
{
    /// <summary>
    ///     账户模块站点业务契约
    /// </summary>
    public interface IAccountSiteContract : IAccountService
    {
        /// <summary>
        ///     用户登录
        /// </summary>
        /// <param name="model">登录模型信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult Login(LoginModel model,string CheckCode);

        /// <summary>
        ///     用户退出
        /// </summary>
        void Logout();

        /// <summary>
        ///     用户注册
        /// </summary>
        /// <param name="model">注册模型信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult Register(RegisterModel model);
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="model">修改模型信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult Update(MemberView model);
    }
}