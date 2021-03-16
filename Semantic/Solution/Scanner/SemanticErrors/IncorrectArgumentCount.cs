using System.Collections.Generic;
using System.Linq;

namespace Semantic.Solution
{
    public class IncorrectArgumentCount : SemanticError
    {
        public IncorrectArgumentCount(SemanticOperator @operator, List<Word> items, int actualCount, int formalCount) 
            : base(@operator, items)
        {
            Function = items.Last();
            ActualCount = actualCount;
            FormalCount = formalCount;

            Text = string.Format(Strings.FunctionTakesIncorrectParametersCount, Function.Text, FormalCount, ActualCount);
        }
        
        public Word Function { get; set; }
        public int ActualCount { get; set; }
        public int FormalCount { get; set; }
    }
}