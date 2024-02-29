
namespace Yxp.Command
{
    public interface ICommand
    {
        public bool FinishedExecuting { get; }
        
        public void Execute();
        
        public void Undo();
    }
}
