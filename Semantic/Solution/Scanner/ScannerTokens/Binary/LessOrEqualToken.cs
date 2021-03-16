namespace Semantic.Solution
{
    public class LessOrEqualToken : BinaryOperationToken
    {
        public LessOrEqualToken(Token token, ScanToken leftOperand, ScanToken rightOperand) 
            : base(token, leftOperand, rightOperand) { }

        protected override ISemanticType ReturnType() => SimpleType.Boolean;

        protected override bool OperationIsCorrect()
            => LeftType.Equals(RightType) 
               && (LeftType.Equals(SimpleType.Integer) || LeftType.Equals(SimpleType.Real));
    }
}