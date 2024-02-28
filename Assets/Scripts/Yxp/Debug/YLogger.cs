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
        public static bool Enabled { get; set; }               
        public static YLogLevel LogLevel { get; set; }
        
        private static ILogger _currentLogger;

        private static bool _decorateWithTimestamp;

        static YLogger()
        {
            Enabled = false;
            LogLevel = YLogLevel.Warning;
        }

        public static void UseLogger(ILogger logger)
        {
            Enabled = true;            
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

        public static void SetDecorateWithTimestamp(bool enabled)
        {
            _decorateWithTimestamp = enabled;
        }

        private static bool canLog(YLogLevel level)
        {
            if (!Enabled) return false;

            if (LogLevel > level) return false;

            if (_currentLogger == null) return false;

            return true;
        }

    }

}
