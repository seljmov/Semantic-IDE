using System.Collections.Generic;
using System.Linq;

namespace Semantic.Solution
{
    public class Call : SingleLineOperator, IHaveExpression
    {
        public Call()
        {
            Expression = (Expression)_items.First(x => x is Expression);
        }

        public Expression Expression { get; private set; }

        public override string ItemName => SyntaxNames.Call;

        internal override void Scan(bool scanNext) => throw new System.NotImplementedException();
    }
}
