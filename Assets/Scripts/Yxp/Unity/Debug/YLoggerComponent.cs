using UnityEngine;
using Yxp.Debug;

namespace Ypx.Unity.Debug
{
    [AddComponentMenu("Yxp/Debug/Logger")]
    public class YLoggerComponent : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] bool _showLogs;
        [SerializeField] YLogLevel _logLevel;

        void Awake()
        {
            ApplySettings();
        }

        void OnValidate()
        {
            ApplySettings();
        }

        void ApplySettings()
        {
            YLogger.SetEnabled(_showLogs);
            YLogger.SetLogLevel(_logLevel);
        }
    }

}