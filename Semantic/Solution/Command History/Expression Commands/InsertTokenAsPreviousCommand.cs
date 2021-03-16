using System.Collections.Generic;

namespace Semantic.Solution
{
    public class InsertTokenAsPreviousCommand : Command
    {
        private readonly List<SemanticItem> _items;

        internal InsertTokenAsPreviousCommand(List<SemanticItem> items, Token insertedToken, Token existedToken)
        {
            _items = items;
            InsertedToken = insertedToken;
            ExistedToken = existedToken;
        }

        public Token InsertedToken { get; private set; }
        public Token ExistedToken { get; private set; }

        internal override void Execute()
        {
            _items.Insert(_items.IndexOf(ExistedToken), InsertedToken);
            InsertedToken.Expression.NotifyObservers(this);
        }

        internal override void Undo()
            => new DeleteTokenCommand(_items, InsertedToken).Execute();
    }
}