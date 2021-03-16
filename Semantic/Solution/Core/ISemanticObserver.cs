namespace Semantic.Solution
{
    public interface ISemanticObserver
    {
        void Update(Command command);
        void Update(EObserverHint hint);
    }
}