using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace WitKeyDu.Component.Tools.ThirdRegion
{
    /// <summary>
    /// 新浪微博开放平台OAuth客户端接口实现
    /// </summary>
    public class SinaWeiBoClient:OAuthClient
    {
        #region //构造方法
        public SinaWeiBoClient(String clientId, String clientSecret, String callbackUrl):base(clientId, clientSecret, callbackUrl)
        {
            InitOAuthOption();
            InitOAuthInterface();
        }
        #endregion

        #region //私有方法
        private void InitOAuthOption()
        {
            this.Option.ApiUrlBase = "https://api.weibo.com/2/";
            if (String.IsNullOrEmpty(this.Option.AuthorizeUrl)) this.Option.AuthorizeUrl = "https://api.weibo.com/oauth2/authorize";
            if (String.IsNullOrEmpty(this.Option.AccessTokenUrl)) this.Option.AccessTokenUrl = "https://api.weibo.com/oauth2/access_token";
            this.Option.Urls["UserInfo"] = "https://api.weibo.com/2/users/show.json";
            this.Option.Urls["EndSession"] = "https://api.weibo.com/2/account/end_session.json";
        }

        private void InitOAuthInterface()
        {
            if (User == null) User = new SinaWeiBoUserImpl(this); 
        }
        #endregion

        #region 重写父类方法
        /// <summary>
        /// 解析访问令牌
        /// </summary>
        /// <param name="responseResult"></param>
        protected override void ParseAccessToken(String responseResult)
        {
            //解析新浪微博授权令牌数据
            dynamic resultData = DynamicHelper.FromJSON(responseResult);
            this.Token.AccessToken = resultData.access_token;
            this.Token.ExpiresIn = (int)resultData.expires_in;
            this.Token.OAuthId = resultData.uid;

            //debug
            this.Token.TraceInfo = responseResult;
        }

        /// <summary>
        /// Http请求Url数据处理
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="isMultipart"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected override RequestOption[] RequestGetExecuting(params RequestOption[] parameters)
        {
            List<RequestOption> requestOptions = new List<RequestOption>();
            requestOptions.AddRange(parameters);
            if (!string.IsNullOrEmpty(this.Token.AccessToken))
            {
                requestOptions.Add(new RequestOption("access_token", this.Token.AccessToken));
            }
            else
            {
                requestOptions.Add(new RequestOption("source", this.Option.ClientId));
            }

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
