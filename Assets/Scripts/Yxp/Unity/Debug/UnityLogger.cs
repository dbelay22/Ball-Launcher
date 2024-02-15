using UnityEngine;

namespace Yxp.Unity.Debug
{
    public class UnityLogger : Yxp.Debug.ILogger
    {
        public void Debug(object message, object sender, bool showTimestamp)
        {
            object decoratedMessage = DecorateMessage(message, showTimestamp);
            
            UnityEngine.Debug.Log(decoratedMessage, sender as Object);
        }

        public void Warning(object message, object sender, bool showTimestamp)
        {
            object decoratedMessage = DecorateMessage(message, showTimestamp);
            
            UnityEngine.Debug.LogWarning(decoratedMessage, sender as Object);
        }

        public void Error(object message, object sender, bool showTimestamp)
        {
            object decoratedMessage = DecorateMessage(message, showTimestamp);

            UnityEngine.Debug.LogError(decoratedMessage, sender as Object);
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
