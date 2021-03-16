using System.Collections.Generic;

namespace Semantic.Solution
{
    public class UnreachableCode : SemanticError
    {
        public UnreachableCode(SemanticOperator @operator, List<Word> items)
            : base(@operator, items)
        {
            Text = Strings.UnreachableCodeDetected;
        }
    }
}