using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WitKeyDu.Site.Models
{
    public class FileView
    {
        /// <summary>
        /// 文件编码
        /// </summary>
        [Required]
        [Display(Name = "文件编号")]
        public string FileCode { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        [Required]
        [Display(Name = "文件标题")]
        public string FileName { get; set; }
        /// <summary>
        /// 文件介绍
        /// </summary>
        [Required]
        [Display(Name = "文件教程")]
        public string FileIntroduction { get; set; }
        /// <summary>
        /// 链接地址
        /// </summary>
        [Display(Name = "返回地址")]
        public string ReturnUrl { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        [Display(Name = "用户名称")]
        public string UserName { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        [Display(Name = "用户头像")]
        public string HeadImg  { get; set; }

        /// <summary>
        /// 获取或设置 所属用户信息
        /// </summary>
        public int UserKey { get; set; }
        public int FileTypeID { get; set; }
    }
}
