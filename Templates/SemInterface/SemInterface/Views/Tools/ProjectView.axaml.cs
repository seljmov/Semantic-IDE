using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SemInterface.Views.Tools
{
    public class ProjectView : UserControl
    {
        public ProjectView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
