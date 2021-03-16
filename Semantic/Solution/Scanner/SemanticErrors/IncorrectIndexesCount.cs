using System.Collections.Generic;
using System.Linq;

namespace Semantic.Solution
{
    public class IncorrectIndexesCount : SemanticError
    {
        public IncorrectIndexesCount(SemanticOperator @operator, List<Word> items, 
            int formalParamsCount, int actualParamsCount) 
            : base(@operator, items)
        {
            Array = items.Last();
            FormalParamsCount = formalParamsCount;
            ActualParamsCount = actualParamsCount;

            Text = string.Format(Strings.ArrayHasIncorrectIndexesCount, Array.Text, ActualParamsCount, FormalParamsCount);
        }
        
        public Word Array { get; set; }
        public int FormalParamsCount { get; set; }
        public int ActualParamsCount { get; set; }
    }
}