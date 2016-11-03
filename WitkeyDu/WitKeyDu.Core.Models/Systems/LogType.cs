using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitKeyDu.Core.Models.Systems
{
    /// <summary>
    /// 表示系统日志类型的枚举
    /// </summary>
    public enum LogType
    {
        /// <summary>
        /// 系统日志类型
        /// </summary>
        System = 0,

        /// <summary>
        /// 管理日志类型
        /// </summary>
        Admin = 1,

        /// <summary>
        /// 用户日志类型
        /// </summary>
        User = 2,

        /// <summary>
        /// 自动操作日志类型
        /// </summary>
        Automatic = 3
    }
}
