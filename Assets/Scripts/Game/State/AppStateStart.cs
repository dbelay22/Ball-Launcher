using BallLauncher.Commands;
using BallLauncher.Core;
using Yxp.State;
using Yxp.Unity.Command;

namespace BallLauncher.State
{
    public class AppStateStart : IState<AppStateMachine.States>
    {
        private App _app;
        private ApplyAppSettingsCommand _command;

        public AppStateMachine.States StateId
        {
            get { return AppStateMachine.States.Start; }
        }

        public AppStateStart(App app)
        {
            _app = app;
        }

        public void Enter()
        {
            _command = new ApplyAppSettingsCommand(_app.Settings);

            CommandInvoker.Instance.ExecuteCommand(_command);
        }

        public AppStateMachine.States GetNextState()
        {
            if (_command.FinishedExecuting)
            {
                // ready to execute next state
                return AppStateMachine.States.Gameplay;
            }

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
