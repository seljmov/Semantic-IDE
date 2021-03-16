using System.Collections.Generic;

namespace Semantic.Solution
{
    public class ValueUsedAsInput : SemanticError
    {
        public ValueUsedAsInput(SemanticOperator @operator, List<Word> items) 
            : base(@operator, items)
        {
            Text = Strings.YouCantInputToValueUseVariable;
        }
    }
}