namespace Semantic.Solution
{
    public class SetTextCommand : Command
    {
        private readonly string _oldText;

        public SetTextCommand(Word word, string newText)
        {
            Word = word;
            _oldText = word.Text;
            NewText = newText;
        }
        
        public Word Word { get; private set; }
        public string NewText { get; private set; }
        
        internal override void Execute()
        {
            Word.SetText(NewText);
            
            if (Word._parent is IEndNameable && Word.HasType(EItemType.Name))
            {
                ((IEndNameable) Word.Operator).EndNameWord.SetText(Word.Text);
            }
            
            Word.NotifyObservers(this);
            Word.ManageLinks();
        }

        internal override void Undo()
            => new SetTextCommand(Word, _oldText).Execute();
    }
}