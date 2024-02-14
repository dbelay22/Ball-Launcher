using Yxp.Unity.Debug;

namespace Yxp.Debug
{
    public static class YLogger
    {
        static bool _enabled = false;

        static ILogger _currentLogger = new UnityLogger();

        public static void Debug(object message, object sender)
        {
            if (!_enabled) return;

            _currentLogger.Debug(message, sender);
        }

        public static void Warning(object message, object sender)
        {
            if (!_enabled) return;

            _currentLogger.Warning(message, sender);
        }

        public static void Error(object message, object sender)
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
