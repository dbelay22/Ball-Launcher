namespace Yxp.Debug
{
    public enum YLogLevel
    {
        Verbose = 0,
        Debug = 1,
        Warning = 10,
        Error = 20
    }

    public static class YLogger
    {
        static bool _enabled;

        static ILogger _currentLogger;

        static YLogLevel _logLevel;

        static bool _decorateWithTimestamp;

        static YLogger()
        {
            _enabled = false;
            _logLevel = YLogLevel.Debug;
        }

        public static void UseLogger(ILogger logger)
        {
            _enabled = true;            
            _currentLogger = logger;
        }

        public static void Verbose(object message, object sender = null)
        {
            if (!canLog(YLogLevel.Verbose)) return;

            _currentLogger.Verbose(message, sender, _decorateWithTimestamp);
        }

        public static void Debug(object message, object sender = null)
        {
            if (!canLog(YLogLevel.Debug)) return;

            _currentLogger.Debug(message, sender, _decorateWithTimestamp);
        }

        public static void Warning(object message, object sender = null)
        {
            if (!canLog(YLogLevel.Warning)) return;

            _currentLogger.Warning(message, sender, _decorateWithTimestamp);
        }

        public static void Error(object message, object sender = null)
        {
            if (!canLog(YLogLevel.Error)) return;

            _currentLogger.Error(message, sender, _decorateWithTimestamp);
        }

        public static void SetEnabled(bool enabled)
        {
            _enabled = enabled;
        }

        public static void SetLogLevel(YLogLevel logLevel)
        {
            _logLevel = logLevel;
        }

        public static void SetDecorateWithTimestamp(bool enabled)
        {
            _decorateWithTimestamp = enabled;
        }

        private static bool canLog(YLogLevel level)
        {
            if (!_enabled) return false;

            if (_logLevel > level) return false;

            if (_currentLogger == null) return false;

            return true;
        }

    }

}
