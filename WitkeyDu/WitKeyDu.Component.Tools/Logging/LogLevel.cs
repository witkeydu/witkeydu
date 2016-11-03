namespace WitKeyDu.Component.Tools.Logging
{
    /// <summary>
    ///     表示日志级别的枚举
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        ///     调试级别，用于调试时输出日志
        /// </summary>
        Debug,

        /// <summary>
        ///     消息级别，用于输出日常操作日志
        /// </summary>
        Info,

        /// <summary>
        ///     警告级别，用于输出操作警告日志
        /// </summary>
        Warn,

        /// <summary>
        ///     错误级别，用于输出普通异常日志
        /// </summary>
        Error,

        /// <summary>
        ///     严重错误级别，用于输出严重异常日志
        /// </summary>
        Fatal
    }
}