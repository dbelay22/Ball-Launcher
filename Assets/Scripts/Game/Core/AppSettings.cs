using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BallLauncher.Core
{
    [CreateAssetMenu(fileName = "AppSettings", menuName = "Game/App Settings", order = 1)]
    public class AppSettings : ScriptableObject
    {
        public int TargetFramerate;
    }
}
