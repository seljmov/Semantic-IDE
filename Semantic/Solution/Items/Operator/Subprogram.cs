using System.Collections.Generic;
using System.Linq;

namespace Semantic.Solution
{
    public abstract class Subprogram : MultiLineOperator, IEndNameable
    {
        public Parameters ParametersWord { get; protected set; }

        public override string Signature => base.Signature;

        public Word NameWord { get; protected set; }
        public HashSet<Word> Usages { get; protected set; }
        public Word EndNameWord { get; protected set; }

        internal override void Scan(bool scanNext) => throw new System.NotImplementedException();
    }
}