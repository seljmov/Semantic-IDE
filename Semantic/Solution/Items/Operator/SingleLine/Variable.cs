using System.Collections.Generic;
using System.Linq;

namespace Semantic.Solution
{
    public class Variable : SingleLineOperator, INameable, IHaveType
    {
        public Variable()
        {
            TypeWord = _items.First(x => HasType(EItemType.Type));
            NameWord = (Word)_items.First(x => HasType(EItemType.Name));
            Usages = new HashSet<Word>();
        }

        public SemanticItem TypeWord { get; set; }
        public Word NameWord { get; private set; }
        public HashSet<Word> Usages { get; private set; }

        public override string ItemName => SyntaxNames.Variable;
        public override string Signature => "";

        internal override void Scan(bool scanNext) => throw new System.NotImplementedException();
    }
}