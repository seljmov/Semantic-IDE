using System.Collections.Generic;
using System.Linq;

namespace Semantic.Solution
{
    public class DeleteTokenCommand : Command
    {
        private readonly int _index;
        private readonly List<SemanticItem> _items;

        public DeleteTokenCommand(List<SemanticItem> items, Token deletedToken)
        {
            DeletedToken = deletedToken;
            _items = items;
            _index = _items.IndexOf(deletedToken) - 1;
        }

        public Token DeletedToken { get; private set; }

        internal override void Execute()
        {
            DeletedToken.ManageLinks();
            _items.Remove(DeletedToken);
            DeletedToken.Expression.NotifyObservers(this);
        }

        internal override void Undo()
            => new InsertTokenAsNextCommand(_items, DeletedToken, (Token)_items.ElementAtOrDefault(_index)).Execute();
    }
}