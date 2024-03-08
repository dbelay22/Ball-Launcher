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

        void OnEnable()
        {
            InitializeStateMachine();   
        }

        void InitializeStateMachine()
        {
            gameObject.TryGetComponent(out _appStateMachine);

            if (_appStateMachine == null)
            {
                _appStateMachine = gameObject.AddComponent<AppStateMachine>();

                _appStateMachine.Initialize(this);
            }
        }

        void Start()
        {
        }

        void Update()
        {
        }

        void OnDestroy()
        {
            _appStateMachine.Stop();
        }
    }

}
