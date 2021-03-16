using System.Collections.Generic;

namespace Semantic.Solution
{
    public class IncorrectDotUsage : UndeclaredObject
    {
        public IncorrectDotUsage(SemanticOperator @operator, List<Word> items, string name) 
            : base(@operator, items, name)
        {
            Text = string.Format(Strings.AccessOperationCantBeUsedToType);
        }
    }
}