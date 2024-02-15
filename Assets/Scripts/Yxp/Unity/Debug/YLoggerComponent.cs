using UnityEngine;
using Yxp.Debug;
using Yxp.Unity.Debug;

namespace Ypx.Unity.Debug
{
    [AddComponentMenu("Yxp/Debug/YLogger")]
    public class YLoggerComponent : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] bool _showLogs;
        [SerializeField] YLogLevel _logLevel;

        [Header("Development")]
        [SerializeField] bool _generateDebugLogs;
        [SerializeField] YLoggerComponentData _devSettings;

        void Awake()
        {
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

            ApplySettings();
        }

        void ApplySettings()
        {
            YLogger.SetEnabled(_showLogs);
            YLogger.SetLogLevel(_logLevel);
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