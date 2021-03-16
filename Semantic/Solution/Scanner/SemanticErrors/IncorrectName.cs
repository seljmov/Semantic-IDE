using System.Collections.Generic;

namespace Semantic.Solution
{
    public class IncorrectName : SemanticError
    {
        public IncorrectName(SemanticOperator @operator, List<Word> items, bool isKeyword = false) 
            : base(@operator, items)
        {
            Text = isKeyword ? Strings.IdentifierConcursWithKeyword : Strings.IncorrectName;
        }
    }
}