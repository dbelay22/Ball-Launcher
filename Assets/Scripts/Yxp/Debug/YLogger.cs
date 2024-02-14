using Yxp.Unity.Debug;

namespace Yxp.Debug
{
    public static class YLogger
    {
        static bool _enabled;

        static ILogger _currentLogger;

        static YLogger()
        {
            _currentLogger = new UnityLogger();
            _enabled = true;
        }

        public static void Debug(object message, object sender = null)
        {
            if (!_enabled) return;

            _currentLogger.Debug(message, sender);
        }

        public static void Warning(object message, object sender = null)
        {
            if (!_enabled) return;

            _currentLogger.Warning(message, sender);
        }

        public static void Error(object message, object sender = null)
        {
            if (!_enabled) return;

            _currentLogger.Error(message, sender);
        }

        public static void SetEnabled(bool enabled)
        {
            _enabled = enabled;
        }
    }

}
