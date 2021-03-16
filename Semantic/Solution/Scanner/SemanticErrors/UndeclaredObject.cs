using System.Collections.Generic;

namespace Semantic.Solution
{
    public abstract class UndeclaredObject : SemanticError
    {
        protected UndeclaredObject(SemanticOperator @operator, List<Word> items, string name) 
            : base(@operator, items)
        {
            Name = name;
        }
        
        public string Name { get; set; }
    }
}