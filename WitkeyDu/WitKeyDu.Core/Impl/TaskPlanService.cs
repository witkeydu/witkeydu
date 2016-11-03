using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitKeyDu.Core.Data.Repositories.Account;
using System.ComponentModel.Composition;
using WitKeyDu.Core.Models.Tasks;
using WitKeyDu.Component.Tools;
using WitKeyDu.Core.Data.Repositories.Tasks;

namespace WitKeyDu.Core.Impl
{
    public abstract class TaskPlanService:CoreServiceBase,ITaskPlanService
    {
        #region 属性
        #region 受保护的属性
        /// <summary>
        /// 获取或设置 用户信息数据访问对象
        /// </summary>
        [Import]
        protected ISystemUserRepository SysUserRepository { get; set; }

        /// <summary>
        /// 获取或设置 论坛帖信息数据访问对象
        /// </summary>
        [Import]
        protected ITaskPlanRepository ReleaseTaskPlanRepository { get; set; }
        #endregion
        #region 共有属性
        /// <summary>
        /// 获取论坛帖查询数据集
        /// </summary>
        public IQueryable<TaskPlan> TaskPlans
        {
            get { return ReleaseTaskPlanRepository.Entities; }
        }
        #endregion
        #endregion
        public virtual OperationResult ReleaseTaskPlan(TaskPlan TaskPlanInfo)
        {
            try
            {
                PublicHelper.CheckArgument(TaskPlanInfo, "TaskPlanInfo");
                ReleaseTaskPlanRepository.Insert(TaskPlanInfo);
                return new OperationResult(OperationResultType.Success, "任务计划新增成功。", TaskPlanInfo);
            }
            catch (Exception ex)
            {
                return new OperationResult(OperationResultType.Error, ex.Message.ToString());
            }
        }

    }
}
