using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WitKeyDu.Site.Models
{
    public class RegisterModel
    {

        /// <summary>
        /// 获取或设置 登录账号
        /// </summary>
        [Required]
        [Display(Name = "账号名称")]
        public string Account { get; set; }

        /// <summary>
        /// 获取或设置 登录账号
        /// </summary>
        [Required]
        [Display(Name = "用户昵称")]
        public string NickName { get; set; }

        /// <summary>
        /// 获取或设置 登录密码
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "登录密码")]
        public string Password { get; set; }

        /// <summary>
        /// 获取或设置 重复密码
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "重复密码")]
        [System.Web.Mvc.Compare("Password", ErrorMessage = "密码和确认密码不匹配。")]
        public string RepeatPassword { get; set; }

        /// <summary>
        /// 获取或设置 重复密码
        /// </summary>
        [Required]
        [Display(Name = "联系方式")]
        public string ContectNumber { get; set; }

        /// <summary>
        /// 获取或设置 邮件地址
        /// </summary>
        [Required]
        [Display(Name = "邮件地址")]
        public string Email { get; set; }

        /// <summary>
        /// 获取或设置 登录成功后返回地址
        /// </summary>
        public string ReturnUrl { get; set; }
    }
}
