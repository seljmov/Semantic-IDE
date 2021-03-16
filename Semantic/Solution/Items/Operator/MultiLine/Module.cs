using System.Collections.Generic;
using System.Linq;

namespace Semantic.Solution
{
    public class Module : MultiLineOperator, IEndNameable
    {
        private static readonly List<SemanticOperator> _members = new List<SemanticOperator>();

        public Module()
        {

        }

        public Word NameWord { get; private set; }
        public Word EndNameWord { get; private set; }
        public Beginning Beginning { get; internal set; }
        public HashSet<Word> Usages { get; private set; }

        public override string ItemName => SyntaxNames.Module;

        internal void SetBeginning(Beginning beginning) { }

        internal override void Scan(bool scanNext) => throw new System.NotImplementedException();

        // public override List<string> ToOpsStrings() => null;

        public override void CalculateNumber() => base.CalculateNumber();

        public List<Record> GetDeclaredTypes() => null;

        public List<Import> GetImports() => null;

        public List<SemanticOperator> GetMembers() => null;

        public override string PrettyPrinter(EPrettyPrinterMode mode = EPrettyPrinterMode.WithBody, bool printNext = false, int tabLevel = 0)
            => base.PrettyPrinter(mode, printNext, tabLevel);

        // public override Ge
    }
}