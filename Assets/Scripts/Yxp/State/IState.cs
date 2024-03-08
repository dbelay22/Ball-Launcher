using System;

namespace Yxp.State
{
    public interface IState<EState> where EState : Enum
    {
        public EState StateId { get; }        
        
        public void OnEnter();
        
        public void Update();        
        
        public EState GetNextState();
        
        public void OnExit();        
    }
}
