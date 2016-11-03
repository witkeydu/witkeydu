using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitKeyDu.Site.Models
{
    public class FileResourceView
    {
        /// <summary>
        ///文件存储路径
        /// </summary>
        public string FileSrc { get; set; }
        /// <summary>
        ///     获取或设置 文件类型信息集合
        /// </summary>
        public string FileCode { get; set; }
        public string FileName { get; set; }
    }
}
