namespace Semantic.Solution
{
    public class IsToken : BinaryOperationToken
    {
        public IsToken(Token token, ScanToken leftOperand, ScanToken rightOperand) 
            : base(token, leftOperand, rightOperand) { }

        protected override ISemanticType ReturnType() => SimpleType.Boolean;

        protected override bool OperationIsCorrect()
            => (LeftType is PointerType || LeftType is SimpleType) && RightType.IsUserType;
    }
}