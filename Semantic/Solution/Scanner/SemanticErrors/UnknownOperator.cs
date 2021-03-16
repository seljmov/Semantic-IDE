using System.Collections.Generic;

namespace Semantic.Solution
{
    public class UnknownOperator : SemanticError
    {
        public UnknownOperator(SemanticOperator @operator, List<Word> items) 
            : base(@operator, items)
        {
            Text = Strings.UnknownOperator;
        }
    }
}