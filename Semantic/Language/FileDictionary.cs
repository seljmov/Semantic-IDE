using System;
using System.Collections.Generic;
using System.IO;

namespace Semantic.Language
{
    public class FileDictionary : IDictionary
    {
        private const string _newLineReplacement = "&x0051;";
        private const string _resourcesLexis = @"Resources\Lexis\";
        private const string _resourcesInterface = @"Resources\Interface\";
        
        public Dictionary<string, string> InterfaceLines { get; set; }
        public Dictionary<string, string> SyntaxLines { get; set; }

        public static bool IsThereInterfaceFile(string languageName)
        {
            var fi = new FileInfo(IO.RootDirectory + _resourcesInterface + languageName + ".txt");
            return fi.Exists;
        }

        public static bool IsThereLexisFileForThisLanguage(string language, string lexis)
        {
            var fi = new FileInfo(IO.RootDirectory + _resourcesLexis + language + "_" + lexis + ".txt");
            return fi.Exists;
        }

        public void LoadInterface(string language) 
            => InterfaceLines = LoadFile(_resourcesInterface + language);

        public void LoadSyntax(string syntax)
            => SyntaxLines = LoadFile(_resourcesLexis + syntax);

        public string? GetInterfaceString(string name)
            => InterfaceLines.ContainsKey(name) ? InterfaceLines[name] : null;

        public string? GetSyntaxString(string name)
            => SyntaxLines.ContainsKey(name) ? SyntaxLines[name] : null;

        public void SaveString(string name, string language, string value)
        {
            var path = IO.RootDirectory + _resourcesInterface + language + ".txt";
            var writer = new StreamWriter(path, true);
            var line = name + "=" + value.Replace("\n", _newLineReplacement);
            writer.WriteLine(line);
            writer.Close();
        }

        public void SaveString(string name, string language, string syntaxLang, string value)
        {
            var path = IO.RootDirectory + _resourcesLexis + language + "_" + syntaxLang + ".txt";
            var writer = new StreamWriter(path, true);
            var line = name + "=" + value.Replace("\n", _newLineReplacement); 
            writer.WriteLine(line);
            writer.Close();
        }

        private static Dictionary<string, string> LoadFile(string filename)
        {
            var fi = new FileInfo(IO.RootDirectory + filename + ".txt");
            var lines = new Dictionary<string, string>();
            using var reader = fi.OpenText();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line != null)
                {
                    var index = line.IndexOf("=", StringComparison.Ordinal);
                    var identifier = line.Substring(0, index).Replace("<", "").Replace(">", "");
                    var production = line.Substring(index + 1, line.Length - index - 1);
                    lines.Add(identifier, production.Replace(_newLineReplacement, "\n"));
                }
            }

            return lines;
        }

        public static bool IsDictionaryFileCorrect(FileInfo info)
        {
            var containsBlank = false;
            using var reader = info.OpenText();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line != null)
                {
                    var index = line.IndexOf("=", StringComparison.Ordinal);
                    var production = line.Substring(index + 1, line.Length - index - 1);
                    if (production.Equals("") || production.Trim().Equals(""))
                    {
                        containsBlank = true;
                        break;;
                    }
                }
            }

            return !containsBlank;
        }

        public static bool IsLanguageFileCorrect(string language, string syntax)
        {
            var path = IO.RootDirectory + _resourcesLexis + language + "_" + syntax + ".txt";
            var fi = new FileInfo(path);
            return IsDictionaryFileCorrect(fi);
        }
    }
}