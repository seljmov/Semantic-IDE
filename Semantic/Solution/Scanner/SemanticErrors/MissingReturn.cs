using System.Collections.Generic;

namespace Semantic.Solution
{
    public class MissingReturn : SemanticError
    {
        public MissingReturn(SemanticOperator @operator, List<Word> items, string funcName) 
            : base(@operator, items)
        {
            Text = string.Format(Strings.FunctionMissingReturn, funcName, SyntaxNames.Return);
        }
    }
}