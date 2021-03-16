namespace Semantic.Solution
{
    public class ArrayAction : IdeCommand, IModificatorCommand
    {
        private readonly string _newNameText;
        private readonly Word _word;
        private Expression _expression;

        public ArrayAction(Word word, string text)
        {
            Name = text;
            _word = word;
        }

        public ArrayAction(string pid, string text)
        {
            Description = string.Format(Strings.InsertArrayUsingIs, text);
            ItemPid = pid;
            _newNameText = text;
        }
        
        protected override IdeCommandResult Execute()
        {
            var token = (Token)SemanticCursor.Instance.CurrentItem;
            _expression = token.Expression;
            if (token.TokenType == ETokenType.Operand)
            {
                // new Replace
            }

            return null;
        }

        public void Rescan() => _expression.Rescan();
    }
}