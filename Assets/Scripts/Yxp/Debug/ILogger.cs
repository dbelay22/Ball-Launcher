
namespace Yxp.Debug
{
    public interface ILogger
    {
        public void Debug(object message, object sender);        
        public void Warning(object message, object sender);
        public void Error(object message, object sender);
        public void DebugWithTimestamp(object message, object sender);
        public void WarningWithTimestamp(object message, object sender);
        public void ErrorWithTimestamp(object message, object sender);
    }

}