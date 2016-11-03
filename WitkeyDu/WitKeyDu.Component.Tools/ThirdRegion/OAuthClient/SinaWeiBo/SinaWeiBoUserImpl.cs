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
    /// 新浪微博开放平台OAuth客户端用户信息接口实现
    /// </summary>
    public class SinaWeiBoUserImpl : IUserInterface
    {
        #region //字段
        private IOAuthClient oauth;
        #endregion

        #region //构造方法
        public SinaWeiBoUserImpl(IOAuthClient oauth)
        {
            this.oauth = oauth;
        }
        #endregion

        #region //接口实现

        #region //接口方法实现
        /// <summary>
        /// 获取第三方OAuth平台用户信息
        /// response result:
        /// {
        ///    "id": 1404376560,
        ///    "screen_name": "zaku",
        ///    "name": "zaku",
        ///    "province": "11",
        ///    "city": "5",
        ///    "location": "北京 朝阳区",
        ///    "description": "人生五十年，乃如梦如幻；有生斯有死，壮士复何憾。",
        ///    "url": "http://blog.sina.com.cn/zaku",
        ///    "profile_image_url": "http://tp1.sinaimg.cn/1404376560/50/0/1",
        ///    "domain": "zaku",
        ///    "gender": "m",
        ///    "followers_count": 1204,
        ///    "friends_count": 447,
        ///    "statuses_count": 2908,
        ///    "favourites_count": 0,
        ///    "created_at": "Fri Aug 28 00:00:00 +0800 2009",
        ///    "following": false,
        ///    "allow_all_act_msg": false,
        ///    "geo_enabled": true,
        ///    "verified": false,
        ///    "status": {
        ///        "created_at": "Tue May 24 18:04:53 +0800 2011",
        ///        "id": 11142488790,
        ///        "text": "我的相机到了。",
        ///        "source": "<a href="http://weibo.com" rel="nofollow">新浪微博</a>",
        ///       "favorited": false,
        ///        "truncated": false,
        ///        "in_reply_to_status_id": "",
        ///        "in_reply_to_user_id": "",
        ///        "in_reply_to_screen_name": "",
        ///        "geo": null,
        ///        "mid": "5610221544300749636",
        ///        "annotations": [],
        ///        "reposts_count": 5,
        ///        "comments_count": 8
        ///    },
        ///    "allow_all_comment": true,
        ///    "avatar_large": "http://tp1.sinaimg.cn/1404376560/180/0/1",
        ///    "verified_reason": "",
        ///    "follow_me": false,
        ///    "online_status": 0,
        ///    "bi_followers_count": 215
        ///}
        /// </summary>
        /// <returns></returns>
        public dynamic GetUserInfo()
        {
            String responseResult = this.oauth.Get(this.oauth.Option.Urls["UserInfo"], new RequestOption() { Name = "uid", Value = this.oauth.Token.OAuthId });
            dynamic jsonResult = DynamicHelper.FromJSON(responseResult);

            #region //格式化基础的用户信息
            this.oauth.Token.User.OAuthId = this.oauth.Token.OAuthId;
            this.oauth.Token.User.Nickname = jsonResult.screen_name;

            IDictionary<String, String> sexMaps = new Dictionary<String, String>();
            sexMaps["f"] = "0"; //女
            sexMaps["m"] = "1"; //男
            sexMaps["n"] = "2"; //保密
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
