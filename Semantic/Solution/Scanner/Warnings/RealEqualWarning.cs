using System.Collections.Generic;

namespace Semantic.Solution
{
    public class RealEqualWarning : SemanticWarning
    {
        public RealEqualWarning(SemanticOperator @operator, List<Word> runs, bool isEqual)
            : base(@operator, runs)
        {
            this.Text = isEqual
                            ? Strings.RealEquationIsNotRecommendedUseFabsLessEpsilon
                            : Strings.RealEquationIsNotRecommendedUseFabsGreaterEpsilon;
        }
    }
}