using System;
using System.Collections.Generic;

namespace Semantic.Solution
{
    public class Folder : IProjectItem
    {
        public Folder(string name, bool isSystem)
        {

        }

        public List<IProjectItem> Items { get; private set; }
        public string Name { get; internal set; }
        public bool IsSystem { get; private set; }
        public IProjectItem? Parent { get; set; }

        public void Sort()
        {
            Items.Sort(new ProjectComparer());
            Items.ForEach(x => x.Sort());
        }

        public void AddItem(IProjectItem item)
        {
            item.Parent = this;
            Items.Add(item);
        }
        
        public void RemoveItem(IProjectItem item)
        {
            item.Parent = null;
            Items.Remove(item);
        }

        public IEnumerable<SemanticTree> GetModules(Func<SemanticTree, bool> predicate)
        {
            var modules = new List<SemanticTree>();
            foreach (var item in Items)
            {
                modules.AddRange(item.GetModules(predicate));
            }

            return modules;
        }
    }
}