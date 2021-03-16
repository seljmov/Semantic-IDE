using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semantic.Solution
{
    public class Do : MultiLineOperator
    {
        public Do()
        {
            Expression = (Expression)_items.First(x => x is Expression);
        }

        public Expression Expression { get; private set; }

        public override string ItemName => SyntaxNames.While;

        internal override void Scan(bool scanNext) => throw new NotImplementedException();
    }
}