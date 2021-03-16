using System.Collections.Generic;
using System.Linq;

namespace Semantic.Solution
{
    public class Beginning : MultiLineOperator
    {
        public override string ItemName => SyntaxNames.Beginning;

        internal override void Scan(bool scanNext) { }

        public override SemanticOperator FindParent() => Parent;
    }
}