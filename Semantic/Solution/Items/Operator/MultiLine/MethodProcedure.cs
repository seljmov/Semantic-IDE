using System.Collections.Generic;
using System.Linq;

namespace Semantic.Solution
{
    public class MethodProcedure : BaseFunction, IMethod, IField
    {
        public MethodProcedure()
        {
            VisibilityWord = (Word)_items.First(x => HasType(EItemType.VisibilityModifier));
            ClassParameter = _items.OfType<ClassParameter>().First();
            NameWord = (Word)_items.First(x => HasType(EItemType.Name));
            EndNameWord = (Word)_items.First(x => HasType(EItemType.EndName));
            ParametersWord = _items.OfType<Parameters>().First();
            Usages = new HashSet<Word>();
        }

        public override string ItemName => SyntaxNames.MethodProcedure;

        public Word VisibilityWord { get; private set; }

        public ClassParameter ClassParameter { get; private set; }

        internal override void Scan(bool scanNext) => base.Scan(scanNext);
    }
}