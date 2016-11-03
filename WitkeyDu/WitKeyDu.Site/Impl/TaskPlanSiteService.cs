using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using WitKeyDu.Core.Impl;
using WitKeyDu.Component.Tools;
using WitKeyDu.Core.Models.Tasks;
using WitKeyDu.Site.Models;
using System.Web.SessionState;
using System.Web;
using WitKeyDu.Core.Models.Account;

namespace WitKeyDu.Site.Impl
{
    /// <summary>
    /// 任务进度站点业务实现
    /// </summary>
    [Export(typeof(ITaskPlanSiteContract))]
    internal class TaskPlanSiteService : TaskPlanService, ITaskPlanSiteContract
    {
        HttpSessionState Session = HttpContext.Current.Session;
        HttpServerUtility Server = HttpContext.Current.Server;
        HttpRequest Request = HttpContext.Current.Request;
        HttpResponse Response = HttpContext.Current.Response;
        public OperationResult ReleaseTaskPlan(TaskPlanView model)
        {
            PublicHelper.CheckArgument(model, "model");
            TaskPlan releaseTaskPlan = new TaskPlan
            {
                 PlanContent=model.PlanContent,
                 PlanPrice=model.PlanPrice,
                 SystemUserID = ((SystemUser)Session["SystemUser"]).Id,
                 TaskID=model.TaskID,
                 PlanCode = model.PlanCode
            };
            OperationResult result = base.ReleaseTaskPlan(releaseTaskPlan);
            return result;
        }
    }
}
