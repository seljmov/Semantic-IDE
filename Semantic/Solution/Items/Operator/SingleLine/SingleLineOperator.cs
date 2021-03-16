namespace Semantic.Solution
{
    public abstract class SingleLineOperator : SemanticOperator
    {
        public override string PrettyPrinter(EPrettyPrinterMode mode = EPrettyPrinterMode.WithBody, bool printNext = false, int tabLevel = 0)
        {
            string output = "";

            for (var i = 0; i < tabLevel; ++i)
            {
                output += SystemNames.Tab;
            }

            foreach (SemanticItem item in _items)
            {
                output += item.PrettyPrinter();
            }

            string nextText = "";
            if (printNext && Next != null)
            {
                nextText = "\n" + Next.PrettyPrinter(mode, true, tabLevel);
            }

            return (output + nextText);
        }
    }
}