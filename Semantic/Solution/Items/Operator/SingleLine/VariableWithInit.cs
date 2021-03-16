using System.Collections.Generic;
using System.Linq;

namespace Semantic.Solution
{
    public class VariableWithInit : SingleLineOperator, INameable, IHaveExpression, IHaveType
    {
        public VariableWithInit()
        {
            Expression = (Expression)_items.First(x => x is Expression);
            TypeWord = _items.First(x => HasType(EItemType.Type));
            NameWord = (Word)_items.First(x => HasType(EItemType.Name));
            Usages = new HashSet<Word>();
        }

        public Expression Expression { get; private set; }
        public SemanticItem TypeWord { get; set; }
        public Word NameWord { get; private set; }
        public HashSet<Word> Usages { get; private set; }

        public override string ItemName => SyntaxNames.InitializedVariable;
        public override string Signature => "";

        internal override void Scan(bool scanNext) => throw new System.NotImplementedException();
    }
}