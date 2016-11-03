using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;

using WitKeyDu.Component.Tools;
using WitKeyDu.Component.Tools.Extensions;
using WitKeyDu.Component.Tools.Logging;
using WitKeyDu.Core.Models.Systems;


namespace WitKeyDu.Site.Helper.Logging
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AdminLogAttribute : ActionFilterAttribute
    {
        private IDictionary<string, object> _actionParameters;

        //参数值只有这能拿到
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _actionParameters = filterContext.ActionParameters;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Type controllerType = filterContext.ActionDescriptor.ControllerDescriptor.ControllerType;
            Logger logger = Logger.GetLogger(controllerType);
            string methodName = string.Format("{0}.{1}", controllerType.FullName, filterContext.ActionDescriptor.ActionName);
            LogInfo log = new LogInfo(methodName) { LogType = LogType.Admin };
            //参数信息
            ParameterDescriptor[] parameters = filterContext.ActionDescriptor.GetParameters();
            IEnumerable<string> paramInfos = parameters.Select(parameter => parameter.ParameterName)
                .Select(name => string.Format("{0}={1}", name, _actionParameters[name]));
            string paramsInfo = paramInfos.ExpandAndToString(";");
            object customMessage = filterContext.Controller.TempData[SiteStaticStrings.LogCustomeMessage];
            object operationResult = filterContext.Controller.TempData[SiteStaticStrings.LogOperationResult];
            log.Message = customMessage == null ? null : (string)customMessage;
            if (operationResult != null)
            {
                OperationResult result = (OperationResult)operationResult;
                log.ResultType = result.ResultType;
                log.Message = log.Message ?? result.LogMessage ?? (result.Message ?? result.ResultType.ToDescription());
            }
            
        }

    }
}
