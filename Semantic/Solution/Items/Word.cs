using System.Collections.Generic;
using Semantic.Language;

namespace Semantic.Solution
{
    public class Word : SimpleItem
    {
        public readonly HashSet<INameable> _declarations = new();
        protected string _text;

        public Word(SemanticItem @operator, string text, EItemType type, bool isEditable = false)
            : base(@operator)
        {
            _itemType = type;
            _text = text;
            IsEditable = isEditable;
        }

        public bool IsEditable { get; private set; }

        public string Text
        {
            get => _text;
            internal set => Tree.AddCommandAndExecute(new SetTextCommand(this, value));
        }

        public override string ItemName => string.Format(Strings.Word, _text);
        public override List<Word> Words => new() {this};
        public override string Signature => Text;

        internal void SetText(string newText)
        {
            _text = newText;
            if (this is Token token)
            {
                token.Expression.BuildTree();
            }
        }
        
        public override string PrettyPrinter(EPrettyPrinterMode mode = EPrettyPrinterMode.WithBody,
            bool printNext = false, int tabLevel = 0)
            => this is Token {TokenType: ETokenType.UnaryOperation} && _text.Equals("minus")
                ? "-"
                : _text;

        internal override void Changed() => _parent.Changed();

        internal override SemanticItem Clone() => new Word(null, Text, _itemType, IsEditable);

        internal override void CopyFrom(SemanticItem item)
        {
            SetText(((Word) item).Text);
            _itemType = item._itemType;
        }

        internal override void Load(string text)
        {
            if (HasType(EItemType.Name) && _parent is IEndNameable item)
            {
                item.EndNameWord.Text = text;
            }
            else if (HasType(EItemType.VisibilityModifier))
            {
                if (Tree.Version.Equals("1.2"))
                {
                    var syntaxString = Dictionaries.Dictionary.GetSyntaxString(text);
                    if (syntaxString != null)
                    {
                        text = syntaxString;
                    }
                }
                else
                {
                    text = text switch
                    {
                        "открытое" => SyntaxNames.Public,
                        "закрытое" => SyntaxNames.Private,
                        "читаемое" => SyntaxNames.Readonly,
                        _ => SyntaxNames.Public,
                    };
                }
            }

            _text = text;
        }

        internal override void ManageLinks()
        {
            foreach (var word in _declarations)
            {
                word.Usages.Remove(this);
            }
            
            _declarations.Clear();
        }

        internal override string Save()
        {
            if (!HasType(EItemType.ModeModifier) && !HasType(EItemType.VisibilityModifier))
            {
                return IsEditable ? Text : null;
            }

            var text = Text;
            if (text.Equals(SyntaxNames.In))
            {
                text = "In";
            }
            else if (text.Equals(SyntaxNames.Out))
            {
                text = "Out";
            }
            else if (text.Equals(SyntaxNames.Ref))
            {
                text = "Ref";
            }
            else if (text.Equals(SyntaxNames.Public))
            {
                text = "Public";
            }
            else if (text.Equals(SyntaxNames.Private))
            {
                text = "Private";
            }
            else if (text.Equals(SyntaxNames.Readonly))
            {
                text = "Readonly";
            }

            return text;

        }

        internal override void Scan(bool scanNext)
        {
            if (HasType(EItemType.Name))
            {
                if (!StringAnalyzer.IsGoodName(Text))
                {
                    GenerateFind(new IncorrectName(Operator, Words));
                }
                
                CheckAmbiguousNames();
            }
        }

        private void CheckAmbiguousNames()
        {
            var ambiguous = _parent.GetNameInSameScope(Text);
            if (ambiguous != null)
            {
                var words = Words;
                words.AddRange(ambiguous.NameWord.Words);
                GenerateFind(new MultipleDeclaration(Operator, words, Text));
            }
        }

        internal void InsertSymbols(int position, string symbols)
            => Tree.AddCommandAndExecute(new InsertSymbolsCommand(this, position, symbols));
        
        internal void DeleteSymbols(int position, int count)
            => Tree.AddCommandAndExecute(new DeleteSymbolsCommand(this, position, count));
    }
}
