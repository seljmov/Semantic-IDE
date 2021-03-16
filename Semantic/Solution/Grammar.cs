using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semantic.Solution
{
    public class Grammar
    {
        private SemanticItem _item;
        private List<SemanticItem> _result;

        public List<SemanticItem> CreateItems(SemanticItem semanticItem)
        {
            _result = new List<SemanticItem>();
            return _result;
        }
    }
}
