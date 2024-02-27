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
                This allows you to have YLogger available in early Awake calls.
     */

    [AddComponentMenu("Yxp/Debug/YLogger")]
    public class YLoggerComponent : MonoBehaviour
    {
        [Header("Live Settings")]
        [SerializeField] bool _showLogs;
        [SerializeField] YLogLevel _logLevel;
        [SerializeField] bool _showTimestamp;

        [Header("Development Settings")]
        [SerializeField] YLoggerComponentSettings _devSettings;
        [SerializeField] YLoggerComponentSettings _prodSettings;
        [SerializeField] bool _generateDebugLogs;

        private YLoggerComponentSettings _currentSettings;

        UnityLogger _unityLogger;

        void Awake()
        {
            UnityEngine.Debug.Log($"({Time.time}) YLoggerComponent Awake) - Am I enabled ? {this.enabled}");
        }

        void OnEnable()
        {
            UnityEngine.Debug.Log($"({Time.time}) YLoggerComponent OnEnable)");

            InitializeSettings();

            if (_unityLogger == null)
            {
                _unityLogger = new UnityLogger();
                
            }
            YLogger.UseLogger(_unityLogger);

            ApplyLiveSettings();
        }

        void OnDisable()
        {
            UnityEngine.Debug.Log($"({Time.time}) YLoggerComponent OnDisable)");

            _showLogs = false;
            
            ApplyLiveSettings();
        }

        void InitializeSettings()
        {
#if UNITY_EDITOR
            InitializeDevSettings();
#else
            InitializeProdSettings();
#endif

            if (_currentSettings == null)
            {
                UnityEngine.Debug.LogError("Somethig is really wrong, missing settings for YLogger.");
                return;
            }

            // set live settings
            _showLogs = _currentSettings.showLogs;
            _logLevel = _currentSettings.logLevel;
            _generateDebugLogs = _currentSettings.generateDebugLogs;
            _showTimestamp = _currentSettings.decorateWithTimestamp;
        }

        void InitializeDevSettings()
        {
            if (_devSettings == null)
            {
                UnityEngine.Debug.LogWarning($"Missing YLogger development settings, using production.");

                InitializeProdSettings();

                return;
            }

            _currentSettings = _devSettings;
        }

        void InitializeProdSettings()
        {
            if (_prodSettings == null)
            {
                UnityEngine.Debug.LogError($"Missing YLogger production settings. Using fallback.");

                UseFallbackSettings();

                return;
            }

            _currentSettings = _prodSettings;
        }

        void UseFallbackSettings()
        {
            _currentSettings = new YLoggerComponentSettings
            {
                showLogs = false,
                generateDebugLogs = false,
                decorateWithTimestamp = false
            };
        }

        void ApplyLiveSettings()
        {
            YLogger.SetEnabled(_showLogs);
            YLogger.SetLogLevel(_logLevel);
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
            if (_generateDebugLogs)
            {
                YLogger.Debug(_devSettings.debugLogMessage);
                YLogger.Warning(_devSettings.warningLogMessage);
                YLogger.Error(_devSettings.errorLogMessage);
            }
        }
#endif

    }

}