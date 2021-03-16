using System.Linq;

namespace Semantic.Solution
{
    public class AddElseIfCommand : Command
    {
        public AddElseIfCommand(If @if, ElseIf elseIf)
        {
            If = @if;
            AddedElseIf = elseIf;
        }

        public If If { get; private set; }
        public ElseIf AddedElseIf { get; private set; }

        internal override void Execute()
        {
            if (If.ElseIfList.Count > 0)
            {
                If.ElseIfList.Last().Next = AddedElseIf;
                AddedElseIf.Previous = If.ElseIfList.Last();
            }

            if (If.Else != null)
            {
                AddedElseIf.Next = If.Else;
                If.Else.Previous = AddedElseIf;
            }

            If.ElseIfList.Add(AddedElseIf);
            AddedElseIf.Parent = If;
            If.NotifyObservers(this);
        }

        internal override void Undo()
        {
            throw new System.NotImplementedException();
        }
    }
}