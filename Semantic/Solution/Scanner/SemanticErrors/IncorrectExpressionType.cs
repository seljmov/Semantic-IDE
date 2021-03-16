using System.Collections.Generic;

namespace Semantic.Solution
{
    public class IncorrectExpressionType : SemanticError
    {
        public IncorrectExpressionType(SemanticOperator @operator, List<Word> runs, string formalType, string actualType)
            : base(@operator, runs)
        {
            if (formalType == SyntaxNames.Boolean)
            {
                Text = string.Format(Strings.BooleanExpressionExceptedGot, actualType);
            }
            else if (formalType == SyntaxNames.Integer)
            {
                Text = string.Format(Strings.IntegerExpressionExpectedGot, actualType);
            }
            else if (formalType == Strings.Subprogram)
            {
                Text = string.Format(Strings.CallExpectedGot, actualType);
            }
        }
    }
}