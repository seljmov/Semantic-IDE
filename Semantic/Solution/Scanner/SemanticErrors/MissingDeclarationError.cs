using System.Collections.Generic;

namespace Semantic.Solution
{
    public abstract class MissingDeclarationError : SemanticError
    {
        protected MissingDeclarationError(SemanticOperator @operator, List<Word> items, 
            SemanticOperator missingOperator, string member)
            : base(@operator, items)
        {
            MissingOperator = missingOperator;
            Member = member;
        }
        
        public SemanticOperator MissingOperator { get; set; }
        public string Member { get; set; }

        public string OperatorName => ((INameable) MissingOperator).NameWord.Text;
    }
}