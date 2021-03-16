using System.Collections.Generic;

namespace Semantic.Solution
{
    public abstract class BinaryOperationToken : ScanToken
    {
        protected BinaryOperationToken(Token token, ScanToken leftOperand, ScanToken rightOperand)
            : base(token)
        {
            LeftOperand = leftOperand;
            RightOperand = rightOperand;

            LeftOperand.Parent = this;
            RightOperand.Parent = this;
        }

        public ScanToken LeftOperand { get; private set; }
        public ScanToken RightOperand { get; private set; }

        public ISemanticType LeftType { get; set; }
        protected ISemanticType RightType { get; private set; }

        protected internal override List<Word> Words
        {
            get
            {
                var words = new List<Word>();
                words.AddRange(LeftOperand.Words);
                words.AddRange(RightOperand.Words);
                words.Add(Token);
                return words;
            }
        }

        protected void CheckRealEquation()
        {
            if (LeftType.Equals(SimpleType.Real) && RightType.Equals(SimpleType.Real))
            {
                GenerateFind(new RealEqualWarning(Token.Operator, Words, this is EqualToken));
            }
        }

        public override ISemanticType GetSemanticType()
        {
            throw new System.NotImplementedException();
        }

        protected abstract ISemanticType ReturnType();
        protected abstract bool OperationIsCorrect();

        protected virtual void ConvertOperands()
        {
            LeftType = LeftOperand.GetSemanticType();
            RightType = RightOperand.GetSemanticType();

            if (LeftType.Equals(SimpleType.Integer) && RightType.Equals(SimpleType.Real))
            {
                LeftType = SimpleType.Real;
            }

            if (LeftType.Equals(SimpleType.Real) && RightType.Equals(SimpleType.Integer))
            {
                RightType = SimpleType.Real;
            }
        }
    }
}