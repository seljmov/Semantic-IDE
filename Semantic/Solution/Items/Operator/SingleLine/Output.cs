using System.Collections.Generic;
using System.Linq;

namespace Semantic.Solution
{
    public class Output : SingleLineOperator, IHaveExpression
    {
        public Output()
        {
            Expression = (Expression)_items.First(x => x is Expression);
        }

        public Expression Expression { get; private set; }

        public override string ItemName => SyntaxNames.Output;

        internal override void Scan(bool scanNext) => throw new System.NotImplementedException();
    }
}
