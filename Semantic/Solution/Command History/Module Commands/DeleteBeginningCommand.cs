namespace Semantic.Solution
{
    public class DeleteBeginningCommand : Command
    {
        public DeleteBeginningCommand(Module module)
        {
            Module = module;
            Beginning = Module.Beginning;
        }
        
        public Module Module { get; private set; }
        public Beginning Beginning { get; private set; }

        internal override void Execute()
        {
            Beginning.ManageLinks();
            Module.Beginning.Parent = null;
            Module.Beginning = null;
            Module.NotifyObservers(this);
        }

        internal override void Undo() 
            => new SetBeginningCommand(Module, Beginning).Execute();
    }
}