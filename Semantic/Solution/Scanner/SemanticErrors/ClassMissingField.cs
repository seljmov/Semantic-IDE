using System.Collections.Generic;

namespace Semantic.Solution
{
    public class ClassMissingField : MissingDeclarationError
    {
        public ClassMissingField(SemanticOperator @operator, List<Word> items, SemanticOperator missingOperator, string member) 
            : base(@operator, items, missingOperator, member)
        {
            Text = string.Format(Strings.FieldIsNotDeclaredInClass, member, OperatorName);
        }
    }
}