using Avalonia.Media;

namespace Semantic
{
    public interface IOptions
    {
        FontFamily EditorFontFamily { get; set; }
        double EditorFontSize { get; set; }
        double EditorFontSizeInPixels { get; set; }
        FontFamily InterfaceFontFamily { get; set; }
        double InterfaceFontSize { get; set; }
        double InterfaceFontSizeInPixels { get; set; }
        double LineHeight { get; set; }
        double TabSize { get; set; }
        double ComposeButtonSize { get; set; }
        double ComposeButtonFontSize { get; set; }
        int ColorLineWidth { get; }
        double ParametersMinWidth { get; }
        Brush Background { get; set; }
        Brush Foreground { get; set; }
        Brush MouseOverColor { get; set; }
        Brush SelectionColor { get; set; }
        Brush TypeColor { get; set; }
        Brush KeyWordColor { get; set; }
        Brush StringColor { get; set; }
        Brush CommentColor { get; set; }
        Brush BasicLineColor { get; set; }
        Brush SubprogramLineColor { get; set; }
        Brush SwitchLineColor { get; set; }
        Brush CycleLineColor { get; set; }
        Brush FakeWordColor { get; set; }
        string CurrentLanguage { get; set; }
        string CurrentInterface { get; set; }
        string CurrentSyntax { get; set; }
        string CopyMode { get; set; }
        Brush ErrorItemColor { get; set; }
        Brush ModificationItemColor { get; set; }
    }
}