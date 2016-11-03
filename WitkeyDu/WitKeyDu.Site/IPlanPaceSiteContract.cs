using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitKeyDu.Component.Tools;
using WitKeyDu.Site.Models;
using WitKeyDu.Core;

namespace WitKeyDu.Site
{
    /// <summary>
    /// 计划进度站点业务契约
    /// </summary>
    public interface IPlanPaceSiteContract:IPlanPaceService
    {
        /// <summary>
        /// 添加进度计划
        /// </summary>
        /// <param name="taskModel">计划进度模型信息</param>
        /// <returns></returns>
        OperationResult ReleasePlanPace(PlanPaceView model);
    }
}
