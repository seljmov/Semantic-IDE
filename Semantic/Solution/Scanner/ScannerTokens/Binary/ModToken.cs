namespace Semantic.Solution
{
    public class ModToken : BinaryOperationToken
    {
        public ModToken(Token token, ScanToken leftOperand, ScanToken rightOperand) 
            : base(token, leftOperand, rightOperand) { }

        protected override ISemanticType ReturnType() => LeftType;

        protected override bool OperationIsCorrect()
            => LeftType.Equals(RightType) && LeftType.Equals(SimpleType.Integer);
    }
}