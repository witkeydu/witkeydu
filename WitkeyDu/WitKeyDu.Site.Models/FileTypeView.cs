using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitKeyDu.Site.Models
{
    public class FileTypeView
    {
        public string FileTypeName { get; set; }

        /// <summary>
        /// 论坛帖类型名称
        /// </summary>
        public string FileTypeLogo { get; set; }

        /// <summary>
        /// 父级论坛帖类型
        /// </summary>
        public string FileParentTypeID { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        public string FileTypeURL { get; set; }
    }
}
