using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor.SceneManagement;
using Ypx.Unity.Debug;
using App = BallLauncher.Core.App;
using Yxp.Unity.Command;

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
        YLoggerComponent[] loggerComponents = FastFindObjectsOfType<YLoggerComponent>();

        Assert.AreEqual(1, loggerComponents.Length);

        Assert.NotNull(loggerComponents[0]);

        yield return null;
    }

    [UnityTest, Order(5)]
    public IEnumerator CommandInvokerInstance()
    {
        CommandInvoker[] commandInvokers = FastFindObjectsOfType<CommandInvoker>();

        Assert.AreEqual(1, commandInvokers.Length);

        Assert.NotNull(commandInvokers[0]);

        yield return null;
    }

    private T[] FastFindObjectsOfType<T>() where T : Object
    { 
        return Object.FindObjectsByType<T>(FindObjectsInactive.Include, FindObjectsSortMode.None);
    }

}
