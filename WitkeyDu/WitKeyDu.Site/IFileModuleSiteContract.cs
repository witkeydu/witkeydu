using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitKeyDu.Core;
using WitKeyDu.Component.Tools;
using WitKeyDu.Site.Models;

namespace WitKeyDu.Site
{
    public interface IFileModuleSiteContract:IFileModuleService
    {
        /// <summary>
        /// 分析文件
        /// </summary>
        /// <param name="forumModel">分享文件模型信息</param>
        /// <returns></returns>
        OperationResult ReleaseFile(FileView model);
    }
}
