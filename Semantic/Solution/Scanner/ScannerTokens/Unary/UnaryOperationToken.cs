using System.Collections.Generic;

namespace Semantic.Solution
{
    public abstract class UnaryOperationToken : ScanToken
    {
        protected UnaryOperationToken(Token token, ScanToken operand) 
            : base(token)
        {
            Operand = operand;
            Operand.Parent = this;
        }
        
        public ISemanticType OperandType { get; private set; }
        public ScanToken Operand { get; private set; }

        protected internal override List<Word> Words => Operand.Words;
        public override ISemanticType GetSemanticType()
        {
            OperandType = Operand.GetSemanticType();
            if (OperationIsCorrect())
            {
                return ReturnType();
            }

            if (!OperandType.Equals(SimpleType.Undefined))
            {
                GenerateFind(new NotApplicableOperatorTypes(Token.Operator, Token, OperandType.FullType));
            }
            
            return SimpleType.Undefined;
        }

        protected abstract ISemanticType ReturnType();
        protected abstract bool OperationIsCorrect();
    }
}