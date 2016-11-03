using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitKeyDu.Site.Models;
using WitKeyDu.Component.Tools;
using WitKeyDu.Core.Impl;

namespace WitKeyDu.Site
{

    /// <summary>
    /// 项目模块站点业务契约
    /// </summary>
    public interface IProjectSiteContract:IProjectService
    {
        /// <summary>
        /// 发布新项目
        /// </summary>
        /// <param name="taskModel">发布项目模型信息</param>
        /// <returns></returns>
        OperationResult ReleaseProject(ProjectView model);
    }
}
