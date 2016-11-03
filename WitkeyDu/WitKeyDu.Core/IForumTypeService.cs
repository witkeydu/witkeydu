using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitKeyDu.Core.Models.Forums;
using WitKeyDu.Component.Tools;

namespace WitKeyDu.Core
{
    /// <summary>
    /// 帖子类型核心业务实现
    /// </summary>
    public interface IForumTypeService
    {
        #region 属性
        IQueryable<ForumType> ForumTypes { get; }
        #endregion

        #region 公有方法
        /// <summary>
        ///  发布任务
        /// </summary>
        /// <param name="TaskInfo">任务信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult AddForumType(ForumType ForumTypeInfo);
        #endregion
    }
}
