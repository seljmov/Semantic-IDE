using System.Collections.Generic;

namespace Semantic.Solution
{
    public class AccessToPrivateMember : MissingDeclarationError
    {
        public AccessToPrivateMember(SemanticOperator @operator, List<Word> items, 
            SemanticOperator missingOperator, string member, string className) 
            : base(@operator, items, missingOperator, member)
        {
            Text = string.Format(Strings.AccessToPrivateMember, Member, className);
        }
    }
}