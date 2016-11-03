using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WitKeyDu.Site.Models
{
    public class TaskPlanView
    {
        /// <summary>
        /// 计划内容
        /// </summary>
        [Required]
        [StringLength(1000)]
        [Display(Name = "解决方案")]
        public string PlanContent { get; set; }

        /// <summary>
        /// 报价价格
        /// </summary>
        [Required]
        [Display(Name = "项目预算")]
        public Decimal PlanPrice { get; set; }
        /// <summary>
        /// 启动时间
        /// </summary>
        [Display(Name = "开始时间")]
        public DateTime PlanStartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        [Display(Name = "结束时间")]
        public DateTime PlanEndTime { get; set; }
        /// <summary>
        /// 计划进度
        /// </summary>
        [Display(Name = "进度安排")]
        public string CompleteContent { get; set; }
        /// <summary>
        /// 提交文件
        /// </summary>
        [Display(Name = "备注信息")]
        public string Remark { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        [Display(Name = "文件路径")]
        public string FileSrc { get; set; }
        /// <summary>
        /// 任务编号
        /// </summary>
        public int TaskID { get; set; }
        /// <summary>
        /// 任务计划
        /// </summary>
        public string PlanCode { get; set; }
        public int ID { get; set; }
        public int SystemUserID { get; set; }
    }
}
