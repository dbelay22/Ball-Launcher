using System;
using System.Collections.Generic;
using UnityEngine;
using Yxp.State;

namespace Yxp.Unity.State
{
    public abstract class BaseStateMachine<EState, TContext> : MonoBehaviour, IStateMachine where EState : Enum
    {
        protected Dictionary<EState, IState<EState>> _states = new Dictionary<EState, IState<EState>>();

        protected IState<EState> _currentState;

        protected bool _isTransitioningState = false;

        protected TContext _context;

        public virtual void Initialize(TContext context)
        {
            _context = context;
        }

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

        private void TransitionToState(EState nextStateId)
        {
            _isTransitioningState = true;

            _currentState.Exit();

            _currentState = _states[nextStateId];

            _currentState.Enter();

            _isTransitioningState = false;
        }

        public void Stop()
        {
            _currentState.Exit();
        }
    }
}