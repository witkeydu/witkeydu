using System.Linq;

using WitKeyDu.Component.Tools;
using WitKeyDu.Core.Data.Repositories;
using WitKeyDu.Core.Models;
using WitKeyDu.Core.Models.Account;
using WitKeyDu.Core.Models.Security;


namespace WitKeyDu.Core
{
    /// <summary>
    ///     账户模块核心业务契约
    /// </summary>
    public interface IAccountService
    {
        #region 属性

        /// <summary>
        /// 获取 用户信息查询数据集
        /// </summary>
        IQueryable<SystemUser> SysUsers { get; }

        /// <summary>
        /// 获取 登录记录信息查询数据集
        /// </summary>
        IQueryable<LoginLog> LoginLogs { get; }

        /// <summary>
        /// 获取 角色信息查询数据集
        /// </summary>
        IQueryable<Role> Roles { get; }

        #endregion

        #region 公共方法

        /// <summary>
        ///     用户登录
        /// </summary>
        /// <param name="loginInfo">登录信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult Login(LoginInfo loginInfo,string checkCode,ref SystemUser user);

        /// <summary>
        ///     用户注册
        /// </summary>
        /// <param name="RegisterInfo">注册信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult Register(SystemUser registerInfo);
        
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="updateInfo">信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult UpdateSysUser(SystemUser updateInfo);
        
        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="deleteInfo">用户信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult DeleteSysUser(SystemUser deleteInfo);
        #endregion
    }
}