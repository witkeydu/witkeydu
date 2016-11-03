using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitKeyDu.Component.Tools;
using WitKeyDu.Site.Models;
using WitKeyDu.Core;

namespace WitKeyDu.Site
{
    public interface IForumCommentSiteContract : IForumCommentService
    {
        /// <summary>
        /// 发布任务
        /// </summary>
        /// <param name="forumModel">发布帖子模型信息</param>
        /// <returns></returns>
        OperationResult ReleaseFroumComment(ForumCommentView model);
    }
}
