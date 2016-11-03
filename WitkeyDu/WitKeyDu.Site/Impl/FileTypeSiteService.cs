using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using WitKeyDu.Core;

namespace WitKeyDu.Site.Impl
{
    /// <summary>
    /// 文件类型模块站点业务实现
    /// </summary>
    [Export(typeof(IFileTypeSiteContract))]
    internal class FileTypeSiteService : IFileTypeService, IFileTypeSiteContract
    {
    }
}
