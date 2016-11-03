using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using WitKeyDu.Component.Tools;
using WitKeyDu.Core.Models.Tasks;

namespace WitKeyDu.Core.Models.Tasks
{
    public class PlanPace : EntityBase<int>
    {
        public PlanPace()
        { 
        
        }
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
    }
}
