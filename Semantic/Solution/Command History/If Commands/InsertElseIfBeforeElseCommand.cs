namespace Semantic.Solution
{
    public class InsertElseIfBeforeElseCommand : Command
    {
        internal InsertElseIfBeforeElseCommand(If iif, ElseIf newElseIf, Else existedElse)
        {
            this.If = iif;
            this.InsertedElseIf = newElseIf;
            this.ExistedElse = existedElse;
        }

        public If If { get; private set; }
        public ElseIf InsertedElseIf { get; private set; }
        public Else ExistedElse { get; private set; }

        internal override void Execute()
        {
            if (this.ExistedElse.Previous != null)
            {
                this.InsertedElseIf.Previous = this.ExistedElse.Previous;
                this.ExistedElse.Previous.Next = this.InsertedElseIf;
            }
            this.ExistedElse.Previous = this.InsertedElseIf;
            this.InsertedElseIf.Next = this.ExistedElse;

            this.If.ElseIfList.Add(this.InsertedElseIf);
            this.ExistedElse.Parent = this.If;
            this.InsertedElseIf.Parent = this.If;
            this.If.NotifyObservers(this);
        }

        internal override void Undo()
        {
            new DeleteElseIfCommand(this.If, this.InsertedElseIf).Execute();
        }
    }
}