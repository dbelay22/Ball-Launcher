using UnityEngine;
using Yxp.Debug;

namespace Ypx.Unity.Debug
{
    [AddComponentMenu("Yxp/Debug/Logger")]
    public class YLoggerComponent : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField]
        bool _enabled;

        void Awake()
        {
            YLogger.SetEnabled(_enabled);
        }

        void Update()
        {
            YLogger.SetEnabled(_enabled);
        }
    }

}