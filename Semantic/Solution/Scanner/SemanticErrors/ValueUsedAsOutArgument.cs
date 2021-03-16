using System.Collections.Generic;

namespace Semantic.Solution
{
    public class ValueUsedAsOutArgument : SemanticError
    {
        public ValueUsedAsOutArgument(SemanticOperator @operator, List<Word> items) 
            : base(@operator, items)
        {
            Text = Strings.YouCantPassValueByReferenceUseVariable;
        }
    }
}