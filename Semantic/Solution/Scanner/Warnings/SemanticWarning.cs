using System.Collections.Generic;

namespace Semantic.Solution
{
    public class SemanticWarning : SemanticFind
    {
        public SemanticWarning(SemanticOperator @operator, List<Word> items)
            : base(@operator, items)
        {
            ErrorType = Strings.Warning;
        }
    }
}