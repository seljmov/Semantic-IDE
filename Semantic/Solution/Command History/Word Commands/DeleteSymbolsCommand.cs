namespace Semantic.Solution
{
    public class DeleteSymbolsCommand : Command
    {
        private readonly string _symbols;

        public DeleteSymbolsCommand(Word word, int position, int count)
        {
            Word = word;
            Position = position;
            Count = count;
            _symbols = Count >= 0
                ? Word.Text.Substring(Position, Count)
                : Word.Text.Substring(Position + Count, -Count);
        }
        
        public Word Word { get; private set; }
        public int Position { get; private set; }
        public int Count { get; private set; }
        
        internal override void Execute()
        {
            Word.SetText(Count >= 0
                ? Word.Text.Remove(Position, Count)
                : Word.Text.Remove(Position + Count, -Count));

            if (Word._parent is IEndNameable && Word.HasType(EItemType.Name))
            {
                ((IEndNameable) Word.Operator).EndNameWord.SetText(Word.Text);
            }
            
            Word.NotifyObservers(this);
            Word.ManageLinks();
            SemanticCursor.Instance.Focus(Word);
            SemanticCursor.Instance.SetOffset(Count >= 0 ? Position : Position + Count);
        }

        internal override void Undo()
        {
            var pos = Count >= 0 ? Position : Position + Count;
            new InsertSymbolsCommand(Word, pos, _symbols).Execute();
        }
    }
}