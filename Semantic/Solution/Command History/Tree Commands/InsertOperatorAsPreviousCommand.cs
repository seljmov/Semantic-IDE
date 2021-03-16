namespace Semantic.Solution
{
    public class InsertOperatorAsPreviousCommand : Command
    {
        public InsertOperatorAsPreviousCommand(SemanticOperator insertedOperator, SemanticOperator existedOperator)
        {
            InsertedOperator = insertedOperator;
            ExistedOperator = existedOperator;
        }
        
        public SemanticOperator InsertedOperator { get; private set; }
        public SemanticOperator ExistedOperator { get; private set; }
        
        internal override void Execute()
        {
            if (ExistedOperator.Previous != null)
            {
                ExistedOperator.Previous.Next = InsertedOperator;
                InsertedOperator.Previous = ExistedOperator.Previous;
                ExistedOperator.Previous = InsertedOperator;
                InsertedOperator.Next = ExistedOperator;
            }
            else
            {
                InsertedOperator.Next = ExistedOperator.Parent?.Child;
                ExistedOperator.Parent.Child = InsertedOperator;
                ExistedOperator.Previous = InsertedOperator;
                InsertedOperator.Parent = ExistedOperator.Parent;
                ExistedOperator.Parent = null;
            }
            InsertedOperator.SetParent(ExistedOperator.FindParent());
            InsertedOperator.Tree.NotifyObservers(this);
        }

        internal override void Undo()
            => new DeleteOperatorCommand(InsertedOperator).Execute();
    }
}