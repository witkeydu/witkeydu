using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;

namespace WitKeyDu.Component.Tools.ThirdRegion
{
    /// <summary>
    /// 腾讯QQ开放平台OAuth客户端用户信息接口实现
    /// </summary>
    public class TencentQQUserImpl : IUserInterface
    {
        #region //字段
        private IOAuthClient oauth;
        #endregion

        #region //构造方法
        public TencentQQUserImpl(IOAuthClient oauth)
        {
            this.oauth = oauth;
        }
        #endregion

        #region //接口实现

        #region //接口方法实现
        /// <summary>
        /// 获取第三方OAuth平台用户信息
        /// response result:
        ///{
        ///"ret":0,
        ///"msg":"",
        ///"nickname":"Peter",
        ///"figureurl":"http://qzapp.qlogo.cn/qzapp/111111/942FEA70050EEAFBD4DCE2C1FC775E56/30",
        ///"figureurl_1":"http://qzapp.qlogo.cn/qzapp/111111/942FEA70050EEAFBD4DCE2C1FC775E56/50",
        ///"figureurl_2":"http://qzapp.qlogo.cn/qzapp/111111/942FEA70050EEAFBD4DCE2C1FC775E56/100",
        ///"figureurl_qq_1":"http://q.qlogo.cn/qqapp/100312990/DE1931D5330620DBD07FB4A5422917B6/40",
        ///"figureurl_qq_2":"http://q.qlogo.cn/qqapp/100312990/DE1931D5330620DBD07FB4A5422917B6/100",
        ///"gender":"男",
        ///"is_yellow_vip":"1",
        ///"vip":"1",
        ///"yellow_vip_level":"7",
        ///"level":"7",
        ///"is_yellow_year_vip":"1"
        ///}
        /// </summary>
        /// <returns></returns>
        public dynamic GetUserInfo()
        {
            String responseResult = this.oauth.Get(this.oauth.Option.Urls["UserInfo"]);
            dynamic jsonResult = DynamicHelper.FromJSON(responseResult);

            #region //格式化基础的用户信息
            this.oauth.Token.User.OAuthId = this.oauth.Token.OAuthId;
            this.oauth.Token.User.Nickname = jsonResult.nickname;

            IDictionary<String, String> sexMaps = new Dictionary<String, String>();
            sexMaps["女"] = "0"; //女
            sexMaps["男"] = "1"; //男
            this.oauth.Token.User.Sex = sexMaps[jsonResult.gender];
            this.oauth.Token.User.Description = jsonResult.description;
            #endregion

            //debug
            this.oauth.Token.TraceInfo = responseResult;

            return jsonResult;
        }

        public void EndSession()
        {
            String responseResult = this.oauth.Get(this.oauth.Option.Urls["EndSession"], new RequestOption() { Name = "access_token", Value = this.oauth.Token.AccessToken });

            //debug
            this.oauth.Token.TraceInfo = responseResult;
        }
        #endregion

        #endregion




    }
}
