using System;
using System.Collections.Generic;

namespace Semantic.Solution
{
    public interface IProjectItem
    {
        string Name { get; }
        bool IsSystem { get; }
        IProjectItem Parent { get; set; }
        
        void Sort();
        void AddItem(IProjectItem item);
        void RemoveItem(IProjectItem item);
        IEnumerable<SemanticTree> GetModules(Func<SemanticTree, bool> predicate);
    }
}