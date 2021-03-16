using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

// -- ??? -- Здесь только русские -- ??? --

namespace Semantic
{
    public static class StringAnalyzer
    {
        public const string DumbSpecialSymbols = "§¶«¤©®";

        public const string DumbLetters = "_абвгдеёжзийклмнопрстуфхцчшщьыъэюяabcdefghijklmnopqrstuvwxyz\"'";
        private const string _digits = "1234567890";
        public const string DumbOperands = DumbLetters + _digits;
        private static readonly List<string> _specialSymbols = new List<string> {"'\\n'", "'\\t'", "'\\н'", "'\\т'"};
        public static IOptions Options { get; set; }

        public static bool IsGoodName(string name)
        {
            return !(name.Length == 0 || _digits.IndexOf(name[0]) != -1
                     || !name.ToLower().ToCharArray().All(ch => DumbLetters.IndexOf(ch) != -1 || _digits.IndexOf(ch) != -1));
        }

        public static string FormatString(string line)
        {
            //переписать на регулярки
            int s = 0;
            for (int i = 0; i < line.Length; i++)
            {
                switch (line[i])
                {
                    case 'r':
                        if (s == 0)
                        {
                            s = 1;
                        }
                        else if (s == 4)
                        {
                            s = 3;
                        }
                        break;
                    case '\'':
                        switch (s)
                        {
                            case 0:
                                s = 3;
                                break;
                            case 1:
                                s = 2;
                                break;
                            case 2:
                            case 3:
                                s = 0;
                                break;
                        }
                        break;

                    case '\\':
                        switch (s)
                        {
                            case 3:
                                s = 4;
                                break;
                            case 1:
                            case 0:
                                s = 5;
                                break;
                        }
                        break;

                    case 'n':
                    case 'н':
                        switch (s)
                        {
                            case 4:
                                line = line.Remove(i, 1);
                                line = line.Insert(i, "n");
                                s = 3;
                                break;
                            case 5:
                                line = line.Remove(i - 1, 2);
                                line = line.Insert(i - 1, "\r\n");
                                s = 0;
                                break;
                        }
                        break;

                    case 't':
                    case 'т':
                        switch (s)
                        {
                            case 4:
                                line = line.Remove(i, 1);
                                line = line.Insert(i, "t");
                                s = 3;
                                break;
                            case 5:
                                line = line.Remove(i - 1, 2);
                                line = line.Insert(i - 1, "\t");
                                i--;
                                s = 0;
                                break;
                        }
                        break;
                    default:
                        switch (s)
                        {
                            case 1:
                            case 5:
                                s = 0;
                                break;
                            case 4:
                                s = 3;
                                break;
                        }
                        break;
                }
            }
            return line;
        }

        public static bool IsInt(string expression)
        {
            return expression.Length > 0 && expression.All(Char.IsDigit);
        }

        public static bool IsReal(string expression)
        {
            double result;
            return double.TryParse(expression, NumberStyles.Float, CultureInfo.InvariantCulture, out result);
        }

        public static bool IsChar(string expression)
        {
            return expression.Length == 3 && (expression[0] == '\'' && expression[2] == '\'')
                   || _specialSymbols.Contains(expression);
        }

        public static bool IsString(string expression)
        {
            return expression.Length > 1 && expression[0] == '\"' && expression[expression.Length - 1] == '\"';
        }

        public static bool IsLogical(string expression)
        {
            return expression == SyntaxNames.True || expression == SyntaxNames.False;
        }

        public static bool IsClassOrModuleMember(string expression)
        {
            return expression.Contains(".");
        }

        public static string GetMemberName(string text)
        {
            int dotIndex = text.IndexOf(".", StringComparison.Ordinal);
            return text.Substring(dotIndex + 1, text.Length - dotIndex - 1);
        }

        public static string GetClassOrModuleName(string text)
        {
            int dotIndex = text.IndexOf(".", StringComparison.Ordinal);
            return text.Substring(0, dotIndex);
        }

        public static bool IsUserType(string type)
        {
            return !IsArray(type) && !IsSubprograme(type) && !IsPointer(type) && !SyntaxNames.Types.Contains(type);
        }

        public static bool IsArray(string expression)
        {
            return expression.Split(' ').FirstOrDefault() == SyntaxNames.Array;
        }

        public static bool IsSubprograme(string expression)
        {
            return expression.Split(' ').FirstOrDefault() == Strings.Subprogram;
        }

        public static bool IsPointer(string type)
        {
            return type.Split(' ').FirstOrDefault() == SyntaxNames.Pointer.Split(' ').FirstOrDefault();
        }

        /*
        public static string RtfToString(string rtf)
        {
            try
            {
                return new RichTextBox {Rtf = rtf}.Text;
            }
            catch
            {
                return "";
            }
        }

        public static string StringToRtf(string str)
        {
            try
            {
                Color foreColor = ((SolidColorBrush) Options.CommentColor).Color;
                return new RichTextBox
                    {
                        Text = str,
                        Font = new Font(Options.EditorFontFamily.Source, (float) Options.EditorFontSize),
                        ForeColor = System.Drawing.Color.FromArgb(foreColor.R, foreColor.G, foreColor.B)
                    }.Rtf;
            }
            catch
            {
                return
                    @"{\rtf1\ansi\ansicpg1252\uc1\htmautsp\deff2{\fonttbl{\f0\fcharset0 Times New Roman;}{\f2\fcharset0 Consolas;}}{\colortbl;\red255\green255\blue255;}\loch\hich\dbch\pard\plain\ltrpar\itap0{\lang1033\fs21\f2\cf0 \cf0\ql{\f2 {\lang1049\ltrch - }{\ltrch -}\li0\ri0\sa0\sb0\fi0\ql\par}&#xD;&#xA;}&#xD;&#xA;}";
            }
        }
        */

        public static bool CanBeChar(string text)
        {
            return text.Length > 1 && text[0] == '\'' && text[text.Length - 1] == '\'';
        }

        public static bool IsConstantValue(string name)
        {
            return IsInt(name) || IsReal(name) || IsChar(name) || IsLogical(name) || IsString(name);
        }

        public static int GetLevensteinDistance(string string1, string string2)
        {
            var m = new int[string1.Length + 1,string2.Length + 1];

            for (int i = 0; i <= string1.Length; i++)
            {
                m[i, 0] = i;
            }
            for (int j = 0; j <= string2.Length; j++)
            {
                m[0, j] = j;
            }

            for (int i = 1; i <= string1.Length; i++)
            {
                for (int j = 1; j <= string2.Length; j++)
                {
                    int diff = (string1[i - 1] == string2[j - 1]) ? 0 : 1;

                    m[i, j] = Math.Min(Math.Min(m[i - 1, j] + 1,
                                                m[i, j - 1] + 1),
                                       m[i - 1, j - 1] + diff);
                }
            }

            return m[string1.Length, string2.Length];
        }
    }
}