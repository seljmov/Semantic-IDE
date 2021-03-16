using System.Collections.Generic;

namespace Semantic.Solution
{
    public class ModuleMissingVariable : MissingDeclarationError
    {
        public ModuleMissingVariable(SemanticOperator @operator, List<Word> items, 
            SemanticOperator missingOperator, string member) 
            : base(@operator, items, missingOperator, member)
        {
            Text = string.Format(Strings.VariableIsNotDeclaredInModule, member, OperatorName);
        }
    }
}