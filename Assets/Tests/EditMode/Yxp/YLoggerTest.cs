using NUnit.Framework;
using Yxp.Debug;
public class YLoggerTest
{
    [Test]
    public void Startup()
    {
        // Test the initial state of the logger is correct
        Assert.That(YLogger.Enabled.Equals(false));
        Assert.That(YLogger.LogLevel.Equals(YLogLevel.Warning));
    }
}
