using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semantic.Solution
{
    public abstract class SimpleItem : SemanticItem
    {
        protected SimpleItem(SemanticItem parent) 
            : base(parent) { }

        public override SemanticItem GetNextItem(SemanticItem currentItem)
        {
            return null;
        }

        public override SemanticItem GetPreviousItem(SemanticItem currentItem)
        {
            return null;
        }
    }
}
