using System.Collections.Generic;

namespace Semantic.Solution
{
    public class IncorrectOutputType : SemanticError
    {
        public IncorrectOutputType(SemanticOperator @operator, List<Word> items, string type) 
            : base(@operator, items)
        {
            if (type == SyntaxNames.Type)
            {
                Text = Strings.ImpossibleToOutputType;
            }
            else if (type == Strings.Subprogram)
            {
                Text = Strings.ImpossibleToOutputProcedure + SyntaxNames.Call;
            }
        }
    }
}