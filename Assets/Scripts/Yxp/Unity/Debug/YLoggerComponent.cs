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

    [AddComponentMenu("Yxp/Debug/YLogger")]
    public class YLoggerComponent : MonoBehaviour
    {
        [Header("Live Settings")]
        [SerializeField] bool _showLogs;
        [SerializeField] YLogLevel _logLevel;
        [SerializeField] bool _showTimestamp;

        [Header("Development Settings")]
        [SerializeField] YLoggerComponentSettings _prodSettings;
        [SerializeField] bool _generateDebugLogs;

        private YLoggerComponentSettings _currentSettings;

        UnityLogger _unityLogger;

        void OnEnable()
        {
            //UnityEngine.Debug.Log($"({Time.time}) YLoggerComponent OnEnable)");

            InitializeCurrentSettings();

            if (_unityLogger == null)
            {
                //UnityEngine.Debug.Log("Creating UnityLogger instance /+++++++");

                _unityLogger = new UnityLogger();                
            }

            YLogger.UseLogger(_unityLogger);

            ApplyLiveSettings();
        }

        void OnDisable()
        {
            //UnityEngine.Debug.Log($"({Time.time}) YLoggerComponent OnDisable)");

            _showLogs = false;
            
            YLogger.Enabled = _showLogs;
        }

        void InitializeCurrentSettings()
        {
#if UNITY_EDITOR
            InitializeDevSettings();
#else
            InitializeProdSettings();
#endif
        }

        void InitializeDevSettings()
        {
            // just use serialized settings of this component
            _currentSettings = null;            
        }

        void InitializeProdSettings()
        {
            if (_prodSettings != null)
            {
                _currentSettings = _prodSettings;
            }
            else
            {
                UnityEngine.Debug.LogError($"Missing YLogger production settings. Using fallback.");
                
                _currentSettings = GetFallbackSettings();
            }

            // Override serialized editor values in production, use "scriptable object" settings.
            _showLogs = _currentSettings.ShowLogs;
            _logLevel = _currentSettings.LogLevel;
            _generateDebugLogs = _currentSettings.GenerateDebugLogs;
            _showTimestamp = _currentSettings.DecorateWithTimestamp;
        }

        YLoggerComponentSettings GetFallbackSettings()
        {
           return new YLoggerComponentSettings
            {
                ShowLogs = false,
                GenerateDebugLogs = false,
                DecorateWithTimestamp = false
            };
        }

        void ApplyLiveSettings()
        {
            YLogger.Enabled = _showLogs;
            YLogger.LogLevel = _logLevel;
            YLogger.SetDecorateWithTimestamp(_showTimestamp);
        }

#if UNITY_EDITOR
        void OnValidate()
        {
            ApplyLiveSettings();
        }
#endif

        void Update()
        {
#if UNITY_EDITOR
            ProcessDebugLogs();
#endif
        }

#if UNITY_EDITOR
        void ProcessDebugLogs()
        {
            if (_generateDebugLogs == true && _currentSettings != null)
            {
                YLogger.Debug(_currentSettings.DebugLogMessage);
                YLogger.Warning(_currentSettings.WarningLogMessage);
                YLogger.Error(_currentSettings.ErrorLogMessage);
            }
        }
#endif

    }

}