namespace Yxp.Debug
{
    public interface ILogger
    {
        public void Verbose(object message, object sender, bool showTimestamp);
        public void Debug(object message, object sender, bool showTimestamp);        
        public void Warning(object message, object sender, bool showTimestamp);
        public void Error(object message, object sender, bool showTimestamp);
    }

}