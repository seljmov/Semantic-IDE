using System.Collections.Generic;

namespace Semantic.Solution
{
    public class InsertTokenAsNextCommand : Command
    {
        private readonly List<SemanticItem> _items;

        public InsertTokenAsNextCommand(List<SemanticItem> items, Token insertedToken, Token existedToken)
        {
            _items = items;
            InsertedToken = insertedToken;
            ExistedToken = existedToken;
        }

        public Token InsertedToken { get; private set; }
        public Token ExistedToken { get; private set; }

        internal override void Execute()
        {
            if (ExistedToken != null)
            {
                _items.Insert(_items.IndexOf(ExistedToken) + 1, InsertedToken);
            }
            else
            {
                _items.Insert(0, InsertedToken);
            }

            InsertedToken.Expression.NotifyObservers(this);
        }

        internal override void Undo() 
            => new DeleteTokenCommand(_items, InsertedToken).Execute();
    }
}