namespace Semantic.Solution
{
    public class NotToken : UnaryOperationToken
    {
        public NotToken(Token token, ScanToken operand) 
            : base(token, operand) { } 

        protected override ISemanticType ReturnType() => SimpleType.Boolean;

        protected override bool OperationIsCorrect() => OperandType.Equals(SimpleType.Boolean);
    }
}