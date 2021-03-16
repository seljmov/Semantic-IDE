using System.Collections.Generic;

namespace Semantic.Solution
{
    public class FixedSizeInOpenArray : SemanticError
    {
        public FixedSizeInOpenArray(SemanticOperator @operator, List<Word> items) 
            : base(@operator, items)
        {
            Text = Strings.ArraySizeMustBeOpen;
        }
    }
}