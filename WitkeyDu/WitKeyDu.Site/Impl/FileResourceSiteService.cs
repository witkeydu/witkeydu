using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using WitKeyDu.Core.Impl;
using WitKeyDu.Core.Models.Files;
using WitKeyDu.Site.Models;
using WitKeyDu.Component.Tools;

namespace WitKeyDu.Site.Impl
{
    /// <summary>
    /// 文件存储模块站点业务实现
    /// </summary>
    [Export(typeof(IFileResourceSiteContract))]
    internal class FileResourceSiteService : FileResourceService,IFileResourceSiteContract
    {
        public OperationResult ReleaseFileResource(FileResourceView model)
        {
            FileResource releaseFileResource = new FileResource
            {
                 FileSrc=model.FileSrc,
                 FileCode=model.FileCode                 
            };
            OperationResult result = base.ReleaseFileResource(releaseFileResource);
            return result;
        }
    }
}
