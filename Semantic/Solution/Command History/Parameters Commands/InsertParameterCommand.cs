using System.Collections.Generic;
using System.Linq;

namespace Semantic.Solution
{
    public class InsertParameterCommand : Command
    {
        private readonly List<SemanticItem> _items;

        public InsertParameterCommand(List<SemanticItem> items, TypeParameter insertedParameter, int index)
        {
            _items = items;
            InsertedParameter = insertedParameter;
            Index = index;
        }

        public TypeParameter InsertedParameter { get; private set; }
        public int Index { get; private set; }
        internal override void Execute()
        {
            var parameter = _items.ElementAtOrDefault(Index - 1);
            if (parameter != null && ((TypeParameter) parameter).IsLast)
            {
                ((TypeParameter) parameter).MakeUnLast();
            }
            
            _items.Insert(Index, InsertedParameter);
            InsertedParameter.Parameters.NotifyObservers(this);
        }

        internal override void Undo()
            => new DeleteParameterCommand(_items, InsertedParameter).Execute();
    }
}