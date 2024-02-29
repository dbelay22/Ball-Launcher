using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor.SceneManagement;
using Ypx.Unity.Debug;
using App = BallLauncher.Core.App;


public class GameTest
{
    private App _app;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        // Load Main scene
        EditorSceneManager.LoadScene(0);
    }

    [UnityTest, Order(1)]
    public IEnumerator AppInstance()
    {
        _app = Object.FindFirstObjectByType<App>();

        Assert.NotNull(_app);

        yield return null;
    }

    [UnityTest, Order(2)]
    public IEnumerator AppSettings()
    {
        Assert.NotNull(_app.Settings);

        yield return null;
    }

    [UnityTest, Order(3)]
    public IEnumerator AppFramerateMobile()
    {
        Assert.AreEqual(30, Application.targetFrameRate);

        yield return null;
    }

    [UnityTest, Order(4)]
    public IEnumerator YLoggerComponentInstance()
    {
        YLoggerComponent loggerComponent = Object.FindFirstObjectByType<YLoggerComponent>();

        Assert.NotNull(loggerComponent);

        yield return null;
    }

}
