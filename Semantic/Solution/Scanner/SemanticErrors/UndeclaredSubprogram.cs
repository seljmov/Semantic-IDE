using System.Collections.Generic;

namespace Semantic.Solution
{
    public class UndeclaredSubprogram : UndeclaredObject
    {
        public UndeclaredSubprogram(SemanticOperator @operator, List<Word> items, string name) 
            : base(@operator, items, name)
        {
            Text = string.IsNullOrEmpty(Name)
                ? Strings.InputSubprogramName
                : string.Format(Strings.SubprogramIsNotDeclared, Name);
        }
    }
}