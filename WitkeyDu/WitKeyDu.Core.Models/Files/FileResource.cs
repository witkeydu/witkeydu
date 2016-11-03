using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using WitKeyDu.Component.Tools;

namespace WitKeyDu.Core.Models.Files
{
    [Description("文件存储信息")]
    public class FileResource : EntityBase<int>
    {
        public FileResource()
        { 

        }
        /// <summary>
        ///文件存储路径
        /// </summary>
        [Required]
        [StringLength(500)]
        public string FileSrc { get; set; }
        /// <summary>
        ///     获取或设置 文件类型信息集合
        /// </summary>
        [Required]
        [StringLength(500)]
        public string FileCode { get; set; }
    }
}
