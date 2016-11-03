using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using WitKeyDu.Core.Data.Repositories.Tasks;
using WitKeyDu.Component.Tools;
using WitKeyDu.Core.Models.Tasks;

namespace WitKeyDu.Core.Impl
{
    /// <summary>
    /// 任务类型核心模块实现
    /// </summary>
    public  abstract class TaskTypeService
    {
        #region 属性
        #region 受保护的属性
        /// <summary>
        /// 获取或设置 任务类型信息数据访问对象
        /// </summary>
        [Import]
        protected ITaskTypeRepository TaskTypeRepository { get; set; }

        #endregion
        #region 共有属性
        /// <summary>
        /// 获取任务类型数据查询数据集
        /// </summary>
        public IQueryable<TaskType> TaskTypes
        {
            get { return TaskTypeRepository.Entities; }
        }
        #endregion
        #endregion
        public virtual OperationResult AddTaskType(TaskType TaskTypeInfo)
        {
            try
            {
                PublicHelper.CheckArgument(TaskTypeInfo, "TaskTypeInfo");
                TaskTypeRepository.Insert(TaskTypeInfo);
                return new OperationResult(OperationResultType.Success, "任务类型添加成功。", TaskTypeInfo);
            }
            catch (Exception ex)
            {
                return new OperationResult(OperationResultType.Error, ex.Message.ToString());
            }
        }
    }
}
