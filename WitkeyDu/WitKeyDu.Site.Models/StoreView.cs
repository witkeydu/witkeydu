using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WitKeyDu.Site.Models
{
    public class StoreView
    {
        /// <summary>
        /// 店铺名称
        /// </summary>
        [Required]
        [StringLength(100)]
        [Display(Name = "店铺名称")]
        public string StoreName { get; set; }
        /// <summary>
        /// 店铺图标
        /// </summary>
        [Required]
        [StringLength(4000)]
        [Display(Name = "店铺图标")]
        public string StoreLogo { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        [Required]
        [StringLength(4000)]
        [Display(Name = "联系方式")]
        public string StoreContract { get; set; }
        /// <summary>
        /// 店铺详情
        /// </summary>
        [Required]
        [StringLength(4000)]
        [Display(Name = "店铺介绍")]
        public string StoreContent { get; set; }
        /// <summary>
        /// 获取或设置 所属用户信息
        /// </summary>
        public int SystemUserID { get; set; }
        public int Id { get; set; }
        public int taskPlanId { get; set; }
    }
}
