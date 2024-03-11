using UnityEngine;
using Yxp.Debug;

namespace Yxp.Unity.Debug
{
    [CreateAssetMenu(fileName = "YLoggerBehaviourData", menuName = "Yxp/YLogger Behaviour Data", order = 1)]
    public class YLoggerBehaviourData : ScriptableObject
    {
        public YLoggerSettings settings;
    }

}
