using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitKeyDu.Core.Models.Forums;
using WitKeyDu.Core.Models.Account;
using WitKeyDu.Component.Tools;

namespace WitKeyDu.Core
{
    /// <summary>
    /// 论坛模块核心业务契约
    /// </summary>
    public interface IForumModuleService
    {
        #region 属性
        /// <summary>
        /// 获取 发布帖子查询数据集
        /// </summary>
        IQueryable<Forum> Forums { get; }
        /// <summary>
        /// 获取 帖子类型查询数据集
        /// </summary>
        IQueryable<ForumType> ForumTypes { get; }
        /// <summary>
        /// 获取 用户信息查询数据集
        /// </summary>
        IQueryable<SystemUser> SysUsers { get; }
        /// <summary>
        /// 获取评论信息查询数据集
        /// </summary>
        IQueryable<ForumComment> ForumComments { get; }
        #endregion
        #region 公有方法
        /// <summary>
        ///  发布任务
        /// </summary>
        /// <param name="TaskInfo">任务信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult ReleaseForum(Forum ForumInfo);
        #endregion
    }
}
