using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitKeyDu.Core.Impl;
using System.ComponentModel.Composition;

namespace WitKeyDu.Site.Impl
{

    [Export(typeof(IForumTypeSiteContract))]
    internal class ForumTypeSiteService : ForumTypeService, IForumTypeSiteContract
    {

    }
}
