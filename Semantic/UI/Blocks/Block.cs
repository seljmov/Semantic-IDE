using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;


namespace Semantic.UI
{
    public abstract class Block : DockPanel
    {
        internal static bool BlockIsDrag;
        internal static bool BlockIsGoodPlace;
    }
}
