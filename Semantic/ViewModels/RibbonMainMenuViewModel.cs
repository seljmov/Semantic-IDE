using System;
using Avalonia;
using ReactiveUI;

namespace Semantic.ViewModels
{
    public class RibbonMainMenuViewModel : ViewModelBase
    {
        private string _interfaceLanguage = "English";

        public int InterfaceLanguageIndex
        {
            get => _interfaceLanguage == "English" ? 1 : 0;
        }
    }
}
