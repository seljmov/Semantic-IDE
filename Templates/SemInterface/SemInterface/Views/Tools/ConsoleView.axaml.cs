using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SemInterface.Views.Tools
{
    public class ConsoleView : UserControl
    {
        public ConsoleView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
