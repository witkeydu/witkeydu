using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitKeyDu.Component.Tools;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using WitKeyDu.Core.Models.Account;

namespace WitKeyDu.Core.Models.Tasks
{
    public class Task : EntityBase<int>
    {
        /// <summary>
        ///     实体类——任务信息
        /// </summary>
        [Description("任务信息")]
        public Task()
        {
            TaskState = "招标中";
        }
        /// <summary>
        /// 任务名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string TaskName { get; set; }
        /// <summary>
        /// 任务内容
        /// </summary>
        [Required]
        [StringLength(4000)]
        public string TaskContent { get; set; }
        /// <summary>
        /// 任务赏金
        /// </summary>
        [Required]
        public Decimal TaskReward { get; set; }
        /// <summary>
        /// 任务状态
        /// </summary>
        [Required]
        [StringLength(10)]
        public string TaskState { get; set; }
        /// <summary>
        /// 启动时间
        /// </summary>
        [Required]
        public DateTime TaskStartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        [Required]
        public DateTime TaskEndTime { get; set; }

        /// <summary>
        /// 任务天数
        /// </summary>
        public int DayNum { get; set; }
        /// <summary>
        /// 任务图标
        /// </summary>
        [StringLength(200)]
        public string TaskLogo { get; set; }

        /// <summary>
        /// 获取或设置 所属用户信息
        /// </summary>
        public virtual SystemUser User { get; set; }
        public int SystemUserID { get; set; }
        /// <summary>
        ///     获取或设置 任务类型信息集合
        /// </summary>
        public virtual TaskType TaskType { get; set; }
        public int TaskTypeID { get; set; }

        public int Employer { get; set; }
        public int TaskPlanID { get; set; }
        public DateTime EmployeTime { get; set; }
    }
}
