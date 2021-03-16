using System.Collections.Generic;
using System.Linq;

namespace Semantic.Solution
{
    public class Comment : SingleLineOperator
    {
        public Comment()
        {
            TextBody = _items.OfType<TextBody>().First();
        }
        
        public TextBody TextBody { get; private set; }
        public SemanticOperator ReplacedOperator { get; set; }
        
        public override string ItemName => SyntaxNames.Comment;

        public override void CheckControlFlow()
        {
            if (Next != null)
            {
                Next.CheckControlFlow();
            }
        }

        internal override void Scan(bool scanNext)
        {
            for (var i = 0; i < _errors.Count; ++i)
            {
                SemanticFind find = _errors[i];
                if (find.Operator == this)
                {
                    _errors.Remove(find);
                    --i;
                }
            }

            base.Scan(scanNext);
        }
    }
}