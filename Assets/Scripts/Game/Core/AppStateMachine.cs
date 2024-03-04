using BallLauncher.State;
using Yxp.Debug;
using Yxp.State;

namespace BallLauncher.Core
{
    public class AppStateMachine : StateMachine<AppStateMachine.States>
    {
        public enum States
        {
            Start,
            Gameplay,
            ExitToOS
        }

        private App _app;

        public AppStateMachine(App app)
        {
            _app = app;
        }

        public override States CreateStateInstances()
        {
            _states[States.Start] = new AppStateStart(_app);
            _states[States.Gameplay] = new AppStateGameplay(_app);
            _states[States.ExitToOS] = new AppStateExitToOS(_app);

            return States.Start;
        }

        /*
        public override void Update()
        {
            //YLogger.Verbose($"AppStateMachine] Update) _current state is {_currentState}");

            base.Update();
        }
        */
    }
}
