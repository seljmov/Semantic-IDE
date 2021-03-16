using System.Collections.Generic;

namespace Semantic.Solution
{
    public class UndeclaredArray : UndeclaredObject
    {
        public UndeclaredArray(SemanticOperator @operator, List<Word> items, string name) 
            : base(@operator, items, name)
        {
            Text = string.IsNullOrEmpty(Name)
                    ? Strings.InputArrayName
                    : string.Format(Strings.ArrayIsNotDeclared, Name);
        }
    }
}