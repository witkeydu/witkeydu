using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitKeyDu.Component.Tools.ThirdRegion
{
    /// <summary>
    /// Description: IOAuth接口
    /// </summary>
    public interface IOAuthClient
    {
        AuthOption Option { get; }
        AuthToken Token { get; }
        IUserInterface User { get; }

        String GetAuthorizeUrl(ResponseType responseType);
        AuthToken GetAccessTokenByAuthorizationCode(string code);
        AuthToken GetAccessTokenByPassword(string passport, string password);
        AuthToken GetAccessTokenByRefreshToken(string refreshToken);
        String Get(String url, params RequestOption[] options);
        String Post(String url, params RequestOption[] options);
    }

}
