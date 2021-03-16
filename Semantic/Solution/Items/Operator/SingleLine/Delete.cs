using System.Collections.Generic;
using System.Linq;

namespace Semantic.Solution
{
    public class Delete : SingleLineOperator, IHaveExpression
    {
        public Delete()
        {
            Expression = (Expression)_items.First(x => x is Expression);
        }

        public Expression Expression { get; private set; }

        public override string ItemName => SyntaxNames.Delete;

        internal override void Scan(bool scanNext) => throw new System.NotImplementedException();
    }
}