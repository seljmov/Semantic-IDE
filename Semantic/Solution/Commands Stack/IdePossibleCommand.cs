namespace Semantic.Solution
{
    public abstract class IdePossibleCommand : IdeCommand
    {
        public string CantBeExecutedMessage { get; protected set; }

        public override IdeCommandResult TryExecute()
        {
            if (CanBeExecuted())
            {
                var result = Execute();

                if (this is IModificationCommand command)
                {
                    command.Rescan();
                    SemanticCursor.Instance.CurrentTree.SaveChanges();
                }

                return result;
            }

            return new IdeCommandResult(false) {Message = CantBeExecutedMessage};
        }

        protected abstract bool CanBeExecuted();
    }
}