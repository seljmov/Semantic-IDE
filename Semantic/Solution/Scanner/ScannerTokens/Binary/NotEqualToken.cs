namespace Semantic.Solution
{
    public class NotEqualToken : BinaryOperationToken
    {
        public NotEqualToken(Token token, ScanToken leftOperand, ScanToken rightOperand) 
            : base(token, leftOperand, rightOperand) { }

        protected override ISemanticType ReturnType()
        {
            CheckRealEquation();
            return SimpleType.Boolean;
        }

        protected override bool OperationIsCorrect()
            => LeftType.Equals(RightType) 
               || LeftType.Equals(SimpleType.NullPointer) && RightType is PointerType 
               || RightType.Equals(SimpleType.NullPointer) && LeftType is PointerType;
    }
}