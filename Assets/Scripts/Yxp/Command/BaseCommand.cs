using System;

namespace Yxp.Command
{
    public abstract class BaseCommand : ICommand
    {
        protected bool _finishedExecuting = false;

        public bool FinishedExecuting { get { return _finishedExecuting; } protected set { _finishedExecuting = value; } }

        protected BaseCommand()
        {
            FinishedExecuting = false;
        }

        public virtual void Execute()
        {
            FinishedExecuting = false;
        }

        protected void OnFinishedExecuting()
        {
            FinishedExecuting = true;
        }

        public virtual void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
