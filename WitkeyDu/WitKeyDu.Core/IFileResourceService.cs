using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitKeyDu.Core.Models.Files;
using WitKeyDu.Component.Tools;

namespace WitKeyDu.Core
{
    /// <summary>
    /// 文件存储模块核心业务契约
    /// </summary>
    public interface IFileResourceService
    {
        #region 属性
        /// <summary>
        /// 获取 分享文件查询数据集
        /// </summary>
        IQueryable<FileResource> FileResources { get; }
        #endregion
        #region 公有方法
        /// <summary>
        ///  发布任务
        /// </summary>
        /// <param name="TaskInfo">任务信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult ReleaseFileResource(FileResource FileResourceInfo);
        #endregion
    }
}
