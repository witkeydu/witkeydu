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
    /// 腾讯QQ开放平台OAuth客户端接口实现
    /// </summary>
    public class TencentQQClient:OAuthClient
    {
        #region //构造方法
        public TencentQQClient(String clientId, String clientSecret, String callbackUrl):base(clientId, clientSecret, callbackUrl)
        {
            InitOAuthOption();
            InitOAuthInterface();
        }
        #endregion

        #region 私有方法
        private void InitOAuthOption()
        {
            this.Option.ApiUrlBase = "https://graph.qq.com/";
            if (String.IsNullOrEmpty(this.Option.AuthorizeUrl)) this.Option.AuthorizeUrl = "https://graph.qq.com/oauth2.0/authorize";
            if (String.IsNullOrEmpty(this.Option.AccessTokenUrl)) this.Option.AccessTokenUrl = "https://graph.qq.com/oauth2.0/token";
            this.Option.Urls["OpenId"] = "https://graph.qq.com/oauth2.0/me";
            this.Option.Urls["UserInfo"] = "https://graph.qq.com/user/get_user_info";

            if (String.IsNullOrEmpty(this.Option.State)) this.Option.State = "sanxia123";
        }

        private void InitOAuthInterface()
        {
            if (User == null) User = new TencentQQUserImpl(this);
        }
        #endregion

        #region 重写父类方法
        /// <summary>
        /// 解析第三方OAuth平台返回的Token
        /// response result for token:
        /// access_token=E8B5CD3FA2A62B8701FA1402293FCA6A&expires_in=7776000&refresh_token=6020A9D456BCBFA233ED2684597AF3D1
        /// 
        /// response result for openid:
        /// callback({"client_id":"101167791","openid":"123dwexxxxxxxxxxxxxx"});
        /// </summary>
        /// <param name="responseResult"></param>
        protected override void ParseAccessToken(String responseResult)
        {
            #region 解析QQ授权令牌数据
            Dictionary<string, string> resultMaps = new Dictionary<string, string>();
            foreach (var item in responseResult.Split('&'))
            {
                string[] split = item.Split('=');
                if (split.Length == 2)
                {
                    resultMaps.Add(split[0], split[1]);
                }
            }

            if (resultMaps.ContainsKey("access_token"))
            {
                this.Token.AccessToken = resultMaps["access_token"];
            }
            if (resultMaps.ContainsKey("refresh_token"))
            {
                this.Token.RefreshToken = resultMaps["refresh_token"];
            }
            if (resultMaps.ContainsKey("expires_in"))
            {
                this.Token.ExpiresIn = Convert.ToInt32(resultMaps["expires_in"]);
            }
            #endregion

            #region 解析腾讯QQ的OpenId
            String openidResponseResult = base.Get(this.Option.Urls["OpenId"]);
            var jsonResult = GetResponseJsonData(openidResponseResult);

            this.Token.OAuthId = jsonResult.openid;

            //debug
            this.Token.TraceInfo = responseResult + "\r\n" + openidResponseResult;

            #endregion
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
            #region 请求参数处理
            List<RequestOption> commonRequestOptions = new List<RequestOption>();
            commonRequestOptions.Add(new RequestOption("oauth_consumer_key", this.Option.ClientId));
            if (!string.IsNullOrEmpty(this.Token.AccessToken)) commonRequestOptions.Add(new RequestOption("access_token", this.Token.AccessToken));
            if (!String.IsNullOrEmpty(this.Token.OAuthId)) commonRequestOptions.Add(new RequestOption("openid", this.Token.OAuthId));
            commonRequestOptions.Add(new RequestOption("oauth_version", "2.a"));
            commonRequestOptions.Add(new RequestOption("scope", "all"));
            commonRequestOptions.Add(new RequestOption("format", "json"));

            List<RequestOption> allRequestOptions = new List<RequestOption>();
            allRequestOptions.AddRange(commonRequestOptions);
            allRequestOptions.AddRange(options);
            #endregion

            return allRequestOptions.ToArray();
        }

        protected override RequestOption[] RequestPostExecuting(params RequestOption[] customRequestOptions)
        {
            List<RequestOption> commonRequestOptions = new List<RequestOption>();
            commonRequestOptions.Add(new RequestOption("oauth_consumer_key", this.Option.ClientId));
            if (!string.IsNullOrEmpty(this.Token.AccessToken)) commonRequestOptions.Add(new RequestOption("access_token", this.Token.AccessToken));
            if (!String.IsNullOrEmpty(this.Token.OAuthId)) commonRequestOptions.Add(new RequestOption("openid", this.Token.OAuthId));
            commonRequestOptions.Add(new RequestOption("oauth_version", "2.a"));
            commonRequestOptions.Add(new RequestOption("scope", "all"));
            commonRequestOptions.Add(new RequestOption("format", "json"));

            List<RequestOption> allRequestOptions = new List<RequestOption>();
            allRequestOptions.AddRange(commonRequestOptions);
            allRequestOptions.AddRange(customRequestOptions);

            return allRequestOptions.ToArray();
        }
        #endregion

        #region 帮助方法
        /// <summary>
        /// 提取腾讯QQ第三方OAuth平台返回的json数据
        /// </summary>
        /// <param name="responseText"></param>
        /// <returns></returns>
        private dynamic GetResponseJsonData(String responseText)
        {
            Regex reg = new Regex("^callback[(](?<data>.*?)[);]+$", RegexOptions.IgnoreCase);
            Match match = reg.Match(responseText.Trim());
            String jsonText = match.Groups["data"].Value;
            return DynamicHelper.FromJSON(jsonText);
        }
        #endregion
    }

}
