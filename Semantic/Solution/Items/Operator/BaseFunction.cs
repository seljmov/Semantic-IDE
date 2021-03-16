using System.Collections.Generic;
using System.Linq;

namespace Semantic.Solution
{
    public abstract class BaseFunction : Subprogram
    {
        public SemanticItem TypeWord { get; set; }

        internal override void Scan(bool scanNext)
        {
            base.Scan(scanNext);
            // CheckControlFlow();
        }

        // public override void CheckControlFlow() { }
    }
}