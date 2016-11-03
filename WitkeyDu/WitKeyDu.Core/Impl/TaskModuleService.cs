using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitKeyDu.Core.Data.Repositories.Account;
using System.ComponentModel.Composition;
using WitKeyDu.Core.Data.Repositories.Tasks;
using WitKeyDu.Core.Models.Tasks;
using WitKeyDu.Core.Models.Account;
using WitKeyDu.Component.Tools;

namespace WitKeyDu.Core.Impl
{
    /// <summary>
    /// 任务模块核心实现
    /// </summary>
    public abstract class TaskModuleService:CoreServiceBase,ITaskModuleService
    {
        #region 属性
        #region 受保护的属性
        /// <summary>
        /// 获取或设置 用户信息数据访问对象
        /// </summary>
        [Import]
        protected ISystemUserRepository SysUserRepository { get; set; }

        /// <summary>
        /// 获取或设置 任务信息数据访问对象
        /// </summary>
        [Import]
        protected ITaskRepository ReleaseTaskRepository { get; set; }
        /// <summary>
        /// 获取或设置 任务类型信息数据访问对象
        /// </summary>
        [Import]
        protected ITaskTypeRepository TaskTypeRepository { get; set; }
        #endregion
        #region 共有属性
        /// <summary>
        /// 获取任务查询数据集
        /// </summary>
        public IQueryable<Task> Tasks
        {
            get { return ReleaseTaskRepository.Entities; }
        }

        /// <summary>
        /// 获取任务数据查询数据集
        /// </summary>
        public IQueryable<SystemUser> SysUsers
        {
            get { return SysUserRepository.Entities; }
        }

        /// <summary>
        /// 获取任务类型数据查询数据集
        /// </summary>
        public IQueryable<TaskType> TaskTypes
        {
            get { return TaskTypeRepository.Entities; }
        }
        #endregion
        #endregion
        public virtual OperationResult ReleaseTask(Task TaskInfo)
        {
            try
            {
                PublicHelper.CheckArgument(TaskInfo, "TaskInfo");
                ReleaseTaskRepository.Insert(TaskInfo);
                return new OperationResult(OperationResultType.Success, "任务发布成功。", TaskInfo);
            }
            catch (Exception ex)
            {
                return new OperationResult(OperationResultType.Error, ex.Message.ToString());
            }
        }


        /// <summary>
        /// 修改任务信息
        /// </summary>
        /// <param name="updateInfo">信息</param>
        /// <returns>业务操作结果</returns>
        public virtual OperationResult UpdateTask(Task TaskInfo)
        {
            PublicHelper.CheckArgument(TaskInfo, "updateInfo");
            ReleaseTaskRepository.Update(TaskInfo);
            return new OperationResult(OperationResultType.Success, "修改成功。");
        }
    }
}
