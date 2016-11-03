using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using WitKeyDu.Core.Models.Files;
using WitKeyDu.Component.Tools;
using WitKeyDu.Core.Data.Repositories.Files;

namespace WitKeyDu.Core.Impl
{
    public abstract class FileResourceService : CoreServiceBase, IFileResourceService
    {
        #region 属性
        #region 受保护的属性

        /// <summary>
        /// 获取或设置 论坛帖信息数据访问对象
        /// </summary>
        [Import]
        protected IFileResourceRepository FileResourceRepository { get; set; }
        #endregion
        #region 共有属性
        /// <summary>
        /// 获取论坛帖查询数据集
        /// </summary>
        public IQueryable<FileResource> FileResources
        {
            get { return FileResourceRepository.Entities; }
        }

        #endregion
        #endregion
        public virtual OperationResult ReleaseFileResource(FileResource FileResourceInfo)
        {
            try
            {
                PublicHelper.CheckArgument(FileResourceInfo, "FileResourceInfo");
                FileResourceRepository.Insert(FileResourceInfo);
                return new OperationResult(OperationResultType.Success, "文件分享成功。", FileResourceInfo);
            }
            catch (Exception ex)
            {
                return new OperationResult(OperationResultType.Error, ex.Message.ToString());
            }
        }

    }
}
