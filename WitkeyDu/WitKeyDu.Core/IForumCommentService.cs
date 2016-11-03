using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitKeyDu.Core.Models.Forums;
using WitKeyDu.Component.Tools;

namespace WitKeyDu.Core
{
    /// <summary>
    /// 评论模块核心业务契约
    /// </summary>
    public interface IForumCommentService
    {
        #region 属性
        /// <summary>
        /// 获取评论信息查询数据集
        /// </summary>
        IQueryable<ForumComment> ForumComments { get; }
        #endregion
        #region 公有方法
        /// <summary>
        ///  发表评论
        /// </summary>
        /// <param name="TaskInfo">评论信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult ReleaseForumComment(ForumComment ForumCommentInfo);
        #endregion
    }
}
