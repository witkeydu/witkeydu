using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitKeyDu.Core.Impl;
using System.ComponentModel.Composition;
using WitKeyDu.Component.Tools;
using WitKeyDu.Site.Models;
using WitKeyDu.Core.Models.Tasks;

namespace WitKeyDu.Site.Impl
{
    /// <summary>
    /// 计划进度站点业务实现
    /// </summary>
    [Export(typeof(IPlanPaceSiteContract))]
    internal class PlanPaceSiteService : PlanPaceService, IPlanPaceSiteContract
    {

        public OperationResult ReleasePlanPace(PlanPaceView model)
        {
            PublicHelper.CheckArgument(model, "model");
            PlanPace releasePlanPace = new PlanPace
            { 
                 Remark=model.Remark,
                 PlanCode=model.PlanCode,
                 CompleteContent=model.CompleteContent,
                 PlanStartTime=model.PlanStartTime,
                 PlanEndTime=model.PlanEndTime
            };
            OperationResult result = base.ReleasePlanPace(releasePlanPace);
            return result;
        }
        
    }
}
