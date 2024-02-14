using UnityEngine;

namespace Yxp.Unity.Utils 
{
    // EnableOnAwake
    // Util for game objects that we want to have enabled at play
    // but we want to keep disabled in the editor
    // Most times is useful for game objects that we don't need to see in the scene

    public class EnableOnAwake : MonoBehaviour
    {
        void Awake()
        {
            gameObject.SetActive(true);
        }
    }
}

