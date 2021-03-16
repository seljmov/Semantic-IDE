namespace Semantic.Solution
{
    public class UnaryMinusToken : UnaryOperationToken
    {
        public UnaryMinusToken(Token token, ScanToken operand) 
            : base(token, operand) { }

        protected override ISemanticType ReturnType() => OperandType;

        protected override bool OperationIsCorrect()
            => OperandType.Equals(SimpleType.Integer) || OperandType.Equals(SimpleType.Real);
    }
}