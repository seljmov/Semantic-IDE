namespace Semantic.Solution
{
    public class ReplaceOperatorCommand : Command
    {
        public ReplaceOperatorCommand(SemanticOperator insertedOperator, SemanticOperator existedOperator)
        {
            InsertedOperator = insertedOperator;
            ExistedOperator = existedOperator;
        }
        
        public SemanticOperator InsertedOperator { get; private set; }
        public SemanticOperator ExistedOperator { get; private set; }
        
        internal override void Execute()
        {
            ExistedOperator.ManageLinks();
            if (ExistedOperator is Comment comment)
            {
                comment.TextBody.NotifyObservers(EObserverHint.CommentSaving);
            }

            if (ExistedOperator.Child != null)
            {
                ExistedOperator.Child.Parent = InsertedOperator;
                InsertedOperator.Child = ExistedOperator.Child;
                InsertedOperator.Child.SetParent(InsertedOperator, true);
            }

            if (ExistedOperator.Next != null)
            {
                ExistedOperator.Next.Previous = InsertedOperator;
                InsertedOperator.Next = ExistedOperator.Next;
            }

            ExistedOperator.Next = InsertedOperator;
            InsertedOperator.Previous = ExistedOperator;
            InsertedOperator.SetParent(ExistedOperator.FindParent());

            if (ExistedOperator.Parent != null)
            {
                ExistedOperator.Parent.Child = InsertedOperator;
                InsertedOperator.Parent = ExistedOperator.Parent;
                InsertedOperator.Previous = null;
                InsertedOperator.Parent = null;
                InsertedOperator.Next = null;
            }
            else
            {
                ExistedOperator.Previous.Next = InsertedOperator;
                InsertedOperator.Previous = ExistedOperator.Previous;
                ExistedOperator.Next = null;
                ExistedOperator.Previous = null;
            }
            
            ExistedOperator.SetParent(null);
            ExistedOperator.Tree.NotifyObservers(this);
        }

        internal override void Undo()
            => new ReplaceOperatorCommand(ExistedOperator, InsertedOperator).Execute();
    }
}