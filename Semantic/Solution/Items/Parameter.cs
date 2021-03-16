using System;
using System.Collections.Generic;
using System.Linq;

namespace Semantic.Solution
{
    public class Parameter : TypeParameter, INameable
    {
        public Parameter(Parameters parameters, string mode, string name, bool isLast)
            : base(parameters, mode, isLast)
        {

        }

        public Word NameWord { get; private set; }
        public HashSet<Word> Usages { get; private set; }
    }
}