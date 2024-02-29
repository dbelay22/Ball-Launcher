using System;

namespace Yxp.State
{
    public interface IState<EState> where EState : Enum
    {
        public EState StateId { get; }        
        public void Enter();
        public void Update();        
        public EState GetNextState();
        public void Exit();        
        
    }
}
