using Avalonia;
using Avalonia.Platform;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Newtonsoft.Json;
using Semantic.Models.JsonObjects;
using System.IO;

namespace Semantic.Views.Controls
{
    public class RibbonMainMenu : UserControl
    {
        private readonly string _winDocsPath = @"C:\Users\seljmov\Documents\Semantic IDE 2021\ide_settings.json";
        private readonly string _linuxDocsPath = @"";
        private string _currentDocsPath = @"";
        IdeLanguageModel ideLanguage = new IdeLanguageModel();

        public RibbonMainMenu()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            _currentDocsPath = GetCurrentDocsPath();
            LoadSettingsFromFile();

            this.FindControl<ComboBox>("InterfaceLanguage").SelectedIndex = ideLanguage.InterfaceLanguage == "Русский" ? 0 : 1;
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

        // - Получить путь к папке Семантика в документах системы
        private string GetCurrentDocsPath()
        {
            // - Определяем текущую ОС
            var builder = AppBuilder.Configure<App>();
            var os = builder.RuntimePlatform.GetRuntimeInfo().OperatingSystem;

            // - В зависимости от ОС выбираем путь к папке Семантика в документах
            return os switch
            {
                OperatingSystemType.Linux => _linuxDocsPath,
                _ => _winDocsPath,
            };
        }

        private void LoadSettingsFromFile()
        {
            using (StreamReader read = new StreamReader(_currentDocsPath))
            {
                ideLanguage = JsonConvert.DeserializeObject<IdeLanguageModel>(read.ReadToEnd());
            }
        }
    }
}