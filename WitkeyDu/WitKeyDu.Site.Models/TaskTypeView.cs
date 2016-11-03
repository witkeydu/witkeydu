using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitKeyDu.Site.Models
{
    public class TaskTypeView
    {

        /// <summary>
        /// 任务类型编号
        /// </summary>
        public int TaskTypeID { get; set; }
        /// <summary>
        /// 任务类型名称
        /// </summary>
        public string TaskTypeName { get; set; }

        /// <summary>
        /// 任务类型Logo
        /// </summary>
        public string TaskTypeLogo { get; set; }

        /// <summary>
        /// 父级任务类型
        /// </summary>
        public int TaskParentTypeID { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        public string TaskTypeURL { get; set; }

        public List<TaskTypeView> ChildrenTaskType { get; set; }
    }
}
