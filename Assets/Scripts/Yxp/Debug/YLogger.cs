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
            _currentLogger = new UnityLogger();            
            _enabled = true;            
            _logLevel = YLogLevel.Debug;
        }

        public static void Debug(object message, object sender = null)
        {
            if (!_enabled) return;

            if (_logLevel > YLogLevel.Debug) return;

            _currentLogger.Debug(message, sender);
        }

        public static void Warning(object message, object sender = null)
        {
            if (!_enabled) return;

            if (_logLevel > YLogLevel.Warning) return;

            _currentLogger.Warning(message, sender);
        }

        public static void Error(object message, object sender = null)
        {
            if (!_enabled) return;

            if (_logLevel > YLogLevel.Error) return;

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
    }

    public enum YLogLevel
    { 
        Debug = 0,
        Warning = 10,
        Error = 20
    }

}
