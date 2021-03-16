using System.Collections.Generic;

namespace Semantic.Solution
{
    public class ValueUsedAsLeftArgument : SemanticError
    {
        public ValueUsedAsLeftArgument(SemanticOperator @operator, List<Word> items) 
            : base(@operator, items)
        {
            Text = Strings.YouCantUseValueToTheLeftOfAssign;
        }
    }
}