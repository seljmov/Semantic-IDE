using System.Collections.Generic;

namespace Semantic.Solution
{
    public class NotArray : SemanticError
    {
        public NotArray(SemanticOperator @operator, List<Word> items, string type) 
            : base(@operator, items)
        {
            Text = string.Format(Strings.ObjectIsNotArray, type);
        }
    }
}