namespace Semantic.Solution
{
    public class IntegerDivideToken : BinaryOperationToken
    {
        public IntegerDivideToken(Token token, ScanToken leftOperand, ScanToken rightOperand) 
            : base(token, leftOperand, rightOperand) { }

        protected override ISemanticType ReturnType()
        {
            if (LeftOperand is OperandToken 
                && LeftOperand.Token.ConstantTypes.Equals(ConstantTypes.Int)
                && RightOperand is OperandToken 
                && RightOperand.Token.ConstantTypes.Equals(ConstantTypes.Real))
            {
                GenerateFind(new IntegerConstantsDivision(Token.Operator, Words));
            }

            return LeftType;
        }

        protected override bool OperationIsCorrect()
            => LeftType.Equals(RightType) && LeftType.Equals(SimpleType.Integer);
    }
}