namespace Semantic.Solution
{
    public abstract class SemanticEditableContainer : SemanticObservableContainer
    {
        protected SemanticEditableContainer(SemanticItem item)
            : base(item) { }

        internal abstract void AddItem(SemanticItem item);
        internal abstract void InsertItem(int index, SemanticItem item);
        internal abstract void RemoveItem(SemanticItem item);
    }
}