using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.SessionState;
using System.Web;
using WitKeyDu.Component.Tools;
using WitKeyDu.Site.Models;
using WitKeyDu.Core.Models.Stores;
using WitKeyDu.Core.Models.Account;
using WitKeyDu.Core.Impl;
using System.ComponentModel.Composition;

namespace WitKeyDu.Site.Impl
{
    /// <summary>
    /// 项目模块站点业务实现
    /// </summary>
    [Export(typeof(IProjectSiteContract))]
    internal class ProjectSiteService : ProjectService,IProjectSiteContract
    {
        HttpSessionState Session = HttpContext.Current.Session;
        HttpServerUtility Server = HttpContext.Current.Server;
        HttpRequest Request = HttpContext.Current.Request;
        HttpResponse Response = HttpContext.Current.Response;
        public OperationResult ReleaseProject(ProjectView model)
        {
            PublicHelper.CheckArgument(model, "model");
            if (Session["SystemUser"] == "" || Session["SystemUser"] == null)
            {
                return new OperationResult(OperationResultType.PurviewLack, "登陆后，方可发布任务！");
            }
            Project releaseProject = new Project
            {
                 ProjectName=model.ProjectName,
                 ProjectIntroduction=model.ProjectIntroduction,
                 ProjectLogo=model.ProjectLogo,
                 ProjectPrice=model.ProjectPrice,
                 TaskTypeID=model.TaskTypeID,
                 ShowUrl=model.ShowUrl,
                 SystemUserID = ((SystemUser)Session["SystemUser"]).Id,
            };
            OperationResult result = base.ReleaseProject(releaseProject);
            return result;
        }
    }
}
