using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yxp.Debug;

namespace Yxp.Unity.Debug
{
    [CreateAssetMenu(fileName = "YLoggerComponentData", menuName = "Yxp/YLogger Settings", order = 1)]
    public class YLoggerComponentData : ScriptableObject
    {
        public bool showLogs;
        public YLogLevel logLevel;
        public bool generateDebugLogs;

        public string debugLogMessage;
        public string warningLogMessage;
        public string errorLogMessage;
    }

}
