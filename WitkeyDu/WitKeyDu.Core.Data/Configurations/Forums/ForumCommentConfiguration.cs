using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitKeyDu.Core.Data.Configurations.Forums
{
    partial class ForumCommentConfiguration
    {
        partial void ForumCommentConfigurationAppend()
        {
            HasRequired(m => m.User)
                .WithMany()
                .HasForeignKey(m => m.SystemUserID)
                .WillCascadeOnDelete(false);

            HasRequired(m => m.Forum)
                .WithMany()
                .HasForeignKey(m => m.ForumID)
                .WillCascadeOnDelete(false);
        }
    }
}
