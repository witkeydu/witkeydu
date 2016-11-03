using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitKeyDu.Core.Data.Configurations.Files
{
    partial class FileConfiguration
    {

        partial void FileConfigurationAppend()
        {
            HasRequired(m => m.User)
                .WithMany()
                .HasForeignKey(m => m.SystemUserID)
                .WillCascadeOnDelete(false);

            HasRequired(m => m.FileType)
                .WithMany()
                .HasForeignKey(m => m.FileTypeID)
                .WillCascadeOnDelete(false);
        }
    }
}
