using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitKeyDu.Core.Models.Tasks;
using WitKeyDu.Core.Models.Account;
using WitKeyDu.Component.Tools;

namespace WitKeyDu.Core
{
    /// <summary>
    /// 任务模块核心业务契约
    /// </summary>
    public interface ITaskModuleService
    {
        #region 属性
        /// <summary>
        /// 获取 发布任务查询数据集
        /// </summary>
        IQueryable<Task> Tasks { get; }
        /// <summary>
        /// 获取 任务类型查询数据集
        /// </summary>
        IQueryable<TaskType> TaskTypes { get; }
        /// <summary>
        /// 获取 用户信息查询数据集
        /// </summary>
        IQueryable<SystemUser> SysUsers { get; }
        #endregion

        #region 公有方法
        /// <summary>
        ///  发布任务
        /// </summary>
        /// <param name="TaskInfo">任务信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult ReleaseTask(Task TaskInfo);
        OperationResult UpdateTask(Task TaskInfo);
        #endregion
    }
}
