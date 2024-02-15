using UnityEngine;

namespace Yxp.Unity.Debug
{
    public class UnityLogger : Yxp.Debug.ILogger
    {
        public void Debug(object message, object sender)
        {
            UnityEngine.Debug.Log(message, sender as Object);
        }

        public void Warning(object message, object sender)
        {
            UnityEngine.Debug.LogWarning(message, sender as Object);
        }

        public void Error(object message, object sender)
        {
            UnityEngine.Debug.LogError(message, sender as Object);
        }

        public void DebugWithTimestamp(object message, object sender)
        {
            Debug(DecorateWithTimestamp(message), sender);
        }

        public void WarningWithTimestamp(object message, object sender)
        {
            Warning(DecorateWithTimestamp(message), sender);
        }

        public void ErrorWithTimestamp(object message, object sender)
        {
            Error(DecorateWithTimestamp(message), sender);
        }

        private object DecorateWithTimestamp(object message)
        {
            return $"({Time.realtimeSinceStartup}) {message}";
        }
    }
}
