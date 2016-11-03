using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitKeyDu.Component.Tools;
using WitKeyDu.Site.Models;
using System.ComponentModel.Composition;
using WitKeyDu.Core.Impl;
using WitKeyDu.Core.Models.Tasks;
using System.Web.SessionState;
using System.Web;
using WitKeyDu.Core.Models.Account;

namespace WitKeyDu.Site.Impl
{
    /// <summary>
    /// 任务模块站点业务实现
    /// </summary>
    [Export(typeof(ITaskModuleSiteContract))]
    internal class TaskModuleSiteService : TaskModuleService, ITaskModuleSiteContract
    {
        HttpSessionState Session = HttpContext.Current.Session;
        HttpServerUtility Server = HttpContext.Current.Server;
        HttpRequest Request = HttpContext.Current.Request;
        HttpResponse Response = HttpContext.Current.Response;
        public OperationResult ReleaseTask(ReleaseTaskModel model)
        {
            PublicHelper.CheckArgument(model, "model");
            if (Session["SystemUser"] == "" || Session["SystemUser"] == null)
            {
                return new OperationResult(OperationResultType.PurviewLack, "登陆后，方可发布任务！");
            }
            Task releaseTask = new Task
            {
                TaskName = model.TaskName,
                TaskContent = model.TaskContent,
                TaskStartTime = model.TaskStartTime,
                TaskReward = model.TaskReward,
                TaskTypeID=model.TaskTypeID,
                TaskLogo = model.TaskLogo,
                TaskEndTime = model.TaskEndTime,
                DayNum=model.TaskEndTime.Subtract(model.TaskStartTime).Days,
                SystemUserID = ((SystemUser)Session["SystemUser"]).Id,
                EmployeTime= DateTime.Now
            };
            OperationResult result = base.ReleaseTask(releaseTask);
            return result;
        }

        public OperationResult UpdateTask(Task model)
        {

            if (Session["SystemUser"] == "")
            {
                return new OperationResult(OperationResultType.PurviewLack, "用户尚未登录！");
            }
            PublicHelper.CheckArgument(model, "model");
            OperationResult result = base.UpdateTask(model);
            return result;
        }
    }
}
