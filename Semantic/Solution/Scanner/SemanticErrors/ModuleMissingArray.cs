using System.Collections.Generic;

namespace Semantic.Solution
{
    public class ModuleMissingArray : MissingDeclarationError
    {
        public ModuleMissingArray(SemanticOperator @operator, List<Word> items, 
            SemanticOperator missingOperator, string member) 
            : base(@operator, items, missingOperator, member)
        {
            Text = string.Format(Strings.ArrayIsNotDeclaredInModule, member, OperatorName);
        }
    }
}