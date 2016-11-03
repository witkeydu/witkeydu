using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WitKeyDu.Component.Tools;
using System.ComponentModel;

namespace WitKeyDu.Core.Models.Systems
{
    /// <summary>
    /// 实体类——权限信息
    /// </summary>
    [Description("权限信息")]
    public class Permission : EntityBase<Guid>
    {
        /// <summary>
        /// Permission Action No
        /// </summary>
        public virtual int ActionNo
        {
            set;
            get;
        }

        /// <summary>
        /// Controller No
        /// </summary>
        public virtual int ControllerNo
        {
            set;
            get;
        }

        /// <summary>
        /// Controller Name
        /// </summary>
        public string ControllerName
        {
            set;
            get;
        }

        /// <summary>
        /// Permission Action Name
        /// </summary>
        public string ActionName
        {
            set;
            get;
        }

        /// <summary>
        /// Controller
        /// </summary>
        public string Controller
        {
            set;
            get;
        }

        /// <summary>
        /// Action
        /// </summary>
        public string Action
        {
            set;
            get;
        }
    }
}
