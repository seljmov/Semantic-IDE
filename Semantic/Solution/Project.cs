using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semantic.Solution
{
    public class Project
    {
        public string Name { get; private set; }
        public bool IsLoaded { internal get; set; }

        internal void Rescan() { }

        public bool HasModuleWithName(string name) => true;

        public SemanticTree GetModuleByName(string name) => null;
    }
}