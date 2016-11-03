using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;

using WitKeyDu.Component.Tools.Extensions;


namespace WitKeyDu.Component.Data
{
    /// <summary>
    ///     数据单元操作类
    /// </summary>
    [Export(typeof(IUnitOfWork))]
    public class EFUnitOfWorkContext : UnitOfWorkContextBase
    {
        /// <summary>
        ///     获取 当前使用的数据访问上下文对象
        /// </summary>
        protected override DbContext Context
        {
            get
            {
                bool secondCachingEnabled = ConfigurationManager.AppSettings["EntityFrameworkCachingEnabled"].CastTo(false);
                return secondCachingEnabled ? EFCachingDbContext.Value : EFDbContext.Value;
            }
        }

        [Import("EF", typeof (DbContext))]
        private Lazy<EFDbContext> EFDbContext { get; set; }

        [Import("EFCaching", typeof(DbContext))]
        private Lazy<EFCachingDbContext> EFCachingDbContext { get; set; }
    }
}