namespace Yxp.Debug
{
    public class YLoggerController
    {
        public YLoggerController(ILogger logger, YLoggerSettings settings)
        {
            ApplyLogger(logger);            
            ApplySettings(settings);
        }

        public void ApplyLogger(ILogger logger)
        {
            YLogger.UseLogger(logger);
        }

        public void ApplySettings(YLoggerSettings settings)
        {
            YLogger.UseSettings(settings);
        }
    }
}
