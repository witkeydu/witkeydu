using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using WitKeyDu.Core.Data.Repositories.Stores;
using WitKeyDu.Core.Models.Stores;
using WitKeyDu.Component.Tools;

namespace WitKeyDu.Core.Impl
{
    public abstract class StoreService : CoreServiceBase, IStoreService
    {
        #region 属性
        #region 受保护的属性
        /// <summary>
        /// 获取或设置 文件存储数据访问对象
        /// </summary>
        [Import]
        protected IStoreRepository StoreRepository { get; set; }
        #endregion
        
        /// <summary>
        /// 获取论坛帖类型数据查询数据集
        /// </summary>
        public IQueryable<Store> Stores
        {
            get { return StoreRepository.Entities; }
        }
        #endregion
        #region 共有属性
        public virtual OperationResult ReleaseStore(Store StoreInfo)
        {
            try
            {
                PublicHelper.CheckArgument(StoreInfo, "StoreInfo");
                StoreRepository.Insert(StoreInfo);
                return new OperationResult(OperationResultType.Success, "文件分享成功。", StoreInfo);
            }
            catch (Exception ex)
            {
                return new OperationResult(OperationResultType.Error, ex.Message.ToString());
            }
        }
        #endregion

    }
}
