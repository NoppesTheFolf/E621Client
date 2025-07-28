using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Noppes.E621.Converters
{
    /// <summary>
    /// Converts between the annoying format related tags are represented in. 
    /// </summary>
    internal class RelatedTagsConverter : StringJsonConverter<ICollection<RelatedTag>>
    {
        private const string EmptyArray = "[]";

        protected override ICollection<RelatedTag> ReadString(string? value, Type objectType, ICollection<RelatedTag>? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (value == EmptyArray || string.IsNullOrWhiteSpace(value))
                return new List<RelatedTag>();

            var relatedTags = value.Split(' ');

            if (relatedTags.Length % 2 == 1)
                throw new ArgumentException("The provided string of related tags doesn't consist of an even number of pairs.", nameof(value));

            return Enumerable.Range(0, relatedTags.Length)
                .Where(i => i % 2 == 0)
                .Select(i =>
                {
                    if (!int.TryParse(relatedTags[i + 1], out var weight))
                        throw new ArgumentException("The provided string of related tags isn't in the expected format.", nameof(value));

                    return new RelatedTag
                    {
                        Name = relatedTags[i],
                        Weight = weight
                    };
                })
                .ToList();
        }

        protected override string AsString(ICollection<RelatedTag>? value)
        {
            return value == null || value.Count == 0 ? EmptyArray : string.Join(' ', value.Select(v => $"{v.Name} {v.Weight}"));
        }
    }
}
