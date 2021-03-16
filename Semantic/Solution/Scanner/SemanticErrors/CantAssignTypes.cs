using System.Collections.Generic;

namespace Semantic.Solution
{
    public class CantAssignTypes : SemanticError
    {
        public CantAssignTypes(SemanticOperator @operator, List<Word> items, string formalType, string actualType) 
            : base(@operator, items)
        {
            Text = string.Format(Strings.CantAssignTypes, actualType, formalType);
        }
    }
}