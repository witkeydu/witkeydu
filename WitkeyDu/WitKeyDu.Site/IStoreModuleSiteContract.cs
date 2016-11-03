using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitKeyDu.Component.Tools;
using WitKeyDu.Site.Models;
using WitKeyDu.Core;

namespace WitKeyDu.Site
{
    /// <summary>
    /// 店铺模块站点业务契约
    /// </summary>
    public interface IStoreModuleSiteContract:IStoreService
    {
        /// <summary>
        /// 发布任务
        /// </summary>
        /// <param name="taskModel">发布任务模型信息</param>
        /// <returns></returns>
        OperationResult ReleaseStore(StoreView model);
    }
}
