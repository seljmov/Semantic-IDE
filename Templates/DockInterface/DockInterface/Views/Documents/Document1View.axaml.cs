using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace DockInterface.Views.Documents
{
    public class Document1View : UserControl
    {
        public Document1View()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
