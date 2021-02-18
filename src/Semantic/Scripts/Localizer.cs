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
        public Dictionary<string, string>? _strings = null;

        public Localizer() { }

        public static Dictionary<string, string>? Strings { get; private set; }

        public string Language { get; private set; }

        public bool LoadLanguage(string language)
        {
            Language = language;
            var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();

            Uri uri = new Uri($"avares://Semantic/Assets/Languages/{language}.json");
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

        public string this[string key]
        {
            get 
            {
                string res;
                if (_strings != null && _strings.TryGetValue(key, out res))
                {
                    return res.Replace("\\n", "\n");
                }
                return $"{Language}:{key}";
            }
        }

        public static Localizer Instance { get; set; } = new Localizer();
        public event PropertyChangedEventHandler? PropertyChanged;

        public void Invalidate()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(IndexerName));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(IndexerArrayName));
        }
    }
}
