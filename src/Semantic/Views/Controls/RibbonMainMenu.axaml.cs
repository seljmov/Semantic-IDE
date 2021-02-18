using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Semantic.Views.Controls
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

        public void OnLanguageChanged(object sender, SelectionChangedEventArgs args)
        {
            var box = sender as ComboBox;
            var language = box?.SelectedIndex switch
            {
                1 => "en-EN",
                _ => "ru-RU",
            };
            Semantic.Scripts.Localizer.Instance.LoadLanguage(language);
        }
    }
}