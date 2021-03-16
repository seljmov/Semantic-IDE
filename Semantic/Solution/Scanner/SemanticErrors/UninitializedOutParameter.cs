using System.Collections.Generic;

namespace Semantic.Solution
{
    public class UninitializedOutParameter : SemanticError
    {
        public UninitializedOutParameter(SemanticOperator @operator, List<Word> items, string name) 
            : base(@operator, items)
        {
            Name = name;
            Text = string.Format(Strings.ValueIsAssignedToOutParameter, Name);
        }
        
        public string Name { get; set; }
    }
}