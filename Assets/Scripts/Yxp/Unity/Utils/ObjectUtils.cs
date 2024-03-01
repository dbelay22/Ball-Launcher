using UnityEngine;

namespace Yxp.Unity.Utils
{
    public static class ObjectUtils
    {
        public static T[] FastFindObjectsOfType<T>() where T : Object
        {
            return Object.FindObjectsByType<T>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        }
    }
}
