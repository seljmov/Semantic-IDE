using System.Collections.Generic;

namespace Semantic.Solution
{
    public interface IToken
    {
        string Text { get; }
        ETokenType TokenType { get; }
        List<Word> Words { get; }
        ConstantTypes ConstantTypes { get; set; }
    }
}