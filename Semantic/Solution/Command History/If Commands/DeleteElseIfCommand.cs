namespace Semantic.Solution
{
    public class DeleteElseIfCommand : Command
    {
        private readonly int _indexOfDeletedBranch;

        public DeleteElseIfCommand(If @if, ElseIf existedElseIf)
        {
            If = @if;
            DeletedElseIf = existedElseIf;
            _indexOfDeletedBranch = If.ElseIfList.IndexOf(existedElseIf);
        }

        public If If { get; private set; }
        public ElseIf DeletedElseIf { get; private set; }

        internal override void Execute()
        {
            DeletedElseIf.ManageLinks();
            if (_indexOfDeletedBranch < If.ElseIfList.Count - 1)
            {
                ElseIf elseIf = If.ElseIfList[_indexOfDeletedBranch + 1];
                elseIf.Previous = DeletedElseIf.Previous;
            }

            if (_indexOfDeletedBranch > 0)
            {
                ElseIf elseIf = If.ElseIfList[_indexOfDeletedBranch - 1];
                elseIf.Next = DeletedElseIf.Next;
            }
        }

        internal override void Undo()
        {
            if (If.ElseIfList.Count == _indexOfDeletedBranch)
            {
                new AddElseIfCommand(If, DeletedElseIf).Execute();
            }
            else
            {
                new InsertElseIfAsPreviousCommand(If, DeletedElseIf, If.ElseIfList[_indexOfDeletedBranch]).Execute();
            }
        }
    }
}