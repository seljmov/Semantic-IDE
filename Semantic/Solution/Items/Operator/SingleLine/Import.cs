using System.Collections.Generic;
using System.Linq;

namespace Semantic.Solution
{
    public class Import : SingleLineOperator, INameable
    {
        public Import()
        {
            NameWord = (Word)_items.First(x => HasType(EItemType.Name));
            Usages = new HashSet<Word>();
        }

        public Word NameWord { get; private set; }
        public HashSet<Word> Usages { get; private set; }

        public override string ItemName => SyntaxNames.Import;

        private void CheckImports(Import import, string names) { }

        internal override void Scan(bool scanNext) => throw new System.NotImplementedException();
    }
}