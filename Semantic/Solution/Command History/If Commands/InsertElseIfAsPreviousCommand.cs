namespace Semantic.Solution
{
    public class InsertElseIfAsPreviousCommand : Command
    {
        internal InsertElseIfAsPreviousCommand(If iif, ElseIf newElseIf, ElseIf existedElseIf)
        {
            this.If = iif;
            this.InsertedElseIf = newElseIf;
            this.ExistedElseIf = existedElseIf;
        }

        public If If { get; private set; }
        public ElseIf InsertedElseIf { get; private set; }
        public ElseIf ExistedElseIf { get; private set; }

        internal override void Execute()
        {
            if (this.ExistedElseIf.Previous != null)
            {
                this.InsertedElseIf.Previous = this.ExistedElseIf.Previous;
                this.ExistedElseIf.Previous.Next = this.InsertedElseIf;
            }
            this.ExistedElseIf.Previous = this.InsertedElseIf;
            this.InsertedElseIf.Next = this.ExistedElseIf;

            this.If.ElseIfList.Insert(this.If.ElseIfList.IndexOf(this.ExistedElseIf), this.InsertedElseIf);
            this.InsertedElseIf.Parent = this.If;
            this.If.NotifyObservers(this);
        }

        internal override void Undo()
        {
            new DeleteElseIfCommand(this.If, this.InsertedElseIf).Execute();
        }
    }
}