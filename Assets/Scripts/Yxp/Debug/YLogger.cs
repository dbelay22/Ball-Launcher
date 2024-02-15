using Yxp.Unity.Debug;

namespace Yxp.Debug
{
    public static class YLogger
    {
        static bool _enabled;

        static ILogger _currentLogger;

        static YLogLevel _logLevel;

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

        public static void Debug(object message, object sender = null)
        {
            if (!canLog(YLogLevel.Debug)) return;

            _currentLogger.Debug(message, sender);
        }

        public static void Warning(object message, object sender = null)
        {
            if (!canLog(YLogLevel.Warning)) return;

            _currentLogger.Warning(message, sender);
        }

        public static void Error(object message, object sender = null)
        {
            if (!canLog(YLogLevel.Error)) return;

            _currentLogger.Error(message, sender);
        }

        public static void SetEnabled(bool enabled)
        {
            _enabled = enabled;
        }

        public static void SetLogLevel(YLogLevel logLevel)
        {
            _logLevel = logLevel;
        }

        private static bool canLog(YLogLevel level)
        {
            if (!_enabled) return false;

            if (_logLevel > level) return false;

            if (_currentLogger == null) return false;

            return true;
        }

    }

    public enum YLogLevel
    { 
        Debug = 0,
        Warning = 10,
        Error = 20
    }

}
