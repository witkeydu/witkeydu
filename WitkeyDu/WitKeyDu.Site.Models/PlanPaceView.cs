using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitKeyDu.Site.Models
{
    public class PlanPaceView
    {
        /// <summary>
        /// 启动时间
        /// </summary>
        public DateTime PlanStartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime PlanEndTime { get; set; }
        /// <summary>
        /// 计划进度
        /// </summary>
        public string CompleteContent { get; set; }
        /// <summary>
        /// 计划报告
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FileSrc { get; set; }
        /// <summary>
        /// 任务计划
        /// </summary>
        public string PlanCode { get; set; }
        public int  PlanID { get; set; }
    }
}
