namespace Semantic.Solution
{
    public abstract class ErrorRefactoring : IdeCommand
    {
        public SemanticError Error { get; protected set; }
    }
}