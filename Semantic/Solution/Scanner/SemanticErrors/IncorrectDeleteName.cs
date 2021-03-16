using System.Collections.Generic;

namespace Semantic.Solution
{
    public class IncorrectDeleteName : SemanticError
    {
        public IncorrectDeleteName(SemanticOperator @operator, List<Word> items) 
            : base(@operator, items)
        {
            Text = string.Format(Strings.IncorrectDeleteNameText, SyntaxNames.Delete);
        }
    }
}