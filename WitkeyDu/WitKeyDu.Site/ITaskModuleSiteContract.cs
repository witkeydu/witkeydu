using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitKeyDu.Core;
using WitKeyDu.Site.Models;
using WitKeyDu.Component.Tools;
using WitKeyDu.Core.Models.Tasks;

namespace WitKeyDu.Site.Impl
{
    /// <summary>
    /// 任务模块站点业务契约
    /// </summary>
    public interface ITaskModuleSiteContract:ITaskModuleService
    {
        /// <summary>
        /// 发布任务
        /// </summary>
        /// <param name="taskModel">发布任务模型信息</param>
        /// <returns></returns>
        OperationResult ReleaseTask(ReleaseTaskModel model);
        OperationResult UpdateTask(Task TaskInfo);
    }
}
