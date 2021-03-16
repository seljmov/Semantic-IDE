using System.Collections.Generic;

namespace Semantic.Solution
{
    public class NotApplicableOperatorTypes : SemanticError
    {
        public NotApplicableOperatorTypes(SemanticOperator @operator, Token operation, string firstType) 
            : base(@operator, operation.Words)
        {
            Operation = operation;
            Text = string.Format(Strings.OperationIsNotAppliedToType, Operation.Text, firstType);
        }
        
        public NotApplicableOperatorTypes(SemanticOperator @operator, Token operation, 
            string firstType, string secondType) 
            : base(@operator, operation.Words)
        {
            Operation = operation;
            Text = string.Format(Strings.OperationIsNotAppliedToTypes, Operation.Text, firstType, secondType);
        }
        
        public Token Operation { get; set; }
    }
}