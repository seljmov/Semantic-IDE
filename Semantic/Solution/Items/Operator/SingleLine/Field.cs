using System.Collections.Generic;
using System.Linq;

namespace Semantic.Solution
{
    public class Field : SingleLineOperator, IField, IHaveType
    {
        public Field()
        {
            VisibilityWord = (Word)_items.First(x => HasType(EItemType.VisibilityModifier));
            TypeWord = _items.First(x => HasType(EItemType.Type));
            NameWord = (Word)_items.First(x => HasType(EItemType.Name));
            Usages = new HashSet<Word>();
        }

        public Word VisibilityWord { get; private set; }
        public SemanticItem TypeWord { get; set; }
        public Word NameWord { get; private set; }
        public HashSet<Word> Usages { get; private set; }

        public override string ItemName => SyntaxNames.Field;

        internal override void Scan(bool scanNext) => throw new System.NotImplementedException();
    }
}