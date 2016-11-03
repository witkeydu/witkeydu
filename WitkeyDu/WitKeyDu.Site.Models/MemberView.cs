using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace WitKeyDu.Site.Models
{
    public class MemberView
    {
        public int Id { get; set; }
        /// <summary>
        /// 获取或设置 登录账号
        /// </summary>
        [Required]
        [Display(Name = "账号名称")]
        public string UserName { get; set; }

        [Display(Name = "用户昵称")]
        public string NickName { get; set; }

        [Required]
        [Display(Name = "邮件地址")]
        public string Email { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsStartUse { get; set; }

        public DateTime AddDate { get; set; }

        public int LoginLogCount { get; set; }

        [Display(Name = "用户头像")]
        public string HeadImage { get; set; }

        [Display(Name = "登录密码")]
        public string Password { get; set; }

        [Display(Name = "联系方式")]
        public string ContactNumber { get; set; }

        [Display(Name = "角色名称")]
        public IEnumerable<string> RoleNames { get; set; }
    }
}
