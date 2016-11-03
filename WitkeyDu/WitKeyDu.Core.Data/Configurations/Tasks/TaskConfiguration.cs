using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;

using WitKeyDu.Component.Data;
using WitKeyDu.Core.Models;

namespace WitKeyDu.Core.Data.Configurations.Tasks
{
    partial class TaskConfiguration
    {
        partial void TaskConfigurationAppend()
        {
            HasRequired(m=>m.User)
                .WithMany()
                .HasForeignKey(m => m.SystemUserID)
                .WillCascadeOnDelete(false);

            HasRequired(m => m.TaskType)
                .WithMany()
                .HasForeignKey(m => m.TaskTypeID)
                .WillCascadeOnDelete(false);
        }
    }
}
