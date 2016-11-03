using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitKeyDu.Core;
using WitKeyDu.Component.Tools;
using WitKeyDu.Site.Models;

namespace WitKeyDu.Site
{
    public interface IFileResourceSiteContract : IFileResourceService
    {
        /// <summary>
        /// 文件存储站点业务契约
        /// </summary>
        /// <param name="forumModel">分享文件模型信息</param>
        /// <returns></returns>
        OperationResult ReleaseFileResource(FileResourceView model);
    }
}
