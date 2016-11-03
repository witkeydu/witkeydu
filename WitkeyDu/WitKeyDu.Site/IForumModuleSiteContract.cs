using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitKeyDu.Core;
using WitKeyDu.Site.Models;
using WitKeyDu.Component.Tools;

namespace WitKeyDu.Site.Impl
{
    /// <summary>
    /// 任务模块站点业务契约
    /// </summary>
    public interface IForumModuleSiteContract : IForumModuleService
    {
        /// <summary>
        /// 发布任务
        /// </summary>
        /// <param name="forumModel">发布帖子模型信息</param>
        /// <returns></returns>
        OperationResult ReleaseFroum(ReleaseForumModel model);
    }
}
