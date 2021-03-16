namespace Semantic.Solution
{
    public class MinusToken : BinaryOperationToken
    {
        public MinusToken(Token token, ScanToken leftOperand, ScanToken rightOperand) 
            : base(token, leftOperand, rightOperand) { }

        protected override ISemanticType ReturnType() => LeftType;

        protected override bool OperationIsCorrect()
            => LeftType.Equals(RightType) 
               && (LeftType.Equals(SimpleType.Integer) || LeftType.Equals(SimpleType.Real));
    }
}