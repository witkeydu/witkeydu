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
    /// <summary>
    /// 实体类——任务类型记录信息
    /// </summary>
    [Description("任务类型记录信息")]
    public class TaskType : EntityBase<int>
    {
        public TaskType()
        {
        }
        /// <summary>
        /// 任务类型名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string TaskTypeName { get; set; }

        /// <summary>
        /// 任务类型名称
        /// </summary>
        [Required]
        [StringLength(200)]
        public string TaskTypeLogo { get; set; }

        /// <summary>
        /// 父级任务类型
        /// </summary>
        public int TaskParentTypeID { get; set; }

        /// <summary>
        /// 获取或设置 所属用户信息
        /// </summary>
        public virtual SystemUser User { get; set; }
        public int SystemUserID { get; set; }
    }
}
