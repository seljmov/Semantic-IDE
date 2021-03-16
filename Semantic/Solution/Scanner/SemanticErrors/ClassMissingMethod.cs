using System.Collections.Generic;

namespace Semantic.Solution
{
    public class ClassMissingMethod : MissingDeclarationError
    {
        public ClassMissingMethod(SemanticOperator @operator, List<Word> items, SemanticOperator missingOperator, string member) 
            : base(@operator, items, missingOperator, member)
        {
            Text = string.Format(Strings.MethodIsNotDeclaredInClass, member, OperatorName);
        }
    }
}