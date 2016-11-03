using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitKeyDu.Core.Models.Files;
using WitKeyDu.Component.Tools;
using WitKeyDu.Core.Models.Forums;

namespace WitKeyDu.Core
{
    /// <summary>
    /// 文件模块核心业务契约
    /// </summary>
    public interface IFileModuleService
    {
        #region 属性
        /// <summary>
        /// 获取 分享文件查询数据集
        /// </summary>
        IQueryable<File> Files { get; }
        /// <summary>
        /// 获取 任务类型查询数据集
        /// </summary>
        IQueryable<FileType> FileTypes { get; }
        #endregion
        #region 公有方法
        /// <summary>
        ///  发布任务
        /// </summary>
        /// <param name="TaskInfo">任务信息</param>
        /// <returns>业务操作结果</returns>
        OperationResult ReleaseFile(File FileInfo);
        #endregion
    }
}
