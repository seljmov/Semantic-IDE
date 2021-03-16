using System.Collections.Generic;
using System.Linq;

namespace Semantic.Solution
{
    public class Root : MultiLineOperator
    {

        public override string ItemName => "корневой оператор";

        internal override void Scan(bool scanNext) => throw new System.NotImplementedException();

        public override void CalculateNumber()
        {
            _counter = 0;
            if (Child != null)
            {
                Child.CalculateNumber();
            }

            _counter = 0;
        }
    }
}