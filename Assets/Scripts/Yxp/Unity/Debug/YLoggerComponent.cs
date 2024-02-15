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
        [Header("Settings")]
        [SerializeField] bool _showLogs;
        [SerializeField] YLogLevel _logLevel;
        [SerializeField] bool _showTimestamp;

        [Header("Development")]
        [SerializeField] bool _generateDebugLogs;
        [SerializeField] YLoggerComponentSettings _devSettings;

        void Awake()
        {
            UnityEngine.Debug.Log($"({Time.time}) YLoggerComponent Awake");

            if (_devSettings == null)
            {
                UnityEngine.Debug.LogError("Missing YLogger development settings");
                return;
            }            

            YLogger.UseLogger(new UnityLogger());

            // use settings from scriptable object
            _showLogs = _devSettings.showLogs;
            _logLevel = _devSettings.logLevel;
            _generateDebugLogs = _devSettings.generateDebugLogs;
            _showTimestamp = _devSettings.decorateWithTimestamp;

            ApplySettings();
        }

        void ApplySettings()
        {
            YLogger.SetEnabled(_showLogs);
            YLogger.SetLogLevel(_logLevel);
            YLogger.SetDecorateWithTimestamp(_showTimestamp);
        }

#if UNITY_EDITOR
        void OnValidate()
        {
            ApplySettings();
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