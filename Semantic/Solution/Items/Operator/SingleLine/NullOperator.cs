using System.Collections.Generic;
using System.Linq;

namespace Semantic.Solution
{
    public class NullOperator : SingleLineOperator
    {
        public NullOperator()
        {
            KeyWord = _items.OfType<Word>().First();
        }

        public NullOperator(string keyWord)
        {
            KeyWord = _items.OfType<Word>().First();
            KeyWord.SetText(keyWord);
        }

        public Word KeyWord { get; private set; }

        public override string ItemName => SyntaxNames.EmptyOperator;

        internal override void Scan(bool scanNext) => throw new System.NotImplementedException();

        // public override void CheckControlFlow() { }
    }
}