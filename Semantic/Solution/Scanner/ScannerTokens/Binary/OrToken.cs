namespace Semantic.Solution
{
    public class OrToken : BinaryOperationToken
    {
        public OrToken(Token token, ScanToken leftOperand, ScanToken rightOperand) 
            : base(token, leftOperand, rightOperand) { }

        protected override ISemanticType ReturnType() => SimpleType.Boolean;

        protected override bool OperationIsCorrect()
            => LeftType.Equals(SimpleType.Boolean) && RightType.Equals(SimpleType.Boolean);
    }
}