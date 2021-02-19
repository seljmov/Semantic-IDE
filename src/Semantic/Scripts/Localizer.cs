using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using Avalonia;
using Avalonia.Platform;
using Newtonsoft.Json;

namespace Semantic.Scripts
{
    public class Localizer : INotifyPropertyChanged
    {
        private const string IndexerName = "Item";
        private const string IndexerArrayName = "Item[]";
        private Dictionary<string, string>? _strings = null;

        private static Localizer? _instance = null;

        public Localizer() { }

        public Dictionary<string, string> Strings { get; private set; } = new Dictionary<string, string>();

        public string? Language { get; private set; }

        // - Загрузка указанной локализации из .Json файла
        public bool LoadLanguage(string language)
        {
            Language = language;
            // - В переменную загружается вся информация о папке Assets
            var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();

            // - Составляем запрос для поиска нужной локализации
            Uri uri = new Uri($"avares://Semantic/Assets/Languages/{language}.json");
            // - Если такая локализация существует в Assets, то грузим
            if (assets.Exists(uri))
            {
                using (StreamReader sr = new StreamReader(assets.Open(uri), Encoding.UTF8))
                {
                    _strings = JsonConvert.DeserializeObject<Dictionary<string, string>>(sr.ReadToEnd());
                }
                Invalidate();
                Strings = _strings;

                return true;
            }
            return false;
        }

        // - Данная функция вставляет слово/фразу из словаря в верстку,
        // - если, конечно, слово/фраза есть в словаре.
        public string this[string key]
        {
            // - Если в словаре есть требуемое, то вернется, иначе вернется строка типа {Language}:{key},
            // - например, ru-RU: WelcomePage, где ru-RU - локализация, а WelcomePage - искомый ключ
            get
            {
                string? res;
                if (_strings != null && _strings.TryGetValue(key, out res))
                {
                    return res.Replace("\\n", "\n");
                }
                return $"{Language}:{key}";
            }
        }

        // - Состояние класса, используем Singletone
        public static Localizer GetInstance()
        {
            if (_instance == null) 
            {
                _instance = new Localizer();
            }
            return _instance;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void Invalidate()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(IndexerName));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(IndexerArrayName));
        }
    }
}