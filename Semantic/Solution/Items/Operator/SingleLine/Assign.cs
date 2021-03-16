using System.Collections.Generic;
using System.Linq;

namespace Semantic.Solution
{
    public class Assign : SingleLineOperator
    {
        public Assign()
        {
            RightValue = (Expression)_items.First(x => x is Expression);
            LeftValue = (Expression)_items.Last(x => x is Expression);
        }

        /// <summary>
        ///     Переменная, к которой присваивается значение.
        ///     Assign _RightValue_ := LeftValue;
        /// </summary>
        public Expression RightValue { get; private set; }
        /// <summary>
        ///     Значение, которое присваивается к переменной.
        ///     Assign RightValue := _LeftValue_;
        /// </summary>
        public Expression LeftValue { get; private set; }

        public override string ItemName => SyntaxNames.Assign;

        internal override void Scan(bool scanNext) => throw new System.NotImplementedException();
    }
}