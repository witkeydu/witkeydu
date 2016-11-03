using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace WitKeyDu.Component.Tools.ThirdRegion
{
    /// <summary>
    /// 淘宝开放平台OAuth客户端接口实现
    /// </summary>
    public class TaoBaoClient:OAuthClient
    {
        #region //构造方法
        public TaoBaoClient(String clientId, String clientSecret, String callbackUrl):base(clientId, clientSecret, callbackUrl)
        {
            InitOAuthOption();
            InitOAuthInterface();
        }
        #endregion

        #region 私有方法
        private void InitOAuthOption()
        {
            this.Option.ApiUrlBase = "https://eco.taobao.com/router/rest/";
            if (String.IsNullOrEmpty(this.Option.AuthorizeUrl)) this.Option.AuthorizeUrl = "https://oauth.taobao.com/authorize";
            if (String.IsNullOrEmpty(this.Option.AccessTokenUrl)) this.Option.AccessTokenUrl = "https://oauth.taobao.com/token";
            this.Option.Urls["UserInfo"] = "https://eco.taobao.com/router/rest";
            this.Option.Urls["EndSession"] = "https://eco.taobao.com/router/rest";
        }

        private void InitOAuthInterface()
        {
            if (User == null) User = new TaoBaoUserImpl(this); 
        }
        #endregion

        #region 重写父类方法
        /// <summary>
        /// 解析访问令牌
        /// response result:
        /// {
        ///   "w2_expires_in": 1800,
        ///   "taobao_user_id": "1826171524",
        ///   "taobao_user_nick": "sanxia_330",
        ///   "w1_expires_in": 86400,
        ///   "re_expires_in": 86400,
        ///   "r2_expires_in": 86400,
        ///   "expires_in": 86400,
        ///   "token_type": "Bearer",
        ///   "refresh_token": "6200522427d9e19e6029540fcc151dfh16c46b3de3a463b1826171524",
        ///   "access_token": "6201522a5dca536dfff594c7fa04bdfh3c4616011106a061826171524",
        ///   "r1_expires_in": 86400
        /// }
        /// </summary>
        /// <param name="responseResult"></param>
        protected override void ParseAccessToken(String responseResult)
        {
            //解析新浪微博授权令牌数据
            dynamic resultData = DynamicHelper.FromJSON(responseResult);
            this.Token.AccessToken = resultData.access_token;
            this.Token.ExpiresIn = (int)resultData.expires_in;
            this.Token.RefreshToken = resultData.refresh_token;
            this.Token.ReExpiresIn = (int)resultData.re_expires_in;
            this.Token.OAuthId = resultData.taobao_user_id;

            this.Token.User.OAuthId = resultData.taobao_user_id;
            this.Token.User.Nickname = resultData.taobao_user_nick;

            //debug
            this.Token.TraceInfo = responseResult;
        }

        /// <summary>
        /// Http请求Url数据处理
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="isMultipart"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        protected override RequestOption[] RequestGetExecuting(params RequestOption[] options)
        {
            List<RequestOption> requestOptions = new List<RequestOption>();
            requestOptions.AddRange(options);

            foreach (var option in options)
            {
                if (option.Name.ToLower() == "method")
                {
                    requestOptions.Add(new RequestOption() { Name = "format", Value = "json" });
                    requestOptions.Add(new RequestOption() { Name = "v", Value = "2.0" });
                    break;
                }
            }
            if (!string.IsNullOrEmpty(this.Token.AccessToken)) requestOptions.Add(new RequestOption("access_token", this.Token.AccessToken));
            return requestOptions.ToArray();
        }

        /// <summary>
        /// Http请求头部处理
        /// </summary>
        /// <param name="httpRequest"></param>
        protected override void RequestHeaderExecuting(HttpWebClient httpRequest)
        {
            if (!string.IsNullOrEmpty(this.Token.AccessToken)) httpRequest.Headers["Authorization"] = string.Format("OAuth2 {0}", this.Token.AccessToken);
        }
        #endregion
    }

}
