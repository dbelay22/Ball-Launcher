using System;

namespace Yxp.State
{
    public interface IStateMachine
    {
        public void Start();
        public void Update();
        public void Stop();
    }
}
