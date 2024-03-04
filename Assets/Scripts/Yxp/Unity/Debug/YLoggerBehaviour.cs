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
        [SerializeField] bool _showLogs = false;
        [SerializeField] YLogLevel _logLevel = YLogLevel.Warning;
        [SerializeField] bool _showTimestamp = false;

        [SerializeField] bool _generateDebugLogs;        
        [SerializeField] string _debugLogMessage;
        [SerializeField] string _warningLogMessage;
        [SerializeField] string _errorLogMessage;

        [Header("Production")]
        [SerializeField] YLoggerBehaviourSettings _prodSettings;
        
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
                return new YLoggerSettings(_showLogs, _logLevel, _showTimestamp);
            }
            else
            {
                if (_prodSettings != null)
                {
                    return new YLoggerSettings(_prodSettings.ShowLogs, _prodSettings.LogLevel, _prodSettings.DecorateWithTimestamp);
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
            _showLogs = settings.Enabled;
            _logLevel = settings.LogLevel;
            _showTimestamp = settings.DecorateWithTimestamp;
        }

        void OnLiveSettingsChange()
        {
            _controller.ApplySettings(new YLoggerSettings(_showLogs, _logLevel, _showTimestamp));
        }

        void OnDisable()
        {
            UnityEngine.Debug.Log($"({Time.time}) YLoggerComponent OnDisable)");

            _showLogs = false;

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