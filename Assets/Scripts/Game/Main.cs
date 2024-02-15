using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yxp.Debug;
using Yxp.Unity.Utils;

namespace BallLauncher.Core
{
    public class Main : MonoBehaviour
    {
        private const int TARGET_FRAMERATE = 30;

        void Awake()
        {
            Application.targetFrameRate = TARGET_FRAMERATE;
        }

        void Start()
        {
            // Game entry point
            YLogger.Debug("Hello, I'm Unity's Main from BallLauncher.Core");

            // then something like in PotatoXY...
            // start cooking pure
            //appFacade = AppFacade.getInstance();
            //appFacade.startup(this);

            // for now...            
            YLogger.Debug("I will load the next scene in the build settings...");

            SceneUtils.LoadNextScene();
        }
    }

}
