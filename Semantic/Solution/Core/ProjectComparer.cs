using System;
using System.Collections.Generic;

namespace Semantic.Solution
{
    public class ProjectComparer : IComparer<IProjectItem>
    {
        public int Compare(IProjectItem x, IProjectItem y)
        {
            if (x is Folder && y is SemanticTree)
            {
                return -1;
            }

            if (x is SemanticTree && y is Folder)
            {
                return 1;
            }

            return string.CompareOrdinal(x.Name, y.Name);
        }
    }
}