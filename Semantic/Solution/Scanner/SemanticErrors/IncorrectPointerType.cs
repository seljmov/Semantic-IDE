using System.Collections.Generic;

namespace Semantic.Solution
{
    public class IncorrectPointerType : SemanticError
    {
        public IncorrectPointerType(SemanticOperator @operator, List<Word> items) 
            : base(@operator, items)
        {
            Text = Strings.PointerTypeCantBeOnlyUserType;
        }
    }
}