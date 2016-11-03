using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using WitKeyDu.Component.Tools;
using WitKeyDu.Core.Models.Security;
using WitKeyDu.Core.Models.Systems;
using WitKeyDu.Core.Models.Tasks;
using WitKeyDu.Core.Models.Forums;


namespace WitKeyDu.Core.Models.Account
{
    /// <summary>
    ///     实体类——用户信息
    /// </summary>
    [Description("用户信息")]
    public class SystemUser : EntityBase<int>
    {
        public SystemUser()
        {
            Roles = new List<Role>();
            LoginLogs = new List<LoginLog>();
            HeadImage = "../../Images/man.GIF";
        }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(32)]
        public string Password { get; set; }

        [StringLength(50)]
        public string NickName { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(20)]
        public string ContactNumber { get; set; }

        [StringLength(200)]
        public string HeadImage { get; set; }

        /// <summary>1
        /// 获取或设置 用户拥有的角色信息集合
        /// </summary>
        public virtual ICollection<Role> Roles { get; set; }

        /// <summary>
        /// 获取或设置 用户登录记录集合
        /// </summary>
        public virtual ICollection<LoginLog> LoginLogs { get; set; }
    }
}