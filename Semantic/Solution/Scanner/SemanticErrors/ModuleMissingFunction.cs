using System.Collections.Generic;

namespace Semantic.Solution
{
    public class ModuleMissingFunction : MissingDeclarationError
    {
        public ModuleMissingFunction(SemanticOperator @operator, List<Word> items, 
            SemanticOperator missingOperator, string member) 
            : base(@operator, items, missingOperator, member)
        {
            Text = string.Format(Strings.SubprogramIsNotDeclaredInModule, member, OperatorName);
        }
    }
}