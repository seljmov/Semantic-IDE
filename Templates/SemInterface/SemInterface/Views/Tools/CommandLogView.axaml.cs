using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SemInterface.Views.Tools
{
    public class CommandLogView : UserControl
    {
        public CommandLogView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
