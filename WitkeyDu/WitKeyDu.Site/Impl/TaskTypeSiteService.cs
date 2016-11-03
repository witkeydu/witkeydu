using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitKeyDu.Core.Impl;
using System.ComponentModel.Composition;

namespace WitKeyDu.Site.Impl
{

    [Export(typeof(ITaskTypeSiteContract))]
    public class TaskTypeSiteService : TaskTypeService,ITaskTypeSiteContract
    {

    }
}
