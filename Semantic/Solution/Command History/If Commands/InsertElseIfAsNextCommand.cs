namespace Semantic.Solution
{
    public class InsertElseIfAsNextCommand : Command
    {
        internal InsertElseIfAsNextCommand(If iif, ElseIf newElseIf, ElseIf existedElseIf)
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
            if (this.ExistedElseIf.Next != null)
            {
                this.InsertedElseIf.Next = this.ExistedElseIf.Next;
                this.ExistedElseIf.Next.Previous = this.InsertedElseIf;
            }
            this.ExistedElseIf.Next = this.InsertedElseIf;
            this.InsertedElseIf.Previous = this.ExistedElseIf;

            this.If.ElseIfList.Insert(this.If.ElseIfList.IndexOf(this.ExistedElseIf) + 1, this.InsertedElseIf);
            this.InsertedElseIf.Parent = this.If;
            this.If.NotifyObservers(this);
        }

        internal override void Undo()
        {
            new DeleteElseIfCommand(this.If, this.InsertedElseIf).Execute();
        }
    }
}