using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WitKeyDu.Site.Models
{
    public class ProjectView
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        [Required]
        [Display(Name = "项目名称")]
        [StringLength(100)]
        public string ProjectName { get; set; }
        /// <summary>
        /// 项目图标
        /// </summary>
        [Required]
        [Display(Name = "项目图标")]
        [StringLength(100)]
        public string ProjectLogo { get; set; }
        /// <summary>
        /// 项目售价
        /// </summary>
        [Required]
        [Display(Name = "项目售价")]
        public int ProjectPrice { get; set; }
        /// <summary>
        /// 项目介绍
        /// </summary>
        [StringLength(100)]
        [Display(Name = "演示网址")]
        public string ShowUrl { get; set; }

        [Required]
        [Display(Name = "项目介绍")]
        [StringLength(4000)]
        public string ProjectIntroduction { get; set; }
        public int SystemUserID { get; set; }
        /// <summary>
        /// 任务类型
        /// </summary>
        [Display(Name = "项目类型")]
        [Required]
        public int TaskTypeID { get; set; }
        public int ID { get; set; }
        /// <summary>
        /// 任务类型
        /// </summary>
        [Display(Name = "项目类型")]
        [Required]
        public string TaskTypeName { get; set; }
    }
}
