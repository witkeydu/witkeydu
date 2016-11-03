using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitKeyDu.Site.Models;
using WitKeyDu.Component.Tools;
using WitKeyDu.Core;

namespace WitKeyDu.Site
{
    /// <summary>
    /// 任务计划模块站点业务契约
    /// </summary>
    public interface ITaskPlanSiteContract : ITaskPlanService
    {
        /// <summary>
        /// 发布任务
        /// </summary>
        /// <param name="taskModel">发布任务模型信息</param>
        /// <returns></returns>
        OperationResult ReleaseTaskPlan(TaskPlanView model);
    }
}
