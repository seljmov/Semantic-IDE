using System.Linq;

namespace Semantic.Solution
{
    public class Token : Word, IToken
    {
        public Token(Expression expression, string text = "", ETokenType type = ETokenType.Fake)
            : base(expression, text, EItemType.General, true)
        {
            TokenType = type;
            ConstantTypes = ConstantTypes.NonType;
        }

        public ScanToken ScanToken { get; set; }
        public ETokenType TokenType { get; private set; }
        public ConstantTypes ConstantTypes { get; set; }

        public Expression Expression => _parent as Expression;
        public override string ItemName => string.Format(Strings.Token, _text);

        public static Token NewToken(string key, Expression expression)
        {
            Token newToken = null;
            if (SyntaxNames.UnaryOperations.Any(x => x == key))
            {
                newToken = new Token(expression, SyntaxNames.UnaryOperations.First(x => x == key), ETokenType.UnaryOperation);
            }
            else if (SyntaxNames.BinaryOperations.Any(x => x.StartsWith(key)))
            {
                newToken = new Token(expression, SyntaxNames.IsOperation, ETokenType.BinaryOperation);
            }
            else if (key == "(")
            {
                newToken = new Token(expression, key, ETokenType.LeftBracket);
            }
            else if (key == ")")
            {
                newToken = new Token(expression, key, ETokenType.RightBracket);
            }
            else if (key == "[")
            {
                newToken = new Token(expression, key, ETokenType.LeftIndex);
            }
            else if (key == "]")
            {
                newToken = new Token(expression, key, ETokenType.RightIndex);
            }
            else if (key == ".")
            {
                newToken = new Token(expression, key, ETokenType.Dot);
            }
            else if (key == ",")
            {
                newToken = new Token(expression, key, ETokenType.Comma);
            }
            else if (key == SystemNames.SystemFuncDog)
            {
                newToken = new Token(expression, key, ETokenType.SystemSymbol);
            }
            return newToken;
        }

        internal override void Changed() => Expression.Changed();

        public static ETokenType LoadTokenType(string text)
        {
            return text switch
            {
                "Space" => ETokenType.Space,
                "Comma" => ETokenType.Comma,
                "LeftBracket" => ETokenType.LeftBracket,
                "LeftIndex" => ETokenType.LeftIndex,
                "Operand" => ETokenType.Operand,
                "BinaryOperation" => ETokenType.BinaryOperation,
                "UnaryOperation" => ETokenType.UnaryOperation,
                "RightIndex" => ETokenType.RightIndex,
                "Function" => ETokenType.Function,
                "Array" => ETokenType.Array,
                "LeftFuncBracket" => ETokenType.LeftFuncBracket,
                "RightFuncBracket" => ETokenType.RightFuncBracket,
                "Dot" => ETokenType.Dot,
                _ => ETokenType.Operand,
            };
        }

        internal override void CopyFrom(SemanticItem word)
        {
            var tWord = (Token) word;
            SetText(tWord.Text);
            TokenType = tWord.TokenType;
            _itemType = word._itemType;
        }

        internal override SemanticItem Clone() => new Token(null, Text, TokenType);

        public void AddInitializedName(string name)
        {
            if (_initializedNames.Count > 0)
            {
                _initializedNames.First().Add(name);
            }
        }
    }
}