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

        // - Обработчик изменения языка
        public void OnLanguageChanged(object sender, SelectionChangedEventArgs args)
        {
            // - Берем язык с ComboBox и грузим его
            var box = sender as ComboBox;
            var language = box?.SelectedIndex switch
            {
                1 => "en-EN",
                _ => "ru-RU",
            };
            Semantic.Scripts.Localizer.GetInstance().LoadLanguage(language);
        }
    }
}