using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semantic.Solution
{
    public abstract class SemanticFind : IComparable
    {
        public SemanticFind(SemanticOperator @operator, List<Word> items)
        {
            Operator = @operator;
            Words = items;
            if (Operator.Number == 0)
            {
                Operator.Tree.Root.CalculateNumber();
            }

            Line = Operator.Number;
        }

        public string Text { get; set; }
        public List<Word> Words { get; set; }
        public int Line { get; private set; }
        public SemanticOperator Operator { get; set; }

        public string ModuleName => Operator.Tree.Name;

        public string ErrorType { get; set; }

        public int CompareTo(object? obj)
        {
            var error = (SemanticFind?)obj;
            return Words.All(x => error.Words.Any(y => y == x)) ? 0 : 1;
        }
    }
}
