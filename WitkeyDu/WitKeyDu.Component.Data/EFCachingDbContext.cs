using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;

using EFCachingProvider;
using EFCachingProvider.Caching;

using WitKeyDu.Component.Tools;


namespace WitKeyDu.Component.Data
{
    /// <summary>
    ///     启用缓存的自定义EntityFramework数据访问上下文
    /// </summary>
    [Export("EFCaching", typeof (DbContext))]
    public class EFCachingDbContext : EFDbContext
    {
        private static readonly InMemoryCache InMemoryCache = new InMemoryCache();

        public EFCachingDbContext()
            : base(CreateConnectionWrapper("default")) { }

        public EFCachingDbContext(string connectionStringName)
            : base(CreateConnectionWrapper(connectionStringName)) { }

        /// <summary>
        ///     由数据库连接串名称创建连接对象
        /// </summary>
        /// <param name="connectionStringName">数据库连接串名称</param>
        /// <returns></returns>
        private static DbConnection CreateConnectionWrapper(string connectionStringName)
        {
            PublicHelper.CheckArgument(connectionStringName, "connectionStringName");

            string providerInvariantName = "System.Data.SqlClient";
            string connectionString = null;
            ConnectionStringSettings connectionStringSetting = ConfigurationManager.ConnectionStrings[connectionStringName];
            if (connectionStringSetting != null)
            {
                providerInvariantName = connectionStringSetting.ProviderName;
                connectionString = connectionStringSetting.ConnectionString;
            }
            if (connectionString == null)
            {
                throw PublicHelper.ThrowComponentException("名称为“" + connectionStringName + "”数据库连接串的ConnectionString值为空。");
            }
            string wrappedConnectionString = "wrappedProvider=" + providerInvariantName + ";" + connectionString;
            EFCachingConnection connection = new EFCachingConnection
            {
                ConnectionString = wrappedConnectionString,
                CachingPolicy = CachingPolicy.CacheAll,
                Cache = InMemoryCache
            };

            return connection;
        }
    }
}