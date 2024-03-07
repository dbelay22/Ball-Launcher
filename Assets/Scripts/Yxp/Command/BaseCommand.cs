using System;

namespace Yxp.Command
{
    public abstract class BaseCommand : ICommand
    {
        protected bool _finishedExecuting = false;

        public bool FinishedExecuting { get { return _finishedExecuting; } }

        protected BaseCommand()
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
