using System;
using System.Collections.Generic;
using System.Linq;

namespace Semantic.Solution
{
    public class ClassParameter : SemanticObservableContainer, IHaveType, INameable
    {
        public ClassParameter(SemanticItem item)
            : base(item)
        {

        }

        public SemanticItem TypeWord { get; set; }
        public Word NameWord { get; private set; }
        public HashSet<Word> Usages { get; private set; }
    }
}