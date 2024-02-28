using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor.SceneManagement;

using App = BallLauncher.Core.App;

public class GameTest
{
    private App _app;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Debug.Log("*** OneTimeSetup");

        //yield return new EnterPlayMode();
        // Load Main scene
        EditorSceneManager.LoadScene(0);
    }

    [UnityTest, Order(1)]
    public IEnumerator AppInstance()
    {
        Debug.Log("*** AppInstance");

        _app = Object.FindFirstObjectByType<App>();

        Assert.NotNull(_app);

        yield return null;
    }

    [UnityTest, Order(2)]
    public IEnumerator AppSettings()
    {
        Debug.Log("*** AppSettings");

        Assert.NotNull(_app.Settings);

        yield return null;
    }

    [UnityTest, Order(3)]
    public IEnumerator AppFramerateMobile()
    {
        Debug.Log("*** AppFramerateMobile");

        Assert.AreEqual(30, Application.targetFrameRate);

        yield return null;
    }

}
