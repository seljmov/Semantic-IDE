using System.Linq;

namespace Semantic.Solution
{
    public class DeleteElseCommand : Command
    {
        public DeleteElseCommand(If @if, Else existedElse)
        {
            If = @if;
            DeletedElse = existedElse;
        }

        public If If { get; private set; }
        public Else DeletedElse { get; private set; }

        internal override void Execute()
        {
            DeletedElse.ManageLinks();
            if (If.ElseIfList.Count > 0)
            {
                If.ElseIfList.Last().Next = null;
            }

            if (If.Else != null)
            {
                If.Else.Previous = null;
                If.Else.Parent = null;
            }

            If.Else = null;
            If.NotifyObservers(this);
        }

        internal override void Undo()
            => new AddElseCommand(If, DeletedElse).Execute();
    }
}