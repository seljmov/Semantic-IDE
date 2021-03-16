using System;

namespace Semantic.Solution
{
    public abstract class IdeCommand
    {
        protected IdeCommand()
        {
            Time = DateTime.Now;
        }

        public string ItemPid { get; protected set; }
        public string Name { get; protected set; }
        public DateTime Time { get; private set; }
        public string Description { get; protected set; }

        public virtual IdeCommandResult TryExecute()
        {
            IdeCommandResult result = Execute();

            if (this is IModificatorCommand command)
            {
                SemanticCursor.Instance.CurrentTree.SaveChanges();
                command.Rescan();
            }

            return result;
        }

        protected abstract IdeCommandResult Execute();

        public virtual bool IsRight(IdeCommand ideCommand)
        {
            return false;
        }
    }
}