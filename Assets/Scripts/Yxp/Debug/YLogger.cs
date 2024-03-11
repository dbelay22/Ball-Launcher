using System;

namespace Yxp.Debug
{
    public enum YLogLevel
    {
        Verbose = 0,
        Debug = 1,
        Warning = 10,
        Error = 20
    }

    [Serializable]
    public struct YLoggerSettings
    {
        public bool Enabled;
        public YLogLevel LogLevel;
        public bool DecorateWithTimestamp;

        public YLoggerSettings(bool enabled, YLogLevel level, bool decorateWithTimestamp)
        {
            Enabled = enabled;
            LogLevel = level;
            DecorateWithTimestamp = decorateWithTimestamp;
        }
    }

    public static class YLogger
    {
        public static bool Enabled { get { return _settings.Enabled; } }
        public static YLogLevel LogLevel { get { return _settings.LogLevel; } }
        
        private static ILogger _currentLogger;

        private static YLoggerSettings _settings;

        static YLogger()
        {
            _settings = new YLoggerSettings(enabled: false, level: YLogLevel.Warning, decorateWithTimestamp: false);
        }
        
        internal static void UseLogger(ILogger logger)
        {
            _currentLogger = logger;
        }

        internal static void UseSettings(YLoggerSettings settings)
        {
            _settings = settings;
        }

        private static bool canLog(YLogLevel level)
        {
            if (!Enabled) return false;

            if (LogLevel > level) return false;

            if (_currentLogger == null) return false;

            return true;
        }

        #region Public methods

        public static void Verbose(object message, object sender = null)
        {
            if (!canLog(YLogLevel.Verbose)) return;

            _currentLogger.Verbose(message, sender, _settings.DecorateWithTimestamp);
        }

        public static void Debug(object message, object sender = null)
        {
            if (!canLog(YLogLevel.Debug)) return;

            _currentLogger.Debug(message, sender, _settings.DecorateWithTimestamp);
        }

        public static void Warning(object message, object sender = null)
        {
            if (!canLog(YLogLevel.Warning)) return;

            _currentLogger.Warning(message, sender, _settings.DecorateWithTimestamp);
        }

        public static void Error(object message, object sender = null)
        {
            if (!canLog(YLogLevel.Error)) return;

            _currentLogger.Error(message, sender, _settings.DecorateWithTimestamp);
        }

        #endregion

    }

}
