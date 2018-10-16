using System.Globalization;
using System.Text;

namespace Lob.Net.Helpers
{
    internal static class StringExtensions
    {
        internal enum SnakeCaseState
        {
            Start,
            Lower,
            Upper,
            NewWord
        }

        private static char ToLower(char c)
        {
            c = char.ToLower(c, CultureInfo.InvariantCulture);
            return c;
        }

        internal static string ToPascalCase(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }

            var ns = s.Replace("_", " ");
            var info = CultureInfo.CurrentCulture.TextInfo;
            return info.ToTitleCase(ns).Replace(" ", string.Empty);
        }

        // Borrowed from Newstonsoft
        internal static string ToSnakeCase(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }

            var stringBuilder = new StringBuilder();
            var snakeCaseState = SnakeCaseState.Start;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == ' ')
                {
                    if (snakeCaseState != 0)
                    {
                        snakeCaseState = SnakeCaseState.NewWord;
                    }
                }
                else if (char.IsUpper(s[i]))
                {
                    switch (snakeCaseState)
                    {
                        case SnakeCaseState.Upper:
                            {
                                bool flag = i + 1 < s.Length;
                                if (i > 0 && flag)
                                {
                                    char c = s[i + 1];
                                    if (!char.IsUpper(c) && c != '_')
                                    {
                                        stringBuilder.Append('_');
                                    }
                                }
                                break;
                            }
                        case SnakeCaseState.Lower:
                        case SnakeCaseState.NewWord:
                            stringBuilder.Append('_');
                            break;
                    }

                    var value = char.ToLower(s[i], CultureInfo.InvariantCulture);
                    stringBuilder.Append(value);
                    snakeCaseState = SnakeCaseState.Upper;
                }
                else if (s[i] == '_')
                {
                    stringBuilder.Append('_');
                    snakeCaseState = SnakeCaseState.Start;
                }
                else
                {
                    if (snakeCaseState == SnakeCaseState.NewWord)
                    {
                        stringBuilder.Append('_');
                    }

                    stringBuilder.Append(s[i]);
                    snakeCaseState = SnakeCaseState.Lower;
                }
            }

            return stringBuilder.ToString();
        }
    }
}
