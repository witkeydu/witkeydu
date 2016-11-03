using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitKeyDu.Core.Models.Stores;
using WitKeyDu.Core.Models.Tasks;
using WitKeyDu.Component.Tools;

namespace WitKeyDu.Core.Impl
{
    /// <summary>
    /// 项目模块核心业务契约
    /// </summary>
    public interface IProjectService
    {
        #region 属性
        /// <summary>
        /// 获取 发布任务查询数据集
        /// </summary>
        IQueryable<Project> Projects { get; }
        #endregion

        #region 公有方法
        /// <summary>
        ///  发布任务
        /// </summary>
        /// <param name="TaskInfo">任务信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult ReleaseProject(Project ProjectInfo);
        #endregion
    }
}
