using System;
using System.IO;
using System.Web;

using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Layout;


namespace WitKeyDu.Component.Tools.Logging
{
    /// <summary>
    ///     日志操作管理类
    /// </summary>
    public class Logger
    {
        #region 字段

        private ILog _log;

        #endregion

        #region 构造函数

        static Logger()
        {
            const string filename = "log4net.config";
            string basePath = HttpContext.Current != null
                ? AppDomain.CurrentDomain.SetupInformation.PrivateBinPath
                : AppDomain.CurrentDomain.BaseDirectory;
            string configFile = Path.Combine(basePath, filename);
            if (File.Exists(configFile))
            {
                XmlConfigurator.ConfigureAndWatch(new FileInfo(configFile));
                return;
            }

            //默认设置
            RollingFileAppender appender = new RollingFileAppender
            {
                Name = "root",
                File = "logs\\log",
                AppendToFile = true,
                RollingStyle = RollingFileAppender.RollingMode.Composite,
                DatePattern = "yyyyMMdd\".log\"",
                MaxSizeRollBackups = 10
            };

            PatternLayout layout = new PatternLayout("[%d{yyyy-MM-dd HH:mm:ss.fff}] %c.%M %t %n%m%n");
            appender.Layout = layout;
            BasicConfigurator.Configure(appender);
            appender.ActivateOptions();
        }

        #endregion

        #region 公共方法

        /// <summary>
        ///     获取指定名称的日志实例
        /// </summary>
        public static Logger GetLogger(string name)
        {
            return new Logger { _log = LogManager.GetLogger(name) };
        }

        /// <summary>
        ///     获取指定类型的日志实例
        /// </summary>
        public static Logger GetLogger(Type type)
        {
            return new Logger { _log = LogManager.GetLogger(type) };
        }

        /// <summary>
        ///     输出<see cref="LogLevel.Debug" />级别的日志
        /// </summary>
        public void Debug(object message, Exception exception = null)
        {
            if (exception == null)
            {
                _log.Debug(message);
            }
            else
            {
                _log.Debug(message, exception);
            }
        }

        /// <summary>
        ///     输出<see cref="LogLevel.Debug" />级别的日志
        /// </summary>
        public void DebugFormat(string format, params object[] args)
        {
            _log.DebugFormat(format, args);
        }

        /// <summary>
        ///     输出<see cref="LogLevel.Info" />级别的日志
        /// </summary>
        public void Info(object message, Exception exception = null)
        {
            if (exception == null)
            {
                _log.Info(message);
            }
            else
            {
                _log.Info(message, exception);
            }
        }

        /// <summary>
        ///     输出<see cref="LogLevel.Info" />级别的日志
        /// </summary>
        public void InfoFormat(string format, params object[] args)
        {
            _log.InfoFormat(format, args);
        }

        /// <summary>
        ///     输出<see cref="LogLevel.Warn" />级别的日志
        /// </summary>
        public void Warn(object message, Exception exception = null)
        {
            if (exception == null)
            {
                _log.Warn(message);
            }
            else
            {
                _log.Warn(message, exception);
            }
        }

        /// <summary>
        ///     输出<see cref="LogLevel.Warn" />级别的日志
        /// </summary>
        public void WarnFormat(string format, params object[] args)
        {
            _log.WarnFormat(format, args);
        }

        /// <summary>
        ///     输出<see cref="LogLevel.Error" />级别的日志
        /// </summary>
        public void Error(object message, Exception exception = null)
        {
            if (exception == null)
            {
                _log.Error(message);
            }
            else
            {
                _log.Error(message, exception);
            }
        }

        /// <summary>
        ///     输出<see cref="LogLevel.Error" />级别的日志
        /// </summary>
        public void ErrorFormat(string format, params object[] args)
        {
            _log.ErrorFormat(format, args);
        }

        /// <summary>
        ///     输出<see cref="LogLevel.Fatal" />级别的日志
        /// </summary>
        public void Fatal(object message, Exception exception = null)
        {
            if (exception == null)
            {
                _log.Fatal(message);
            }
            else
            {
                _log.Fatal(message, exception);
            }
        }

        /// <summary>
        ///     输出<see cref="LogLevel.Fatal" />级别的日志
        /// </summary>
        public void FatalFormat(string format, params object[] args)
        {
            _log.FatalFormat(format, args);
        }

        /// <summary>
        ///     输出指定<see cref="LogLevel" />级别的日志
        /// </summary>
        public void Write(LogLevel logLevel, object message, Exception exception = null)
        {
            switch (logLevel)
            {
                case LogLevel.Debug:
                    Debug(message, exception);
                    break;
                case LogLevel.Info:
                    Info(message, exception);
                    break;
                case LogLevel.Warn:
                    Warn(message, exception);
                    break;
                case LogLevel.Error:
                    Error(message, exception);
                    break;
                case LogLevel.Fatal:
                    Fatal(message, exception);
                    break;
            }
        }

        /// <summary>
        ///     输出指定<see cref="LogLevel" />级别的日志
        /// </summary>
        public void WriteFormat(LogLevel logLevel, string format, params object[] args)
        {
            switch (logLevel)
            {
                case LogLevel.Debug:
                    DebugFormat(format, args);
                    break;
                case LogLevel.Info:
                    InfoFormat(format, args);
                    break;
                case LogLevel.Warn:
                    WarnFormat(format, args);
                    break;
                case LogLevel.Error:
                    ErrorFormat(format, args);
                    break;
                case LogLevel.Fatal:
                    FatalFormat(format, args);
                    break;
            }
        }

        #endregion
    }
}