using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitKeyDu.Site.Models
{
    public class TaskListView
    {
        /// <summary>
        /// 任务编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }
        /// <summary>
        /// 任务天数
        /// </summary>
        public int DayNum { get; set; }
        /// <summary>
        /// 任务状态
        /// </summary>

        public string TaskState { get; set; }
        /// <summary>
        /// 任务赏金
        /// </summary>
        public Decimal TaskReward { get; set; }
        /// <summary>
        /// 启动时间
        /// </summary>
        public DateTime TaskStartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime TaskEndTime { get; set; }
        /// <summary>
        /// 任务图标
        /// </summary>
        public string TaskLogo { get; set; }
    }
}
