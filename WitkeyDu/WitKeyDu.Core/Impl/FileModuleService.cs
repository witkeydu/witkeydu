using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using WitKeyDu.Core.Data.Repositories.Files;
using WitKeyDu.Core.Models.Files;
using WitKeyDu.Component.Tools;

namespace WitKeyDu.Core.Impl
{
    /// <summary>
    /// 文件模块核心业务实现
    /// </summary>
    public abstract class FileModuleService : CoreServiceBase, IFileModuleService
    {
        #region 属性
        #region 受保护的属性

        /// <summary>
        /// 获取或设置 论坛帖信息数据访问对象
        /// </summary>
        [Import]
        protected IFileRepository FileRepository { get; set; }
        /// <summary>
        /// 获取或设置 文件存储数据访问对象
        /// </summary>
        [Import]
        protected IFileResourceRepository FileResourceRepository { get; set; }
        /// <summary>
        /// 获取或设置 论坛帖类型信息数据访问对象
        /// </summary>
        [Import]
        protected IFileTypeRepository FileTypeRepository { get; set; }
        #endregion
        #region 共有属性
        /// <summary>
        /// 获取论坛帖查询数据集
        /// </summary>
        public IQueryable<File> Files
        {
            get { return FileRepository.Entities; }
        }
        
        /// <summary>
        /// 获取论坛帖类型数据查询数据集
        /// </summary>
        public IQueryable<FileType> FileTypes
        {
            get { return FileTypeRepository.Entities; }
        }
        #endregion
        #endregion
        public virtual OperationResult ReleaseFile(File FileInfo)
        {
            try
            {
                PublicHelper.CheckArgument(FileInfo, "FileInfo");
                File file = FileRepository.Entities.SingleOrDefault(m => m.FileCode == FileInfo.FileCode);
                if (file == null)
                {
                    FileRepository.Insert(FileInfo);
                }
                return new OperationResult(OperationResultType.Success, "文件分享成功。", FileInfo);
            }
            catch (Exception ex)
            {
                return new OperationResult(OperationResultType.Error, ex.Message.ToString());
            }
        }
    }
}
