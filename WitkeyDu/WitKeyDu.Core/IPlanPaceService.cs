using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitKeyDu.Core.Models.Tasks;
using WitKeyDu.Component.Tools;

namespace WitKeyDu.Core
{
    /// <summary>
    /// 计划进度核心业务契约
    /// </summary>
    public interface IPlanPaceService
    {
        #region 属性
        /// <summary>
        /// 获取计划进度查询数据集
        /// </summary>
        IQueryable<PlanPace> PlanPaces { get; }
        #endregion
        #region 公有方法
        /// <summary>
        ///  发表评论
        /// </summary>
        /// <param name="TaskInfo">评论信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult ReleasePlanPace(PlanPace PlanPaceInfo);
        #endregion
    }
}
