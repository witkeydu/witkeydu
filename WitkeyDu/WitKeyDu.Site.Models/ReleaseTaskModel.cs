using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace WitKeyDu.Site.Models
{
    public class ReleaseTaskModel
    {
        /// <summary>
        /// 任务名称
        /// </summary>
        [Required]
        [Display(Name="任务名称")]
        public string TaskName { get; set; }
        /// <summary>
        /// 任务内容
        /// </summary>
        [Required]
        [Display(Name = "任务内容")]
        public string TaskContent { get; set; }
        /// <summary>
        /// 任务赏金
        /// </summary>
        [Required]
        [Display(Name = "项目预算")]
        public Decimal TaskReward { get; set; }
        /// <summary>
        /// 启动时间
        /// </summary>
        [Required]
        [Display(Name = "启动时间")]
        public DateTime TaskStartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        [Required]
        [Display(Name = "结束时间")] 
        public DateTime TaskEndTime { get; set; }
        /// <summary>
        /// 任务图标
        /// </summary>
        [Display(Name = "任务图标")]
        public string TaskLogo { get; set; }
        /// <summary>
        /// 链接地址
        /// </summary>
        [Display(Name = "返回地址")]
        public string ReturnUrl { get; set; }
        /// <summary>
        /// 任务类型
        /// </summary>
        [Display(Name = "任务类型")]
        [Required]
        public int TaskTypeID { get; set; }
        /// <summary>
        /// 任务类型
        /// </summary>
        [Display(Name = "任务类型")]
        [Required]
        public string TaskTypeName { get; set; }
    }
}
