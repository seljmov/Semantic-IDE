using System.Collections.Generic;
using System.Linq;

namespace Semantic.Solution
{
    public class Function : BaseFunction, IHaveVisibility
    {
        public Function()
        {
            TypeWord = _items.First(x => HasType(EItemType.Type));
            NameWord = (Word)_items.First(x => HasType(EItemType.Name));
            EndNameWord = (Word)_items.First(x => HasType(EItemType.EndName));
            ParametersWord = _items.OfType<Parameters>().First();
            VisibilityWord = (Word)_items.First(x => HasType(EItemType.VisibilityModifier));
            Usages = new HashSet<Word>();
        }

        public override string ItemName => SyntaxNames.Function;

        public Word VisibilityWord { get; private set; }

        internal override void Scan(bool scanNext) => base.Scan(scanNext);
    }
}