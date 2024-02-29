using System;
using Yxp.Debug;

namespace Yxp.Command
{
    public abstract class YCommand : ICommand
    {
        protected bool _finishedExecuting = false;

        public bool FinishedExecuting { get { return _finishedExecuting; } }

        protected YCommand()
        {
            _finishedExecuting = false;
        }

        public virtual void Execute()
        {
            throw new NotImplementedException();
        }
        
        public virtual void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
