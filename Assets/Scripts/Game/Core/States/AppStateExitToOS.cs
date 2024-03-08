using UnityEngine;
using Yxp.State;

namespace BallLauncher.Core.States
{
    public class AppStateExitToOS : IState<AppStateMachine.States>
    {
        private App _app;

        public AppStateMachine.States StateId
        {
            get { return AppStateMachine.States.ExitToOS; }
        }

        public AppStateExitToOS(App app)
        {
            _app = app;
        }

        public void OnEnter()
        {
            Application.Quit();
        }

        public void Update()
        {           
        }

        public AppStateMachine.States GetNextState()
        {
            return StateId;
        }

        public void OnExit()
        {
        }
    }
}
