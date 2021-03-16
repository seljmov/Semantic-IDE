using System;
using System.Collections.Generic;

namespace Semantic.Solution
{
    public class IncorrectReturnType : SemanticError
    {
        public IncorrectReturnType(SemanticOperator @operator, List<Word> items, 
            string name, string formalType, string actualType) 
            : base(@operator, items)
        {
            Text = string.Format(Strings.FunctionReturnsIncorrectType, name, formalType, actualType);
        }
    }
}