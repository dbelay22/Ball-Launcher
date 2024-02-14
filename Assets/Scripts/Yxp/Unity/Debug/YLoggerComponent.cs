using UnityEngine;
using Yxp.Debug;

namespace Ypx.Unity.Debug
{
    [AddComponentMenu("Yxp/Debug/Logger")]
    public class YLoggerComponent : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField]
        bool _showLogs;

        void Awake()
        {
            YLogger.SetEnabled(_showLogs);
        }

        void Update()
        {
            YLogger.SetEnabled(_showLogs);
        }
    }

}