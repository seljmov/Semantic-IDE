using System.Collections.Generic;

namespace Semantic.Solution
{
    public class ObjectIsNotSubprogram : UndeclaredObject
    {
        public ObjectIsNotSubprogram(SemanticOperator @operator, List<Word> items, string name) 
            : base(@operator, items, name)
        {
            Text = string.Format(Strings.ObjectIsNotSubprogram, Name);
        }
    }
}