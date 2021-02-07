using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Semantic.Views.Tools
{
    public class ErrorsView : UserControl
    {
        public ErrorsView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
