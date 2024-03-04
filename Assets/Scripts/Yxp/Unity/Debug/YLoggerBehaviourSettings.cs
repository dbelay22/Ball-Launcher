using UnityEngine;
using Yxp.Debug;

namespace Yxp.Unity.Debug
{
    [CreateAssetMenu(fileName = "YLoggerBehaviourSettings", menuName = "Yxp/YLogger Behaviour Settings", order = 1)]
    public class YLoggerBehaviourSettings : ScriptableObject
    {
        public bool ShowLogs;        
        public YLogLevel LogLevel;
        public bool DecorateWithTimestamp;
    }

}
