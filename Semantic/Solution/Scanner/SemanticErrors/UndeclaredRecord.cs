using System.Collections.Generic;

namespace Semantic.Solution
{
    public class UndeclaredRecord : UndeclaredObject
    {
        public UndeclaredRecord(SemanticOperator @operator, List<Word> items, string name) 
            : base(@operator, items, name)
        {
            Text = string.IsNullOrEmpty(Name)
                ? Strings.InputTypeName
                : string.Format(Strings.TypeIsNotDeclared, Name);
        }
    }
}