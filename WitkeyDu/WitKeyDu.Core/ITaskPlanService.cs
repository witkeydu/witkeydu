using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitKeyDu.Core.Models.Tasks;
using WitKeyDu.Component.Tools;

namespace WitKeyDu.Core
{
    /// <summary>
    /// 项目计划业务核心契约
    /// </summary>
    public interface ITaskPlanService
    {
        #region 属性
        /// <summary>
        /// 获取任务计划
        /// </summary>
        IQueryable<TaskPlan> TaskPlans { get; }
        #endregion
        #region 公有方法
        /// <summary>
        ///  添加任务计划
        /// </summary>
        /// <param name="TaskInfo">任务计划信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult ReleaseTaskPlan(TaskPlan TaskPlanInfo);
        #endregion

    }
}
