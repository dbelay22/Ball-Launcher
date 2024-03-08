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


        #region abstract methods
        
        // Should return the starting state (enum)
        public abstract EState CreateStateInstances();

        #endregion

        public virtual void Initialize(TContext context)
        {
            _context = context;
        }

        #region IStateMachine

        public virtual void Start()
        {
            EState startingState = CreateStateInstances();

            _currentState = _states[startingState];

            _currentState.OnEnter();
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

        public void Stop()
        {
            _currentState.OnExit();
        }

        #endregion


        private void TransitionToState(EState nextStateId)
        {
            _isTransitioningState = true;

            _currentState.OnExit();

            _currentState = _states[nextStateId];

            _currentState.OnEnter();

            _isTransitioningState = false;
        }

        
    }
}