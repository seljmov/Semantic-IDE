namespace Semantic.Solution
{
    public class AsToken : BinaryOperationToken
    {
        public AsToken(Token token, ScanToken leftOperand, ScanToken rightOperand) 
            : base(token, leftOperand, rightOperand) { }

        protected override ISemanticType ReturnType() => RightType;

        protected override bool OperationIsCorrect()
            => (LeftType is PointerType || LeftType is SimpleType) && RightType.IsUserType;
    }
}