using System.Collections.Generic;

namespace Semantic.Solution
{
    public class ClassMissingArray : MissingDeclarationError
    {
        public ClassMissingArray(SemanticOperator @operator, List<Word> items, SemanticOperator missingOperator, string member) 
            : base(@operator, items, missingOperator, member)
        {
            Text = string.Format(Strings.ArrayIsNotDeclaredInClass, member, OperatorName);
        }
    }
}