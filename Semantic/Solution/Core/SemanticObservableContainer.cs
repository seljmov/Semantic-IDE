using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Semantic.Solution
{
    public abstract class SemanticObservableContainer : SemanticItem, IEnumerable
    {
        protected List<SemanticItem> _items = new();

        protected SemanticObservableContainer(SemanticItem parent)
            : base(parent) { }

        public override List<Word> Words => _items.SelectMany(x => x.Words).ToList();
        public int ItemsCount => _items.Count;

        public SemanticItem this[int index] => _items[index];

        public IEnumerator GetEnumerator() => _items.GetEnumerator();

        internal override void ManageLinks() => Words.ForEach(x => x.ManageLinks());

        public int IndexOfItem(SemanticItem item) => _items.IndexOf(item);

        public SemanticItem FirstItem() => _items.First();
        public SemanticItem LastItem() => _items.Last();

        public override SemanticItem GetPreviousItem(SemanticItem currentItem)
        {
            int currentIndex = _items.IndexOf(currentItem);
            int index = currentIndex != -1 ? currentIndex - 1 : _items.Count - 1;
            return index >= 0 ? _items[index] : null;
        }

        public override SemanticItem GetNextItem(SemanticItem currentItem)
        {
            int currentIndex = _items.IndexOf(currentItem);
            int index = currentIndex != -1 ? currentIndex + 1 : 0;
            return index < _items.Count ? _items[index] : null;
        }

        internal void ChangeTypeInItems(SemanticItem newType)
        {
            int index = _items.IndexOf(_items.First(x => HasType(EItemType.Type)));
            _items.RemoveAt(index);
            _items.Insert(index, newType);
        }

        public override string PrettyPrinter(EPrettyPrinterMode mode = EPrettyPrinterMode.WithBody, 
            bool printNext = false, int tabLevel = 0)
        {
            string output = "";
            foreach (var item in _items)
            {
                output += item.PrettyPrinter();
            }

            return output;
        }
    }
}