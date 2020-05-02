using System;
using System.Linq;

namespace Noppes.E621
{
    public static class TagHelper
    {
        public static string[] ParseSearchString(string? tags)
        {
            if (tags == null)
                return Array.Empty<string>();

            return tags
                .Split(' ')
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToArray();
        }
    }
}
