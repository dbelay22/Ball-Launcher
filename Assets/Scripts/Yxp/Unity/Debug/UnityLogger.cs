using UnityEngine;
using Yxp.Debug;

namespace Yxp.Unity.Debug
{
    public class UnityLogger : Yxp.Debug.ILogger
    {
        public void Debug(object message, object sender, bool showTimestamp)
        {
            LogMessage(YLogLevel.Debug, message, sender, showTimestamp);
        }

        public void Warning(object message, object sender, bool showTimestamp)
        {            
            LogMessage(YLogLevel.Warning, message, sender, showTimestamp);
        }

        public void Error(object message, object sender, bool showTimestamp)
        {
            LogMessage(YLogLevel.Error, message, sender, showTimestamp);
        }

        private void LogMessage(YLogLevel level, object message, object sender, bool showTimestamp)
        {
            object decoratedMessage = DecorateMessage(message, showTimestamp);

            switch (level)
            {
                case YLogLevel.Debug:
                    UnityEngine.Debug.Log(decoratedMessage, sender as Object);
                    break;

                case YLogLevel.Warning:
                    UnityEngine.Debug.LogWarning(decoratedMessage, sender as Object);
                    break;

                case YLogLevel.Error:
                    UnityEngine.Debug.LogError(decoratedMessage, sender as Object);
                    break;

            }
        }

        private object DecorateMessage(object message, bool showTimestamp)
        {
            if (showTimestamp)
            {
                return DecorateWithTimestamp(message);
            }

            return message;
        }
        
        private object DecorateWithTimestamp(object message)
        {
            return $"({Time.realtimeSinceStartup}) {message}";
        }

    }
}
