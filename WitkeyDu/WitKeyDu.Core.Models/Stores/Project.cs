using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitKeyDu.Component.Tools;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WitKeyDu.Core.Models.Account;
using WitKeyDu.Core.Models.Tasks;

namespace WitKeyDu.Core.Models.Stores
{
    /// <summary>
    ///     实体类——店铺项目信息
    /// </summary>
    [Description("店铺信息")]
    public class Project : EntityBase<int>
    {
        public Project()
        { 
        
        }

        /// <summary>
        /// 项目名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string ProjectName { get; set; }
        /// <summary>
        /// 项目图标
        /// </summary>
        [Required]
        [StringLength(100)]
        public string ProjectLogo { get; set; }
        [StringLength(100)]
        public string ShowUrl { get; set; }
        /// <summary>
        /// 销售价格
        /// </summary>
        [Required]
        public int ProjectPrice { get; set; }
        /// <summary>
        /// 项目介绍
        /// </summary>
        [Required]
        [StringLength(4000)]
        public string ProjectIntroduction { get; set; }
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

    }
}
