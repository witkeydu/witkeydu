using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitKeyDu.Core.Impl;
using System.Web.SessionState;
using System.Web;
using System.ComponentModel.Composition;
using WitKeyDu.Component.Tools;
using WitKeyDu.Core.Models.Stores;
using WitKeyDu.Core.Models.Account;
using WitKeyDu.Site.Models;

namespace WitKeyDu.Site.Impl
{
    /// <summary>
    /// 店铺模块站点业务实现
    /// </summary>
    [Export(typeof(IStoreModuleSiteContract))]
    internal class StoreModuleSiteService : StoreService, IStoreModuleSiteContract
    {
        HttpSessionState Session = HttpContext.Current.Session;
        HttpServerUtility Server = HttpContext.Current.Server;
        HttpRequest Request = HttpContext.Current.Request;
        HttpResponse Response = HttpContext.Current.Response;
        public OperationResult ReleaseStore(StoreView model)
        {
            PublicHelper.CheckArgument(model, "model");
            if (Session["SystemUser"] == "" || Session["SystemUser"] == null)
            {
                return new OperationResult(OperationResultType.PurviewLack, "登陆后，方可创建店铺！");
            }
            Store storeModel = new Store
            {
                StoreContent=model.StoreContent,
                StoreLogo=model.StoreLogo,
                StoreName=model.StoreName,
                StoreContract=model.StoreContract,
                SystemUserID = ((SystemUser)Session["SystemUser"]).Id
            };
            OperationResult result = base.ReleaseStore(storeModel);
            return result;
        }
    }
}
