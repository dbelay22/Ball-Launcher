using BallLauncher.State;
using UnityEngine;

namespace BallLauncher.Core
{
    public class App : MonoBehaviour
    {
        [SerializeField]
        private AppSettings _settings;
        public AppSettings Settings { get { return _settings; } }

        AppStateMachine _appStateMachine;

        void Awake()
        {
            _appStateMachine = new AppStateMachine(this);            
        }

        void Start()
        {
            _appStateMachine.Start();
        }

        void Update()
        {
            _appStateMachine.Update();
        }
    }

}
