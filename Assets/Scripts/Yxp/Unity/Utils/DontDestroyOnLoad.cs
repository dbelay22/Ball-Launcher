using UnityEngine;

namespace Yxp.Unity.Utils
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

    }
}

