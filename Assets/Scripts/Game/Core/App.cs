using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yxp.Debug;
using Yxp.Unity.Utils;

namespace BallLauncher.Core
{
    public class App : MonoBehaviour
    {
        [SerializeField]
        private AppSettings _settings;
        public AppSettings Settings { get { return _settings; } }

        void Awake()
        {
            ApplySettings();
        }

        void ApplySettings()
        {
            if (_settings == null)
            {
                YLogger.Error("App] Settings are missing!");
                return;
            }

            YLogger.Debug($"App] * Setting frame rate to {_settings.TargetFramerate}");
            Application.targetFrameRate = _settings.TargetFramerate;
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

            //SceneUtils.LoadNextScene();
        }
    }

}
