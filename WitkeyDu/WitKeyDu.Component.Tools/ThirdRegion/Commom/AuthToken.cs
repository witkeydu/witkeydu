using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitKeyDu.Component.Tools.ThirdRegion
{
    /// <summary>
    /// OAuth Token类
    /// Author: 美丽的地球
    /// Email: sanxia330@qq.com
    /// QQ: 1851690435
    /// </summary>
    public class AuthToken
    {
        #region //属性
        public String AccessToken { get; set; }
        public String RefreshToken { get; set; }
        public String OAuthId { get; set; }
        public int ExpiresIn { get; set; }
        public int ReExpiresIn { get; set; }

        public UserProfile User { get; set; }
        public String TraceInfo { get; set; }
        #endregion

        #region //构造方法
        public AuthToken()
        {
            User = new UserProfile();
        }
        #endregion
    }
}
