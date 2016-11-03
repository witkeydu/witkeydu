using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitKeyDu.Component.Tools;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WitKeyDu.Core.Models.Account;

namespace WitKeyDu.Core.Models.Tasks
{
    public class TaskPlan : EntityBase<int>
    {
        /// <summary>
        /// 实体类——任务计划
        /// </summary>

        [Description("任务计划")]
        public TaskPlan()
        {
            PlanCode = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 计划编号
        /// </summary>
        [Required]
        [StringLength(50)]
        public string PlanCode{ get; set; }
        /// <summary>
        /// 计划内容
        /// </summary>
        [Required]
        [StringLength(4000)]
        public string PlanContent { get; set; }

        /// <summary>
        /// 报价价格
        /// </summary>
        [Required]
        public Decimal PlanPrice { get; set; }

        /// <summary>
        /// 获取或设置 所属用户信息
        /// </summary>
        public virtual SystemUser User { get; set; }
        public int SystemUserID { get; set; }

        /// <summary>
        ///     获取或设置 任务类型信息集合
        /// </summary>
        public virtual Task Task { get; set; }
        public int TaskID { get; set; }
    }
}
