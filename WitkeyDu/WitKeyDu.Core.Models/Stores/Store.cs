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
    ///     实体类——店铺信息
    /// </summary>
    [Description("店铺信息")]
    public class Store : EntityBase<int>
    {
        public Store()
        { 
        
        }
        /// <summary>
        /// 店铺名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string StoreName { get; set; }
        /// <summary>
        /// 店铺图标
        /// </summary>
        [Required]
        [StringLength(4000)]
        public string StoreLogo { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        [Required]
        [StringLength(4000)]
        public string StoreContract { get; set; }
        /// <summary>
        /// 店铺详情
        /// </summary>
        [Required]
        [StringLength(4000)]
        public string StoreContent { get; set; }
        /// <summary>
        /// 获取或设置 所属用户信息
        /// </summary>
        public virtual SystemUser User { get; set; }
        public int SystemUserID { get; set; }
        /// <summary>
        ///     获取或设置 任务类型信息集合
        ///// </summary>
        //public virtual TaskType TaskType { get; set; }
        //public int TaskTypeID { get; set; }
    }
}
