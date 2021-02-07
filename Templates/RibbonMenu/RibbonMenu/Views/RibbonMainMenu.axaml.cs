using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace RibbonMenu.Views
{
    public class RibbonMainMenu : UserControl
    {
        public RibbonMainMenu()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}