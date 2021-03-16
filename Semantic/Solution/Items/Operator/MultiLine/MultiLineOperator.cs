using System.Linq;

namespace Semantic.Solution
{
    public abstract class MultiLineOperator : SemanticOperator
    {
        public bool IsCollapsed { get; set; }

        public override string PrettyPrinter(EPrettyPrinterMode mode = EPrettyPrinterMode.WithBody, bool printNext = false, int tabLevel = 0)
        {
            SemanticItem bodyItem = _items.LastOrDefault(x => HasType(EItemType.Body)) ?? _items.Last();

            string firstLine = "";
            for (var i = 0; i < tabLevel; ++i)
            {
                firstLine += SystemNames.Tab;
            }

            for (var i = 0; i < _items.Count && _items[i] != bodyItem; ++i)
            {
                firstLine += _items[i].PrettyPrinter();
            }

            firstLine += bodyItem.PrettyPrinter();

            string childText = "";
            if (Child != null)
            {
                childText = "\n" + Child.PrettyPrinter(mode, true, tabLevel + 1);
            }

            string lastLine = "";
            if (bodyItem != _items.Last())
            {
                lastLine += "\n";
                for (var i = 0; i < tabLevel; ++i)
                {
                    lastLine += SystemNames.Tab;
                }

                var index = _items.IndexOf(bodyItem);
                for (var i = index + 1; i < _items.Count; ++i)
                {
                    lastLine += _items[i].PrettyPrinter();
                }
            }

            string nextLine = "";
            if (printNext && Next != null)
            {
                nextLine = "\n" + Next.PrettyPrinter(mode, true, tabLevel);
            }

            return mode switch
            {
                EPrettyPrinterMode.FirstLine => firstLine,
                EPrettyPrinterMode.LastLine => lastLine,
                EPrettyPrinterMode.WithoutBody => firstLine + lastLine,
                EPrettyPrinterMode.WithBody => firstLine + childText + lastLine + nextLine,
                _ => null,
            };
        }

        public SemanticItem GetBodyItem()
        {
            for (var i = ItemsCount - 1; i >= 0; --i)
            {
                SemanticItem item = _items[i];
                if (HasType(EItemType.Body))
                {
                    return item;
                }
            }

            return _items.Last();
        }
    }
}