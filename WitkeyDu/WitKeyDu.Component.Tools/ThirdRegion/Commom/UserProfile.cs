using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitKeyDu.Component.Tools.ThirdRegion
{
    /// <summary>
    /// OAuth 用户信息类
    /// Author: 美丽的地球
    /// Email: sanxia330@qq.com
    /// QQ: 1851690435
    /// </summary>
    public class UserProfile
    {
        #region //属性
        public String OAuthId { get; set; }
        public String Nickname { get; set; }
        public String Email { get; set; }
        public String Sex { get; set; }
        public String AvatarUrl { get; set; }
        public String Description { get; set; }

        public String Info { get; set; }
        #endregion

        #region //构造方法
        public UserProfile()
        {
            Sex = "2"; //0：女 | 1：男 | 2：保密
        }
        #endregion
    }
}
