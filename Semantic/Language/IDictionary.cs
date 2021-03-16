namespace Semantic.Language
{
    public interface IDictionary
    {
        void LoadInterface(string language);
        void LoadSyntax(string syntax);

        string? GetInterfaceString(string name);
        string? GetSyntaxString(string name);

        void SaveString(string name, string language, string value);
        void SaveString(string name, string language, string syntaxLang, string value);
    }
}