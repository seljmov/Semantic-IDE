using System.Collections.Generic;

namespace Semantic.Solution
{
    internal class IntegerConstantsDivision : SemanticWarning
    {
        public IntegerConstantsDivision(SemanticOperator @operator, List<Word> words)
            : base(@operator, words)
        {
            this.Text = Strings.UseIntegerDivideOperation;
        }
    }
}