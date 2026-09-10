using System.Text.RegularExpressions;

namespace Noppes.E621
{
    // Pulled from https://github.com/humanizr/humanizer
    internal static class HumanizerStringExtensions
    {
        private const string PascalizePattern = @"(?:[ _-]+|^)([a-zA-Z])";
        private static readonly Regex PascalizeRegexField = new Regex(PascalizePattern, RegexOptions.Compiled);
        private static Regex PascalizeRegex() => PascalizeRegexField;

        public static string Pascalize(this string input)
        {
            return PascalizeRegex().Replace(input, match => match
                .Groups[1]
                .Value.ToUpper());
        }
    }
}
