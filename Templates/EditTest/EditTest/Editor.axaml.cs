using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace EditTest
{
    public class Editor : UserControl
    {
        public Editor()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
