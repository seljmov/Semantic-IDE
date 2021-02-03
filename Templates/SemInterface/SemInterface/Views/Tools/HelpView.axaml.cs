using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SemInterface.Views.Tools
{
    public class HelpView : UserControl
    {
        public HelpView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
