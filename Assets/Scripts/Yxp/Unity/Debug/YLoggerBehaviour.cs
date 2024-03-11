using System;
using UnityEngine;
using Yxp.Debug;
using Yxp.Unity.Debug;

namespace Ypx.Unity.Debug
{
    /*
    IMPORTANT:  When bootstraping a new project you need to change the script execution order of this component
                to run before the default time, -200 should work.
                Refer to "Project Settings" / "Script Execution Order".
                This allows you to have YLogger available in early Awake/OnEnable calls.
     */

    [AddComponentMenu("Yxp/Debug/YLogger Behaviour")]
    public class YLoggerBehaviour : MonoBehaviour
    {
        [Header("Live Dev Settings")]
        [SerializeField] YLoggerSettings _liveSettings;

        [SerializeField] bool _generateDebugLogs;
        [SerializeField] string _debugLogMessage;
        [SerializeField] string _warningLogMessage;
        [SerializeField] string _errorLogMessage;

        [Header("Production")]
        [SerializeField] YLoggerBehaviourData _productionBehaviourData;
        
        private YLoggerController _controller;

        void OnEnable()
        {
            bool isUnityEditor = false;

#if UNITY_EDITOR
            UnityEngine.Debug.Log($"({Time.time}) YLoggerBehaviour OnEnable)");
            isUnityEditor = true;
#endif            

            InitializeController(isUnityEditor);
        }        

        void InitializeController(bool isUnityEditor)
        {
            YLoggerSettings settings = GetInitialSettings(isUnityEditor);

            _controller = new YLoggerController(new UnityLogger(), settings);

            InitializeLiveSettings(settings);
        }

        YLoggerSettings GetInitialSettings(bool isUnityEditor)
        {
            if (isUnityEditor)
            {
                // use behaviour serialized values
                return _liveSettings;
            }
            else
            {
                if (_productionBehaviourData != null)
                {
                    return _productionBehaviourData.settings;
                }
                else
                {
                    UnityEngine.Debug.LogError($"Missing YLogger production settings. Using fallback.");
                    return new YLoggerSettings(false, YLogLevel.Warning, false);
                }
            }
        }

        void InitializeLiveSettings(YLoggerSettings settings)
        {
            _liveSettings = settings;
        }

        void OnLiveSettingsChange()
        {
            _controller.ApplySettings(_liveSettings);
        }

        void OnDisable()
        {
            UnityEngine.Debug.Log($"({Time.time}) YLoggerComponent OnDisable)");

            _liveSettings.Enabled = false;

            OnLiveSettingsChange();
        }


#if UNITY_EDITOR
        void OnValidate()
        {
            if (_controller != null)
            {
                OnLiveSettingsChange();
            }            
        }

        void ProcessDebugLogs()
        {
            if (_generateDebugLogs)
            {
                YLogger.Debug(_debugLogMessage);
                YLogger.Warning(_warningLogMessage);
                YLogger.Error(_errorLogMessage);
            }
        }
#endif

        void Update()
        {
#if UNITY_EDITOR
            ProcessDebugLogs();
#endif
        }

    }

}