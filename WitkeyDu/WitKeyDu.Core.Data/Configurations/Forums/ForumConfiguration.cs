using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitKeyDu.Core.Data.Configurations.Forums
{
    partial class ForumConfiguration
    {
        partial void ForumConfigurationAppend()
        {
            HasRequired(m => m.User)
                .WithMany()
                .HasForeignKey(m => m.SystemUserID)
                .WillCascadeOnDelete(false);
            HasRequired(m => m.ForumType)
                .WithMany()
                .HasForeignKey(m => m.ForumTypeID)
                .WillCascadeOnDelete(false);
        }
    }
}
