using System.Collections.Generic;

namespace Semantic.Solution
{
    public class IncorrectSuperClass : SemanticError
    {
        public IncorrectSuperClass(SemanticOperator @operator, List<Word> items) 
            : base(@operator, items)
        {
            Text = Strings.InheritanceMustBeFromUserClass;
        }
    }
}