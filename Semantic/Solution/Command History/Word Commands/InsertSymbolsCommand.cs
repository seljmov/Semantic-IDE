namespace Semantic.Solution
{
    public class InsertSymbolsCommand : Command
    {
        public InsertSymbolsCommand(Word word, int position, string symbols)
        {
            Word = word;
            Position = position;
            Symbols = symbols;
        }
        
        public Word Word { get; private set; }
        public int Position { get; private set; }
        public string Symbols { get; private set; }
        
        internal override void Execute()
        {
            Word.SetText(Word.Text.Insert(Position, Symbols));
            
            if (Word._parent is IEndNameable && Word.HasType(EItemType.Name))
            {
                ((IEndNameable) Word.Operator).EndNameWord.SetText(Word.Text);
            }
            
            Word.NotifyObservers(this);
            Word.ManageLinks();
            SemanticCursor.Instance.Focus(Word);
            SemanticCursor.Instance.SetOffset(Position + Symbols.Length);
        }

        internal override void Undo()
            => new DeleteSymbolsCommand(Word, Position, Symbols.Length).Execute();
    }
}