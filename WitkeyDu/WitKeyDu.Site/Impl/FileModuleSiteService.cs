using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitKeyDu.Core.Impl;
using System.ComponentModel.Composition;
using System.Web;
using System.Web.SessionState;
using WitKeyDu.Component.Tools;
using WitKeyDu.Site.Models;
using WitKeyDu.Core.Models.Files;
using WitKeyDu.Core.Models.Account;

namespace WitKeyDu.Site.Impl
{
    /// <summary>
    /// 文件模块站点业务实现
    /// </summary>
    [Export(typeof(IFileModuleSiteContract))]
    internal class FileModuleSiteService : FileModuleService,IFileModuleSiteContract
    {
        HttpSessionState Session = HttpContext.Current.Session;
        HttpServerUtility Server = HttpContext.Current.Server;
        HttpRequest Request = HttpContext.Current.Request;
        HttpResponse Response = HttpContext.Current.Response;
        public OperationResult ReleaseFile(FileView model)
        {
            if (Session["SystemUser"] == ""||Session["SystemUser"]==null)
            {
                return new OperationResult(OperationResultType.PurviewLack, "登陆后，方可分享文件！");
            }
            PublicHelper.CheckArgument(model, "model");
            File releaseFile = new File
            {
                FileName = model.FileName,
                FileCode = model.FileCode,
                FileIntroduction = model.FileIntroduction,
                FileTypeID = 1,
                SystemUserID = ((SystemUser)Session["SystemUser"]).Id
            };
            OperationResult result = base.ReleaseFile(releaseFile);
            return result;
        }
    }
}
