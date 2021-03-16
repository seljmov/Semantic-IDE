using System.Collections.Generic;

namespace Semantic.Solution
{
    public class UnfinishedClassUsage : SemanticError
    {
        public UnfinishedClassUsage(SemanticOperator @operator, List<Word> items) 
            : base(@operator, items)
        {
            Text = Strings.YouCantUseUnfinishedClassInDeepCopyMode;
        }
    }
}