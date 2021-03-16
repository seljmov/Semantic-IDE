using System.Collections.Generic;

namespace Semantic.Solution
{
    public class UninitializedVariableUsage : SemanticError
    {
        public UninitializedVariableUsage(SemanticOperator @operator, List<Word> items, string name) 
            : base(@operator, items)
        {
            Name = name;
            Text = string.Format(Strings.VariableIsNotInitialized, Name);
        }
        
        public string Name { get; set; }
    }
}