using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yxp.Debug;

namespace Yxp.Unity.Debug
{
    [CreateAssetMenu(fileName = "YLoggerComponentSettings", menuName = "Yxp/YLogger Settings", order = 1)]
    public class YLoggerComponentSettings : ScriptableObject
    {
        public bool ShowLogs;
        
        public YLogLevel LogLevel;

        public bool DecorateWithTimestamp;

        public bool GenerateDebugLogs;

        public string DebugLogMessage;
        public string WarningLogMessage;
        public string ErrorLogMessage;
    }

}
