using BallLauncher.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yxp.Debug;
using Yxp.State;

namespace BallLauncher.State
{
    public class AppStateGameplay : IState<AppStateMachine.States>
    {
        private App _app;

        public AppStateMachine.States StateId
        {
            get { return AppStateMachine.States.Gameplay; }
        }

        public AppStateGameplay(App app)
        {
            _app = app;
        }

        public void Enter()
        {
            string gameplaySceneName = _app.Settings.GameplayScene.name;

            SceneManager.LoadScene(gameplaySceneName);
        }

        public void Update()
        {
        }
        
        public AppStateMachine.States GetNextState()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                return AppStateMachine.States.ExitToOS;
            }

            return StateId;
        }

        public void Exit()
        {
        }
    }
}
