using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace RibbonMenu.Views
{
    public class RibbonHeaderMenu : UserControl
    {
        public RibbonHeaderMenu()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
