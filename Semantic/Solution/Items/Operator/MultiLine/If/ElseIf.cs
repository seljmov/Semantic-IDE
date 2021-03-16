using System.Collections.Generic;
using System.Linq;

namespace Semantic.Solution
{
    public class ElseIf : MultiLineOperator
    {
        public ElseIf()
        {
            Expression = (Expression)_items.First(x => x is Expression);
        }

        public Expression Expression { get; private set; }

        public override string ItemName => SyntaxNames.ElseIf;

        public override SemanticOperator FindParent() => Parent;

        internal override void Scan(bool scanNext)
        {
            for (var i = 0; i < _errors.Count; ++i)
            {
                SemanticFind find = _errors[i];
                if (find.Operator == this)
                {
                    _errors.Remove(find);
                    --i;
                }
            }

            ISemanticType rightType = Expression.GetSemanticType();
            ISemanticType leftType = SimpleType.Boolean;
            if (rightType.CanCastTo(leftType, false))
            {
                GenerateFind(new InCorrectExpressionType(Operator, Expression.Words, leftType.FullType, rightType.FullType));
            }

            Child?.Scan(true);
        }

        public override void CheckControlFlow()
        {
            if (_returnsBranches.Count > 0)
            {
                _returnsBranches.Push(new Branch {SemanticOperator = this, HasReturn = false});
            }

            if (Child != null)
            {
                Child.CheckControlFlow();
            }

            if (_returnsBranches.Count > 0)
            {
                bool hasReturn = _returnsBranches.Pop().HasReturn;
                Branch branch = _returnsBranches.First();
                branch.HasReturn = branch.HasReturn & hasReturn;
            }
        }

        public override void CalculateNumber()
        {
            _counter++;
            Number = _counter;

            if (Child != null)
            {
                Child.CalculateNumber();
            }
        }
    }
}