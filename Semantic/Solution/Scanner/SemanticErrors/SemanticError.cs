using System.Collections.Generic;

namespace Semantic.Solution
{
    public abstract class SemanticError : SemanticFind
    {
        protected SemanticError(SemanticOperator @operator, List<Word> items)
            : base(@operator, items)
        {
            ErrorType = Strings.Mistake;
        }
    }
}