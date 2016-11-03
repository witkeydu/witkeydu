using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitKeyDu.Component.Tools;
using System.ComponentModel.Composition;
using WitKeyDu.Core.Data.Repositories.Stores;
using WitKeyDu.Core.Models.Stores;

namespace WitKeyDu.Core.Impl
{
    /// <summary>
    /// 任务模块核心实现
    /// </summary>
    public abstract class ProjectService : CoreServiceBase, IProjectService
    {
        #region 属性
        #region 受保护的属性
        /// <summary>
        /// 获取或设置 用户信息数据访问对象
        /// </summary>
        /// <summary>
        /// 获取或设置 任务信息数据访问对象
        /// </summary>
        [Import]
        protected IProjectRepository ReleaseProjectRepository { get; set; }
        #endregion
        #region 共有属性
        /// <summary>
        /// 获取任务查询数据集
        /// </summary>
        public IQueryable<Project> Projects
        {
            get { return ReleaseProjectRepository.Entities; }
        }
        #endregion
        #endregion
        public virtual OperationResult ReleaseProject(Project ProjectInfo)
        {
            try
            {
                PublicHelper.CheckArgument(ProjectInfo, "ProjectInfo");
                ReleaseProjectRepository.Insert(ProjectInfo);
                return new OperationResult(OperationResultType.Success, "项目发布成功。", ProjectInfo);
            }
            catch (Exception ex)
            {
                return new OperationResult(OperationResultType.Error, ex.Message.ToString());
            }
        }
    }
}
