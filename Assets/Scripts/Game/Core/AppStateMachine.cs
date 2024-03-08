using BallLauncher.State;
using Yxp.Debug;
using Yxp.Unity.State;
using Yxp.State;

namespace BallLauncher.Core
{
    public class AppStateMachine : BaseStateMachine<AppStateMachine.States, App>
    {
        public enum States
        {
            Start,
            Gameplay,
            ExitToOS
        }

        public override States CreateStateInstances()
        {
            _states[States.Start] = new AppStateStart(_context);
            _states[States.Gameplay] = new AppStateGameplay(_context);
            _states[States.ExitToOS] = new AppStateExitToOS(_context);

            return States.Start;
        }

        public override void Update()
        {
            YLogger.Verbose($"AppStateMachine] Update) _current state is {_currentState}");

            base.Update();
        }
    }
}
