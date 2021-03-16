using System.Collections.Generic;
using System.Linq;

namespace Semantic.Solution
{
    public interface IScannerObserver
    {
        void ScanCompleted(List<SemanticFind> errors);
        void ScanBegin();
    }
}