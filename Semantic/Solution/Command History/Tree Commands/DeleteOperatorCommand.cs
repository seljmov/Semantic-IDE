namespace Semantic.Solution
{
    public class DeleteOperatorCommand : Command
    {
        private readonly SemanticOperator? _beforeOperator;
        private readonly bool _isChild;

        public DeleteOperatorCommand(SemanticOperator deletedOperator)
        {
            DeletedOperator = deletedOperator;
            _beforeOperator = deletedOperator.Parent ?? deletedOperator.Previous;
            _isChild = deletedOperator.Previous == null;
        }
        
        public SemanticOperator DeletedOperator { get; private set; }
        
        internal override void Execute()
        {
            DeletedOperator.ManageLinks();
            if (DeletedOperator is Comment comment)
            {
                comment.TextBody.NotifyObservers(EObserverHint.CommentSaving);
            }

            if (DeletedOperator.Next != null)
            {
                if (DeletedOperator.Parent != null)
                {
                    DeletedOperator.Parent.Child = DeletedOperator.Next;
                    DeletedOperator.Next.Parent = DeletedOperator.Parent;
                    DeletedOperator.Next.Previous = null;
                    DeletedOperator.Parent = null;
                    DeletedOperator.Next = null;
                }
                else
                {
                    DeletedOperator.Previous.Next = DeletedOperator.Next;
                    DeletedOperator.Next.Previous = DeletedOperator.Previous;
                    DeletedOperator.Next = null;
                    DeletedOperator.Previous = null;
                }
            }
            else
            {
                if (DeletedOperator.Parent != null)
                {
                    DeletedOperator.Parent.Child = null;
                    DeletedOperator.Parent = null;
                }
                else
                {
                    DeletedOperator.Previous.Next = null;
                    DeletedOperator.Previous = null;
                }
            }
            
            DeletedOperator.SetParent(null);
            DeletedOperator.Tree.NotifyObservers(this);
        }

        internal override void Undo()
            => new InsertOperatorAsNextCommand(DeletedOperator, _beforeOperator, _isChild).Execute();
    }
}