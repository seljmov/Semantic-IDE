using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Semantic.Language
{
    public class Dictionaries
    {
        static Dictionaries()
        {
            Dictionary = new FileDictionary();
        }
        
        public static IDictionary Dictionary { get; private set; }

        public static void UpdateCommonDictionary(string language)
        {
            var type = typeof(Strings);
            Dictionary.LoadInterface(language);

            foreach (var info in type.GetProperties()
                .Where(info => info.CanWrite && info.PropertyType == typeof(string)))
            {
                info.SetValue(null, Dictionary.GetInterfaceString(info.Name), null);
            }
        }
        
        public static void SaveCommonDictionary(string language)
        {
            var type = typeof(Strings);
            
            foreach (var info in type.GetProperties()
                .Where(info => info.CanWrite && info.PropertyType == typeof(string)))
            {
                Dictionary.SaveString(info.Name, language, (string) info.GetValue(null, null));
            }
        }
        
        public static void SaveSyntaxDictionary(string language, string syntax)
        {
            var type = typeof(SyntaxNames);
            
            foreach (var info in type.GetProperties()
                .Where(info => info.CanWrite && info.PropertyType == typeof(string)))
            {
                Dictionary.SaveString(info.Name, language, syntax,  (string) info.GetValue(null, null));
            }
        }

        public static void UpdateSyntaxDictionary(string language, string syntax)
        {
            var type = typeof(Strings);
            Dictionary.LoadSyntax(language + "_" + syntax);

            foreach (var info in type.GetProperties()
                .Where(info => info.CanWrite && info.PropertyType == typeof(string)))
            {
                info.SetValue(null, Dictionary.GetInterfaceString(info.Name), null);
            }

            SyntaxNames.ProhibitedNames = new List<string>
            {
                SyntaxNames.Module,
                SyntaxNames.Type,
                SyntaxNames.Function,
                SyntaxNames.Return,
                SyntaxNames.Procedure,
                SyntaxNames.Variable,
                SyntaxNames.InitializedVariable,
                SyntaxNames.Constant,
                SyntaxNames.Call,
                SyntaxNames.Assign,
                SyntaxNames.If,
                SyntaxNames.Else,
                SyntaxNames.ElseIf,
                SyntaxNames.Beginning,
                SyntaxNames.While,
                SyntaxNames.DoNTimes,
                SyntaxNames.Loop,
                SyntaxNames.Output,
                SyntaxNames.Input,
                SyntaxNames.Field,
                SyntaxNames.MethodFunction,
                SyntaxNames.MethodProcedure,
                SyntaxNames.KeyWordText,
                SyntaxNames.Import,
                SyntaxNames.Integer,
                SyntaxNames.Real,
                SyntaxNames.Boolean,
                SyntaxNames.Character,
                SyntaxNames.Pointer,
                SyntaxNames.True,
                SyntaxNames.False,
                SyntaxNames.OrOperation,
                SyntaxNames.AndOperation,
                SyntaxNames.EqualOperation,
                SyntaxNames.NotEqualOperation,
                SyntaxNames.GreaterOperation,
                SyntaxNames.GreaterOrEqualOperation,
                SyntaxNames.LessOperation,
                SyntaxNames.LessOrEqualOperation,
                SyntaxNames.PlusOperation,
                SyntaxNames.MinusOperation,
                SyntaxNames.MultiplyOperation,
                SyntaxNames.DivideOperation,
                SyntaxNames.IntegerDivideOperation,
                SyntaxNames.ModOperation,
                SyntaxNames.IsOperation,
                SyntaxNames.AsOperation,
                SyntaxNames.UnaryMinusOperation,
                SyntaxNames.NotOperation,
                SyntaxNames.Private,
                SyntaxNames.Public,
                SyntaxNames.Readonly,
                SyntaxNames.In,
                SyntaxNames.Ref,
                SyntaxNames.Out,
                SyntaxNames.EndText,
                SyntaxNames.New,
                SyntaxNames.Nil,
                SyntaxNames.Delete,
            };

            SyntaxNames.Keywords = new List<string>
            {
                SyntaxNames.Module,
                SyntaxNames.Type,
                SyntaxNames.Function,
                SyntaxNames.Return,
                SyntaxNames.Procedure,
                SyntaxNames.Variable,
                SyntaxNames.Constant,
                SyntaxNames.Call,
                SyntaxNames.Assign,
                SyntaxNames.If,
                SyntaxNames.Else,
                SyntaxNames.ElseIf,
                SyntaxNames.Beginning,
                SyntaxNames.While,
                SyntaxNames.DoNTimes,
                SyntaxNames.Loop,
                SyntaxNames.Output,
                SyntaxNames.Input,
                SyntaxNames.Field,
                SyntaxNames.MethodFunction,
                SyntaxNames.MethodProcedure,
                SyntaxNames.KeyWordText,
                SyntaxNames.Import,
                SyntaxNames.Delete,
            };

            SyntaxNames.Types = new List<string>
            {
                SyntaxNames.Integer,
                SyntaxNames.Real,
                SyntaxNames.Boolean,
                SyntaxNames.Character,
            };

            SyntaxNames.Booleans = new List<string>
            {
                SyntaxNames.True,
                SyntaxNames.False
            };

            SyntaxNames.UnaryOperations = new List<string>
            {
                SyntaxNames.UnaryMinusOperation,
                SyntaxNames.NotOperation,
            };

            SyntaxNames.BinaryOperations = new List<string>
            {
                SyntaxNames.OrOperation,
                SyntaxNames.AndOperation,
                SyntaxNames.EqualOperation,
                SyntaxNames.NotEqualOperation,
                SyntaxNames.GreaterOperation,
                SyntaxNames.GreaterOrEqualOperation,
                SyntaxNames.LessOperation,
                SyntaxNames.LessOrEqualOperation,
                SyntaxNames.PlusOperation,
                SyntaxNames.MinusOperation,
                SyntaxNames.MultiplyOperation,
                SyntaxNames.DivideOperation,
                SyntaxNames.IntegerDivideOperation,
                SyntaxNames.ModOperation,
                SyntaxNames.IsOperation,
                SyntaxNames.AsOperation,
            };
        }

        public static void SetInterfaceDictionary(string name)
            => UpdateCommonDictionary(name);

        public static void SetSyntaxDictionary(string language, string syntax)
            => UpdateSyntaxDictionary(language, syntax);

        public static List<string> GetDictionaries(bool languageEditorMode = false)
        {
            var languages = new List<string>();
            var path = IO.RootDirectory + @"Resources\Interface";
            var directoryInfo = new DirectoryInfo(path);

            foreach (var info in directoryInfo.GetFiles()
                .Where(info => info.Extension.ToLower().Equals(".txt")))
            {
                if (languageEditorMode || FileDictionary.IsDictionaryFileCorrect(info))
                {
                    languages.Add(info.Name.Replace(".txt", ""));
                }
            }

            return languages;
        }
    }
}