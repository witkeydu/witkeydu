using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;

using WitKeyDu.Component.Tools;


namespace WitKeyDu.Core.Models.Systems
{
    /// <summary>
    /// 实体类——日志信息
    /// </summary>
    [Description("日志信息")]
    public class LogInfo : EntityBase<Guid>
    {
        public LogInfo()
        {
            Id = CombHelper.NewComb();
            if (HttpContext.Current == null)
            {
                return;
            }
            Operator = HttpContext.Current.User.Identity.Name;
            if (string.IsNullOrEmpty(Operator))
            {
                Operator = "系统";
            }
            IpAddress = HttpContext.Current.Request.UserHostAddress;
            LogType = LogType.System;
            ResultType = OperationResultType.NoChanged;
        }

        public LogInfo(string method, string entity = null):this()
        {
            MethodName = method;
            EntityName = entity;
        }

        [StringLength(50)]
        public string Thread { get; set; }

        [StringLength(50)]
        public string Level { get; set; }

        [StringLength(100)]
        public string Logger { get; set; }

        [StringLength(50)]
        public string Operator { get; set; }

        [StringLength(15)]
        public string IpAddress { get; set; }

        [StringLength(300)]
        public string EntityName { get; set; }

        [StringLength(500)]
        public string MethodName { get; set; }

        public LogType LogType
        {
            get { return (LogType)LogTypeNum; }
            set { LogTypeNum = (int)value; }
        }

        public int LogTypeNum { get; set; }

        public OperationResultType ResultType
        {
            get { return (OperationResultType)ResultTypeNum; }
            set { ResultTypeNum = (int)value; }
        }

        public int ResultTypeNum { get; set; }

        public string Message { get; set; }

        public string Exception { get; set; }
    }
}
