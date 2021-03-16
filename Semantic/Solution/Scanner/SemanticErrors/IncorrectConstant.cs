using System.Collections.Generic;

namespace Semantic.Solution
{
    public class IncorrectConstant : SemanticError
    {
        public IncorrectConstant(SemanticOperator @operator, List<Word> items) 
            : base(@operator, items)
        {
            Text = Strings.ConstantMustBeSimpleTypeAndContainContains;
        }
    }
}