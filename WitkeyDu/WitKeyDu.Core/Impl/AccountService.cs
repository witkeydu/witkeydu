using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Data;
using WitKeyDu.Component.Tools;
using WitKeyDu.Core.Data.Repositories.Account;
using WitKeyDu.Core.Data.Repositories.Security;
using WitKeyDu.Core.Models.Account;
using WitKeyDu.Core.Models.Security;
using WitKeyDu.Core.Data.Repositories.Account.Impl;
using System;

namespace WitKeyDu.Core.Impl
{
    /// <summary>
    ///     账户模块核心业务实现
    /// </summary>
    public abstract class AccountService : CoreServiceBase, IAccountService
    {
        #region 属性
        #region 受保护的属性
        /// <summary>
        /// 获取或设置 用户信息数据访问对象
        /// </summary>
        [Import]
        protected ISystemUserRepository SysUserRepository { get; set; }

        /// <summary>
        /// 获取或设置 登录记录信息数据访问对象
        /// </summary>
        [Import]
        protected ILoginLogRepository LoginLogRepository { get; set; }

        /// <summary>
        /// 获取或设置 角色信息业务访问对象
        /// </summary>
        [Import]
        protected IRoleRepository RoleRepository { get; set; }
        #endregion
        #region 公共属性
        /// <summary>
        /// 获取 用户信息查询数据集
        /// </summary>
        public IQueryable<SystemUser> SysUsers
        {
            get { return SysUserRepository.Entities; }
        }

        /// <summary>
        /// 获取 登录记录信息查询数据集
        /// </summary>
        public IQueryable<LoginLog> LoginLogs
        {
            get { return LoginLogRepository.Entities; }
        }

        /// <summary>
        /// 获取 角色信息查询数据集
        /// </summary>
        public IQueryable<Role> Roles
        {
            get { return RoleRepository.Entities; }
        }

        #endregion

        #endregion

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginInfo">登录信息</param>
        /// <returns>业务操作结果</returns>
        public virtual OperationResult Login(LoginInfo loginInfo,string CheckCode,ref SystemUser user)
        {
            try
            {
                PublicHelper.CheckArgument(loginInfo, "loginInfo");
                if (loginInfo.CheckCode.ToUpper() != CheckCode.ToUpper())
                {
                    return new OperationResult(OperationResultType.QueryNull, "验证码输入有误。");
                }
                user = SysUserRepository.Entities.SingleOrDefault(m => m.UserName == loginInfo.Account || m.Email == loginInfo.Account);
                if (user == null)
                {
                    return new OperationResult(OperationResultType.QueryNull, "指定账号的用户不存在。");
                }
                if (user.Password != loginInfo.Password)
                {
                    return new OperationResult(OperationResultType.Warning, "登录密码不正确。");
                }
                LoginLog loginLog = new LoginLog { IpAddress = loginInfo.IpAddress, SystemUserID = user.Id };
                LoginLogRepository.Insert(loginLog);
                return new OperationResult(OperationResultType.Success, "登录成功。", user);
            }
            catch (Exception ex)
            {
                return new OperationResult(OperationResultType.QueryNull, ex.Message.ToString()); ;
            }
        }


        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="registerInfo">注册信息</param>
        /// <returns>业务操作结果</returns>
        public virtual OperationResult Register(SystemUser registerInfo)
        {
            PublicHelper.CheckArgument(registerInfo, "registerInfo");
            SystemUser user = SysUserRepository.Entities.SingleOrDefault(m => m.UserName == registerInfo.UserName || m.Email == registerInfo.UserName||m.ContactNumber==registerInfo.ContactNumber);
            if (user == null)
            {
                SysUserRepository.Insert(registerInfo);
                return new OperationResult(OperationResultType.Success, "注册成功。", user);
            }
            return new OperationResult(OperationResultType.QueryNull, "指定账号的用户已存在。");
        }


        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="updateInfo">信息</param>
        /// <returns>业务操作结果</returns>
        public virtual OperationResult UpdateSysUser(SystemUser updateInfo)
        {
            PublicHelper.CheckArgument(updateInfo, "updateInfo");
            SysUserRepository.Update(updateInfo);
            return new OperationResult(OperationResultType.Success, "修改成功。");
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="deleteInfo">用户信息</param>
        /// <returns>业务操作结果</returns>
        public virtual OperationResult DeleteSysUser(SystemUser deleteInfo)
        {
            PublicHelper.CheckArgument(deleteInfo, "updateInfo");
            SystemUser user = SysUserRepository.Entities.SingleOrDefault(m => m.UserName == deleteInfo.UserName || m.Email == deleteInfo.UserName || m.ContactNumber == deleteInfo.ContactNumber);
            if (user != null)
            {
                SysUserRepository.Delete(deleteInfo);
                return new OperationResult(OperationResultType.Success, "用户删除成功。", user);
            }
            return new OperationResult(OperationResultType.QueryNull, "指定用户不存在。");
        }
    }
}