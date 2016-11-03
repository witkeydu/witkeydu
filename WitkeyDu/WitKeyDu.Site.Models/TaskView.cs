using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WitKeyDu.Site.Models
{
    public class TaskView
    {
        /// <summary>
        /// 任务名称
        /// </summary>
        [Display(Name = "任务名称")]
        public string TaskName { get; set; }
        /// <summary>
        /// 任务内容
        /// </summary>
        [Display(Name = "任务内容")]
        public string TaskContent { get; set; }
        /// <summary>
        /// 任务赏金
        /// </summary>
        [Display(Name = "任务赏金")]
        public Decimal TaskReward { get; set; }
        /// <summary>
        /// 启动时间
        /// </summary>
        [Display(Name = "启动时间")]
        public DateTime TaskStartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        [Display(Name = "结束时间")]
        public DateTime TaskEndTime { get; set; }
        /// <summary>
        /// 任务图标
        /// </summary>
        [Display(Name = "任务图标")]
        public string TaskLogo { get; set; }

        /// <summary>
        /// 任务发布者
        /// </summary>
        [Display(Name = "发布者")]
        public string TaskReleaser { get; set; }
        public int Employer { get; set; }
        public int TaskPlanID { get; set; }
        public DateTime EmployeTime { get; set; }
    }
}
