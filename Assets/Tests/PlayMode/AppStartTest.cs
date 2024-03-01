using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor.SceneManagement;
using Ypx.Unity.Debug;
using App = BallLauncher.Core.App;
using Yxp.Unity.Command;
using Yxp.Unity.Utils;

public class AppStartTest
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
        App[] apps = ObjectUtils.FastFindObjectsOfType<App>();

        Assert.AreEqual(1, apps.Length);

        Assert.NotNull(apps[0]);

        _app = apps[0];

        yield return null;
    }

    [UnityTest, Order(2)]
    public IEnumerator AppSettings()
    {
        Assert.NotNull(_app.Settings);

        yield return null;
    }

    [UnityTest, Order(3)]
    public IEnumerator YLoggerComponentInstance()
    {
        YLoggerComponent[] loggerComponents = ObjectUtils.FastFindObjectsOfType<YLoggerComponent>();

        Assert.AreEqual(1, loggerComponents.Length);

        Assert.NotNull(loggerComponents[0]);

        yield return null;
    }

    [UnityTest, Order(4)]
    public IEnumerator CommandInvokerInstance()
    {
        CommandInvoker[] commandInvokers = ObjectUtils.FastFindObjectsOfType<CommandInvoker>();

        Assert.AreEqual(1, commandInvokers.Length);

        Assert.NotNull(commandInvokers[0]);

        yield return null;
    }

    [UnityTest, Order(5)]
    public IEnumerator AppFramerateMobile()
    {
        Assert.AreEqual(30, Application.targetFrameRate);

        yield return null;
    }

}
