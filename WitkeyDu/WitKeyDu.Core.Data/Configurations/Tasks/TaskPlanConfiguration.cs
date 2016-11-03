using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using WitKeyDu.Component.Data;
using WitKeyDu.Core.Models;

namespace WitKeyDu.Core.Data.Configurations.Tasks
{
    partial class TaskPlanConfiguration
    {
        partial void TaskPlanConfigurationAppend()
        {
            HasRequired(m => m.User)
                .WithMany()
                .HasForeignKey(m => m.SystemUserID)
                .WillCascadeOnDelete(false);

            HasRequired(m => m.Task)
                .WithMany()
                .HasForeignKey(m => m.TaskID)
                .WillCascadeOnDelete(false);
        }
    }
}
