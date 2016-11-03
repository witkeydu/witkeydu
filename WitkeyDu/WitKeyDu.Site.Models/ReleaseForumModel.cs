using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WitKeyDu.Site.Models
{
    public class ReleaseForumModel
    {
        /// <summary>
        /// 帖子名称
        /// </summary>
        [Required]
        [Display(Name = "帖子名称")]
        public string ForumName { get; set; }

        /// <summary>
        /// 帖子内容
        /// </summary>
        [Required]
        [Display(Name = "帖子内容")]
        public string ForumContent { get; set; }

        /// <summary>
        /// 帖子图标
        /// </summary>
        [Display(Name = "帖子图标")]
        public string ForumLogo { get; set; }
        
        /// <summary>
        /// 链接地址
        /// </summary>
        [Display(Name = "返回地址")]
        public string ReturnUrl { get; set; }
        /// <summary>
        /// 帖子图标
        /// </summary>
        [Display(Name = "帖子类型")]
        public int ForumTypeID { get; set; }
    }
}
