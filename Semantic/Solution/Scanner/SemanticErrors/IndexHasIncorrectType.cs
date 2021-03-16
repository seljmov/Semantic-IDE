using System.Collections.Generic;

namespace Semantic.Solution
{
    public class IndexHasIncorrectType : SemanticError
    {
        public IndexHasIncorrectType(SemanticOperator @operator, List<Word> items,
            string formalType, string actualType) 
            : base(@operator, items)
        {
            FormalType = formalType;
            ActualType = actualType;

            Text = string.Format(Strings.IndexHasIncorrectType, FormalType, ActualType);
        }
        
        public string FormalType { get; set; }
        public string ActualType { get; set; }
    }
}