namespace Semantic.Solution
{
    public class AndToken : BinaryOperationToken
    {
        public AndToken(Token token, ScanToken leftOperand, ScanToken rightOperand) 
            : base(token, leftOperand, rightOperand) { }

        protected override ISemanticType ReturnType() => SimpleType.Boolean;

        protected override bool OperationIsCorrect()
            => LeftType.Equals(SimpleType.Boolean) && RightType.Equals(SimpleType.Boolean);
    }
}