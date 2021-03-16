using System.Collections.Generic;

namespace Semantic.Solution
{
    public class DeleteParameterCommand : Command
    {
        private readonly List<SemanticItem> _items;

        public DeleteParameterCommand(List<SemanticItem> parameters, TypeParameter deletedParameter)
        {
            _items = parameters;
            DeletedParameter = deletedParameter;
            Index = _items.IndexOf(DeletedParameter);
        }
        
        public TypeParameter DeletedParameter { get; private set; }
        public int Index { get; private set; }
        
        internal override void Execute()
        {
            DeletedParameter.ManageLinks();
            if (DeletedParameter.IsLast && Index != 0)
            {
                var previousParameter = (TypeParameter)_items[Index - 1];
                previousParameter.MakeLast();
            }

            _items.Remove(DeletedParameter);
            DeletedParameter.Parameters.NotifyObservers(this);
        }

        internal override void Undo()
            => new InsertParameterCommand(_items, DeletedParameter, Index).Execute();
    }
}