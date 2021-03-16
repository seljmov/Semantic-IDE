using System.Linq;

namespace Semantic.Solution
{
    public class AddElseCommand : Command
    {
        public AddElseCommand(If @if, Else @else)
        {
            If = @if;
            AddedElse = @else;
        }

        public If If { get; private set; }
        public Else AddedElse { get; private set; }

        internal override void Execute()
        {
            if (If.ElseIfList.Count > 0)
            {
                If.ElseIfList.Last().Next = AddedElse;
                AddedElse.Previous = If.ElseIfList.Last();
            }

            AddedElse.Parent = If;
            If.Else = AddedElse;
            If.NotifyObservers(this);
        }

        internal override void Undo()
            => new DeleteElseCommand(If, AddedElse).Execute();
    }
}