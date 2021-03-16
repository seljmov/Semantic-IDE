using System.Collections.Generic;

namespace Semantic.Solution
{
    public class AmbiguousModule : SemanticFind
    {
        public AmbiguousModule(SemanticOperator @operator, List<Word> items, string module) 
            : base(@operator, items)
        {
            Text = string.Format(Strings.ModuleWithNameHasDeclared, module, @operator.Tree.Project.Name);
        }
    }
}