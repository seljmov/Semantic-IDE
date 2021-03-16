namespace Semantic.Solution
{
    public class SetBeginningCommand : Command
    {
        public SetBeginningCommand(Module module, Beginning beginning)
        {
            Module = module;
            Beginning = beginning;
        }

        public Module Module { get; private set; }
        public Beginning Beginning { get; private set; }
        
        internal override void Execute()
        {
            Module.Beginning = Beginning;
            Beginning.Parent = Module;
            Module.NotifyObservers(this);
        }

        internal override void Undo()
            => new DeleteBeginningCommand(Module).Execute();
    }
}