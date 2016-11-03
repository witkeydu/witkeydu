using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;

using WitKeyDu.Component.Data;
using WitKeyDu.Core.Models;


namespace WitKeyDu.Core.Data.Configurations.Account
{
    partial class LoginLogConfiguration
    {
        partial void LoginLogConfigurationAppend()
        {
            HasRequired(m => m.User).WithMany(n => n.LoginLogs);
        }
    }
}