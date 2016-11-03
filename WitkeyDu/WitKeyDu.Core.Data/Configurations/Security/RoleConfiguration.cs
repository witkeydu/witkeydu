using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;

using WitKeyDu.Component.Data;
using WitKeyDu.Core.Models;


namespace WitKeyDu.Core.Data.Configurations.Security
{
    partial class RoleConfiguration
    {
        partial void RoleConfigurationAppend()
        {
            HasMany(m => m.Users).WithMany(n => n.Roles);
        }
    }
}
