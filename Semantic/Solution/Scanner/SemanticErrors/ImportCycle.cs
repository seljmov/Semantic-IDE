using System.Collections.Generic;

namespace Semantic.Solution
{
    public class ImportCycle : SemanticError
    {
        public ImportCycle(SemanticOperator @operator, List<Word> items) 
            : base(@operator, items)
        {
            Text = Strings.CycleModuleImport;
        }
    }
}