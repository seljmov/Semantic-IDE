namespace Semantic.Solution
{
    public class InsertOperatorAsNextCommand : Command
    {
        public InsertOperatorAsNextCommand(SemanticOperator insertedOperator, SemanticOperator existedOperator, bool asChild)
        {
            InsertedOperator = insertedOperator;
            ExistedOperator = existedOperator;
            AsChild = asChild;
        }
        
        public SemanticOperator InsertedOperator { get; private set; }
        public SemanticOperator ExistedOperator { get; private set; }
        public bool AsChild { get; private set; }
        
        internal override void Execute()
        {
            if (AsChild)
            {
                if (ExistedOperator.Child != null)
                {
                    InsertedOperator.Next = ExistedOperator.Child;
                    ExistedOperator.Child.Previous = InsertedOperator;
                    ExistedOperator.Child.Parent = null;
                }

                ExistedOperator.Child = InsertedOperator;
                InsertedOperator.Parent = ExistedOperator;
                InsertedOperator.SetParent(ExistedOperator);
            }
            else
            {
                if (ExistedOperator.Next != null)
                {
                    ExistedOperator.Next.Previous = InsertedOperator;
                    InsertedOperator.Next = ExistedOperator.Next;
                }

                ExistedOperator.Next = InsertedOperator;
                InsertedOperator.Previous = ExistedOperator;
                InsertedOperator.SetParent(ExistedOperator.FindParent());
            }
            InsertedOperator.Tree.NotifyObservers(this);
        }

        internal override void Undo()
            => new DeleteOperatorCommand(InsertedOperator).Execute();
    }
}