using System.Collections.Generic;

namespace Semantic.Solution
{
    public class MultipleDeclaration : SemanticError
    {
        public MultipleDeclaration(SemanticOperator @operator, List<Word> items, string name) 
            : base(@operator, items)
        {
            Name = name;
            Text = string.Format(Strings.NameIsDeclaredMoreThanOnce, Name);
        }
        
        // TODO: А нужно ли?
        public string Name { get; set; }
    }
}