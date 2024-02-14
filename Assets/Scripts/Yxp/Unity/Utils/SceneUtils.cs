using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Yxp.Unity.Utils
{
    public static class SceneUtils
    {
        public static void LoadNextScene()
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

            SceneManager.LoadScene(nextSceneIndex);
        }

        public static void ReloadCurrentScene()
        {
            string curSceneName = SceneManager.GetActiveScene().name;
            
            SceneManager.LoadScene(curSceneName);
        }

    }

}