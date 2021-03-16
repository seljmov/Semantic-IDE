using System.Collections.Generic;

namespace Semantic.Solution
{
    public class UndeclaredVariable : UndeclaredObject
    {
        public UndeclaredVariable(SemanticOperator @operator, List<Word> items, string name) 
            : base(@operator, items, name)
        {
            Text = string.IsNullOrEmpty(Name)
                ? Strings.InputName
                : string.Format(Strings.NameIsNotDeclared, Name);
        }
    }
}