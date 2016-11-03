using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using WitKeyDu.Core.Data.Repositories.Account;
using WitKeyDu.Core.Models.Tasks;
using WitKeyDu.Component.Tools;
using WitKeyDu.Core.Data.Repositories.Tasks;

namespace WitKeyDu.Core.Impl
{
    public abstract class PlanPaceService : CoreServiceBase,IPlanPaceService
    {
        #region 属性
        #region 受保护的属性
        /// <summary>
        /// 获取或设置 用户信息数据访问对象
        /// </summary>
        [Import]
        protected ISystemUserRepository SysUserRepository { get; set; }

        /// <summary>
        /// 获取或设置 论坛帖信息数据访问对象
        /// </summary>
        [Import]
        protected IPlanPaceRepository ReleasePlanPaceRepository { get; set; }
        #endregion
        #region 共有属性
        /// <summary>
        /// 获取论坛帖查询数据集
        /// </summary>
        public IQueryable<PlanPace> PlanPaces
        {
            get { return ReleasePlanPaceRepository.Entities; }
        }
        #endregion
        #endregion
        public virtual OperationResult ReleasePlanPace(PlanPace PlanPaceInfo)
        {
            try
            {
                PublicHelper.CheckArgument(PlanPaceInfo, "PlanPaceInfo");
                ReleasePlanPaceRepository.Insert(PlanPaceInfo);
                return new OperationResult(OperationResultType.Success, "计划进度新增成功。", PlanPaceInfo);
            }
            catch (Exception ex)
            {
                return new OperationResult(OperationResultType.Error, ex.Message.ToString());
            }
        }

    }
}
