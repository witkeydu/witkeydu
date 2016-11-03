using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitKeyDu.Core.Models.Tasks;
using WitKeyDu.Component.Tools;

namespace WitKeyDu.Core
{
    /// <summary>
    /// 任务类型模块核心业务契约
    /// </summary>
    public interface ITaskTypeService
    {
        #region 属性
        IQueryable<TaskType> TaskTypes { get; }
        #endregion

        #region 公有方法
        /// <summary>
        ///  发布任务
        /// </summary>
        /// <param name="TaskInfo">任务信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult AddTaskType(TaskType TaskTypeInfo);
        #endregion
    }
}
