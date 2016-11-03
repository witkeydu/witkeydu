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
    /// 任务模块站点业务实现
    /// </summary>
    [Export(typeof(IForumModuleSiteContract))]
    internal class ForumModuleSiteService : ForumModuleService, IForumModuleSiteContract
    {
        HttpSessionState Session = HttpContext.Current.Session;
        HttpServerUtility Server = HttpContext.Current.Server;
        HttpRequest Request = HttpContext.Current.Request;
        HttpResponse Response = HttpContext.Current.Response;
        public OperationResult ReleaseFroum(ReleaseForumModel model)
        {
            if (Session["SystemUser"] == "" || Session["SystemUser"] == null)
            {
                return new OperationResult(OperationResultType.PurviewLack, "登陆后，方可发布帖子！");
            }
            PublicHelper.CheckArgument(model, "model");
            Forum releaseForum = new Forum
            {
                ForumName = model.ForumName,
                ForumContent = model.ForumContent,
                ForumLogo = model.ForumLogo,
                ForumTypeID = model.ForumTypeID,
                SystemUserID = ((SystemUser)Session["SystemUser"]).Id
                
            };
            OperationResult result = base.ReleaseForum(releaseForum);
            return result;
        }
    }
}
