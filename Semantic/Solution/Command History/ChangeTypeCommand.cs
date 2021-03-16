namespace Semantic.Solution
{
    public class ChangeTypeCommand : Command
    {
        public ChangeTypeCommand(SemanticItem item, SemanticItem newType)
        {
            Item = item;
            NewType = newType;
            OldType = ((IHaveType) item).TypeWord;
        }
        
        public SemanticItem Item { get; set; }
        public SemanticItem NewType { get; set; }
        public SemanticItem OldType { get; set; }
        
        internal override void Execute()
        {
            NewType._itemType = OldType._itemType;
            ((IHaveType) Item).TypeWord = NewType;
            NewType._parent = Item;

            if (Item is SemanticObservableContainer semanticItem)
            {
                semanticItem.ChangeTypeInItems(NewType);
            }
            
            Item.NotifyObservers(this);
        }

        internal override void Undo()
            => new ChangeTypeCommand(Item, OldType).Execute();
    }
}