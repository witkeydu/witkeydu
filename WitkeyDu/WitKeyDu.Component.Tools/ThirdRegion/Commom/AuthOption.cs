using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitKeyDu.Component.Tools.ThirdRegion
{
    /// <summary>
    /// OAuth通用参数类
    /// Author: 美丽的地球
    /// Email: sanxia330@qq.com
    /// QQ: 1851690435
    /// </summary>
    public class AuthOption
    {
        #region //属性
        public String ClientId { get; set; }
        public String ClientSecret { get; set; }

        public String ApiUrlBase { get; set; }
        public String AuthorizeUrl { get; set; }
        public String AccessTokenUrl { get; set; }
        public String CallbackUrl { get; set; }

        public String Display { get; set; }
        public String State { get; set; }
        public String Scope { get; set; }

        public IDictionary<String, String> Urls { get; set; }
        #endregion

        #region //构造方法
        public AuthOption()
		{
            this.Urls = new Dictionary<String, String>();
            this.Display = "default ";
        }
        #endregion

    }

}
