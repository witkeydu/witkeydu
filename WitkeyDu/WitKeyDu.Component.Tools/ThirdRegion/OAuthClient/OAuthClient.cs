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
    /// OAuthClient抽象类
    /// </summary>
    public abstract class OAuthClient:IOAuthClient
    {
        #region //构造方法
        public OAuthClient(String clientId, String clientSecret, String callbackUrl)
        {
            this.Option = new AuthOption() { ClientId = clientId, ClientSecret = clientSecret, CallbackUrl = callbackUrl };
            this.Token = new AuthToken();
        }
        #endregion

        #region //接口实现
        #region //属性
        public AuthOption Option { get; private set; }

        public AuthToken Token { get; private set; }

        public IUserInterface User { get; protected set; }
        #endregion

        #region //接口方法实现
        public virtual string GetAuthorizeUrl(ResponseType responseType = ResponseType.Code)
        {
            UriBuilder uriBuilder = new UriBuilder(this.Option.AuthorizeUrl);
            List<RequestOption> authorizeOptions = new List<RequestOption>() { 
				new RequestOption(){ Name= "client_id", Value= this.Option.ClientId},
				new RequestOption(){ Name="redirect_uri", Value = this.Option.CallbackUrl},
                new RequestOption(){ Name="response_type", Value = responseType.ToString().ToLower()}
			};
            if (!String.IsNullOrEmpty(this.Option.Display)) authorizeOptions.Add(new RequestOption() { Name = "display", Value = this.Option.Display });
            if (!String.IsNullOrEmpty(this.Option.State)) authorizeOptions.Add(new RequestOption() { Name = "state", Value = this.Option.State });
            if (!String.IsNullOrEmpty(this.Option.Scope)) authorizeOptions.Add(new RequestOption() { Name = "scope", Value = this.Option.Scope });

            List<String> keyValuePairs = new List<String>();
            foreach (var item in authorizeOptions)
            {
                if (item.IsBinary) continue;
                var value = String.Format("{0}", item.Value);
                if (!String.IsNullOrEmpty(value)) keyValuePairs.Add(String.Format("{0}={1}", Uri.EscapeDataString(item.Name), Uri.EscapeDataString(value)));
            }
            uriBuilder.Query = String.Join("&", keyValuePairs.ToArray());
            return uriBuilder.Uri.ToString();
        }

        public virtual AuthToken GetAccessTokenByAuthorizationCode(string code)
        {
			return GetAccessToken(GrantType.AuthorizationCode, new Dictionary<string, string> { 
				{"code",code},
				{"redirect_uri", this.Option.CallbackUrl}
			});
        }

        public AuthToken GetAccessTokenByPassword(string passport, string password)
        {
            return GetAccessToken(GrantType.Password, new Dictionary<string, string> { 
				{"username",passport},
				{"password", password}
			});
        }

        public AuthToken GetAccessTokenByRefreshToken(string refreshToken){
            return GetAccessToken(GrantType.RefreshToken, new Dictionary<string, string> { 
				{"refresh_token",refreshToken}
			});
        }

        public String Get(String apiUrl, params RequestOption[] options)
        {
            return Request(RequestUrlExecuting(apiUrl), RequestMethod.Get, options);
        }

        public String Post(String apiUrl, params RequestOption[] options)
        {
            return Request(RequestUrlExecuting(apiUrl), RequestMethod.Post, options);
        }

        public virtual dynamic GetUserInfo() { return String.Empty; }

        public virtual void EndSession()
        {
            String responseResult = Get(this.Option.Urls["EndSession"], new RequestOption() { Name = "access_token", Value = this.Token.AccessToken });
        }
        #endregion
        #endregion

        #region //可重写的方法和属性
        protected abstract void ParseAccessToken(String responseResult);

        protected virtual RequestOption[] RequestGetExecuting(params RequestOption[] parameters) { return parameters; }
        protected virtual RequestOption[] RequestPostExecuting(params RequestOption[] parameters) { return parameters; }
        protected virtual void RequestHeaderExecuting(HttpWebClient httpRequest) { }
        protected virtual AuthToken GetAccessToken(GrantType type, Dictionary<string, string> parameters)
        {
            List<RequestOption> accessTokenOptions = new List<RequestOption>()
			{
				new RequestOption(){ Name= "client_id", Value= this.Option.ClientId},
				new RequestOption(){ Name="client_secret", Value = this.Option.ClientSecret}
			};

            switch (type)
            {
                case GrantType.AuthorizationCode:
                    {
                        accessTokenOptions.Add(new RequestOption() { Name = "grant_type", Value = "authorization_code" });
                        accessTokenOptions.Add(new RequestOption() { Name = "code", Value = parameters["code"] });
                        accessTokenOptions.Add(new RequestOption() { Name = "redirect_uri", Value = parameters["redirect_uri"] });
                    }
                    break;
                case GrantType.Password:
                    {
                        accessTokenOptions.Add(new RequestOption() { Name = "grant_type", Value = "password" });
                        accessTokenOptions.Add(new RequestOption() { Name = "username", Value = parameters["username"] });
                        accessTokenOptions.Add(new RequestOption() { Name = "password", Value = parameters["password"] });
                    }
                    break;
                case GrantType.RefreshToken:
                    {
                        accessTokenOptions.Add(new RequestOption() { Name = "grant_type", Value = "refresh_token" });
                        accessTokenOptions.Add(new RequestOption() { Name = "refresh_token", Value = parameters["refresh_token"] });
                    }
                    break;
            }

            String response = Post(this.Option.AccessTokenUrl, accessTokenOptions.ToArray());
            if (!string.IsNullOrEmpty(response))
            {
                ParseAccessToken(response);
            }
            return this.Token;
        }
        #endregion 

        #region //帮助方法
        /// <summary>
        /// 请求Url预处理
        /// </summary>
        /// <param name="apiUrl"></param>
        /// <returns></returns>
        private string RequestUrlExecuting(string apiUrl)
        {
            string requestUrl = string.Empty;
            if (apiUrl.StartsWith("http://") || apiUrl.StartsWith("https://"))
            {
                requestUrl = apiUrl;
            }
            else
            {
                String baseApiUrl = this.Option.ApiUrlBase;
                if (this.Option.ApiUrlBase.EndsWith("/")) baseApiUrl = this.Option.ApiUrlBase.Remove(this.Option.ApiUrlBase.Length - 1, 1);
                if (apiUrl.StartsWith("/")) apiUrl = apiUrl.Remove(0, 1);
                requestUrl = string.Format("{0}/{1}", baseApiUrl, apiUrl);
            }
            return requestUrl;
        }

        /// <summary>
        /// 发送Http请求
        /// </summary>
        /// <param name="url">请求Url</param>
        /// <param name="method">请求类型</param>
        /// <param name="parameters">参数集合</param>
        /// <returns></returns>
        private String Request(string url, RequestMethod method = RequestMethod.Get, params RequestOption[] parameters)
        {
            HttpWebClient httpWebClient = new HttpWebClient();
            HttpWebResponseResult responseResult = null;

            #region 发起Http数据请求
            switch (method)
            {
                case RequestMethod.Get:
                    {
                        #region //请求头和请求参数预处理
                        RequestHeaderExecuting(httpWebClient);
                        parameters = RequestGetExecuting(parameters);
                        #endregion

                        #region //传递参数
                        foreach (var item in parameters)
                        {
                            httpWebClient.SetField(item.Name, (string)item.Value);
                        }
                        #endregion

                        //发起请求
                        responseResult = httpWebClient.Get(url);
                    }
                    break;
                case RequestMethod.Post:
                    {
                        #region //请求头和请求参数预处理
                        RequestHeaderExecuting(httpWebClient);
                        parameters = RequestPostExecuting(parameters);
                        #endregion

                        #region //判断当前POST是否为Multipart（即同时包含普通表单字段和文件表单字段）
                        bool isMultipart = false;
                        foreach (var item in parameters)
                        {
                            if (item.IsBinary)
                            {
                                isMultipart = true;
                                break;
                            }
                        }
                        #endregion

                        #region //传递参数
                        if (isMultipart)
                        {
                            foreach (var item in parameters)
                            {
                                if (item.IsBinary)
                                {
                                    httpWebClient.SetField(item.Name, (byte[])item.Value, String.Empty, String.Empty);
                                }
                                else
                                {
                                    httpWebClient.SetField(item.Name, (string)item.Value, true);
                                }
                            }
                        }
                        else
                        {
                            foreach (var item in parameters)
                            {
                                httpWebClient.SetField(item.Name, (string)item.Value);
                            }
                        }
                        #endregion

                        //发起请求
                        responseResult = httpWebClient.Post(url);
                    }
                    break;
            }
            #endregion

            return responseResult.IsSuccess ? responseResult.ResponseText : String.Empty;
        }
        #endregion
    }
}
