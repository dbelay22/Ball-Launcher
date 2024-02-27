using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yxp.Debug;
using Yxp.Unity.Utils;

namespace BallLauncher.Core
{
    public class App : MonoBehaviour
    {
        private const int TARGET_FRAMERATE = 30;

        void Awake()
        {
            YLogger.Debug($"App] * Setting frame rate to {TARGET_FRAMERATE}");

            Application.targetFrameRate = TARGET_FRAMERATE;
        }

        void Start()
        {
            // Game entry point

            YLogger.Debug("Hello, I'm the main App from BallLauncher.Core");

            // then something like in PotatoXY...
            // start cooking pure
            //appFacade = AppFacade.getInstance();
            //appFacade.startup(this);

            // for now...            

            SceneUtils.LoadNextScene();
        }
    }

}