namespace Semantic.Solution
{
    public class DeclareAndInitializeGlobalVariable : ErrorRefactoring, IModificatorCommand
    {
        private SemanticOperator _parent;
        private VariableWithInit _variable;

        public DeclareAndInitializeGlobalVariable(SemanticError error)
        {
            Name = Strings.DeclareAndInitializeGlobalVariable;
            Error = error;
        }

        public void Rescan()
        {
            _variable.DeleteErrors();
            _parent.RescanParent();
        }
        
        protected override IdeCommandResult Execute()
        {
            return null;
        }
    }
}