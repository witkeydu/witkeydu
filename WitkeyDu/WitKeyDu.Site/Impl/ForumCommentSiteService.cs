using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitKeyDu.Component.Tools;
using WitKeyDu.Site.Models;
using System.ComponentModel.Composition;
using WitKeyDu.Core.Impl;
using WitKeyDu.Core.Models.Forums;
using System.Web.SessionState;
using System.Web;
using WitKeyDu.Core.Models.Account;

namespace WitKeyDu.Site.Impl
{
    /// <summary>
    /// 文件存储模块站点业务实现
    /// </summary>
    [Export(typeof(IForumCommentSiteContract))]
    internal class ForumCommentSiteService : ForumCommentService, IForumCommentSiteContract
    {
        HttpSessionState Session = HttpContext.Current.Session;
        HttpServerUtility Server = HttpContext.Current.Server;
        HttpRequest Request = HttpContext.Current.Request;
        HttpResponse Response = HttpContext.Current.Response;
        public OperationResult ReleaseFroumComment(ForumCommentView model)
        {
            if (Session["SystemUser"] == "" || Session["SystemUser"] == null)
            {
                return new OperationResult(OperationResultType.PurviewLack, "登陆后，方可发表评论！");
            }
            PublicHelper.CheckArgument(model, "model");
            ForumComment releaseForumComment = new ForumComment
            {
                CommentContent = model.CommentContent,
                SystemUserID = ((SystemUser)Session["SystemUser"]).Id,
                UserName=((SystemUser)Session["SystemUser"]).UserName,
                HeadImage = ((SystemUser)Session["SystemUser"]).HeadImage,
                ForumID=model.ForumID,
                CommentUserID=model.CommentUserID,
                CommentUserName=model.CommentUserName,
                ParentCommentID=model.ParentCommentID
            };
            OperationResult result = base.ReleaseForumComment(releaseForumComment);
            return result;
        }
    }
}
