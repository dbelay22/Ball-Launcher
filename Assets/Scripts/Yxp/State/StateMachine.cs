using System;
using System.Collections.Generic;

namespace Yxp.State
{
    public abstract class StateMachine<EState> where EState : Enum
    {
        protected Dictionary<EState, IState<EState>> _states = new Dictionary<EState, IState<EState>>();

        protected IState<EState> _currentState;

        protected bool _isTransitioningState = false;
        
        
        // Should return the starting state (enum)
        public abstract EState CreateStateInstances();


        public virtual void Start()
        {
            EState startingState = CreateStateInstances();

            _currentState = _states[startingState];
            
            _currentState.Enter();
        }

        public virtual void Update()
        {
            if (_isTransitioningState)
            {
                // wait until new current state is setup
                return;
            }

            EState nextStateId = _currentState.GetNextState();

            if (nextStateId.Equals(_currentState.StateId))
            {
                _currentState.Update();
            }
            else
            {
                TransitionToState(nextStateId);
            }
        }

        protected void TransitionToState(EState nextStateId)
        {
            _isTransitioningState = true;

            _currentState.Exit();
            
            _currentState = _states[nextStateId];

            _currentState.Enter();

            _isTransitioningState = false;
        }

        public virtual void Destroy()
        {
            _currentState.Exit();
        }

    }
}
