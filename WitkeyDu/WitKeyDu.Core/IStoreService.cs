using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitKeyDu.Core.Models.Stores;
using WitKeyDu.Component.Tools;

namespace WitKeyDu.Core
{
    /// <summary>
    /// 店铺模块核心业务契约
    /// </summary>
    public interface IStoreService
    {
        #region 属性
        /// <summary>2
        /// 获取店铺查询数据集
        /// </summary>
        IQueryable<Store> Stores { get; }
        #endregion
        #region 公有方法
        /// <summary>
        ///  发布任务
        /// </summary>
        /// <param name="TaskInfo">任务信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult ReleaseStore(Store StoreInfo);
        #endregion
    }
}
