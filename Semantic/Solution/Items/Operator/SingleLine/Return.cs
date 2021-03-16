using System.Collections.Generic;
using System.Linq;

namespace Semantic.Solution
{
    public class Return : SingleLineOperator, IHaveExpression
    {
        public Return()
        {
            Expression = (Expression)_items.First(x => x is Expression);
        }

        public Expression Expression { get; private set; }

        public override string ItemName => SyntaxNames.Return;

        internal override void Scan(bool scanNext) => throw new System.NotImplementedException();

        // public override void CheckControlFlow() { }
    }
}