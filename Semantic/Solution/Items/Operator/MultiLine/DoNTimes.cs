using System.Collections.Generic;
using System.Linq;

namespace Semantic.Solution
{
    public class DoNTimes : MultiLineOperator
    {
        public DoNTimes()
        {

        }

        public Expression Expression { get; private set; }

        public override string ItemName => SyntaxNames.DoNTimes;

        // TODO: implement this and other operators functions
        internal override void Scan(bool scanNext) { }
    }
}