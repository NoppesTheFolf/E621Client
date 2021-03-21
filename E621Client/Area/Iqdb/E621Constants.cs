using System.Collections.Generic;
using System.Linq;

namespace Noppes.E621
{
    // Contains constants related to e621's IQDB area.
    public static partial class E621Constants
    {
        /// <summary>
        /// The image formats IQDB supports. This <see cref="IReadOnlyDictionary{TKey,TValue}"/>
        /// contains both the abbreviation of the image format (JPEG, PNG, GIF) and their
        /// corresponding file extensions. However, you are unlikely to need this. Take a look at
        /// <see cref="IqdbAllowedFormatNames"/> and <see cref="IqdbAllowedFormatExtensions"/> first.
        /// </summary>
        public static readonly IReadOnlyDictionary<string, string[]> IqdbAllowedFormats = new Dictionary<string, string[]>
        {
            {"JPEG", new[] {"jpg", "jpeg"}},
            {"PNG", new[] {"png"}},
            {"GIF", new[] {"gif"}}
        };

        /// <summary>
        /// The names of the image formats IQDB supports.
        /// </summary>
        public static readonly IReadOnlyCollection<string> IqdbAllowedFormatNames = IqdbAllowedFormats
            .Select(iaif => iaif.Key)
            .ToHashSet();

        /// <summary>
        /// The file extensions of the image formats IQDB supports.
        /// </summary>
        public static readonly IReadOnlyCollection<string> IqdbAllowedFormatExtensions = IqdbAllowedFormats
            .SelectMany(iaif => iaif.Value)
            .Select(e => '.' + e)
            .ToHashSet();
    }
}
