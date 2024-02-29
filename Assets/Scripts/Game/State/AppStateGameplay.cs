using BallLauncher.Core;
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

        public AppStateMachine.States GetNextState()
        {
            return StateId;
        }

        public void Update()
        {
        }

        public void Exit()
        {
        }
    }
}
