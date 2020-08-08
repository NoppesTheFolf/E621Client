using Flurl.Http.Content;
using Newtonsoft.Json.Linq;
using Noppes.E621.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Noppes.E621
{
    public partial class E621Client
    {
        private const string MultipartUrlName = "url";

        private const string MultipartFileName = "file";

        // It seems like e621 doesn't use the same rules for the IQDB area of the API than they do
        // for the rest. That in turn causes 429 responses to be thrown. Waiting two seconds after
        // an IQDB query seems to prevent 429 responses.
        private const int IqdbRequestInterval = 0;
        private const int IqdbRequestDelay = 2000;

        /// <summary>
        /// The image formats IQDB supports. This <see cref="IReadOnlyDictionary{TKey,TValue}"/>
        /// contains both the abbreviation of the image format (JPEG, PNG, GIF) and their
        /// corresponding file extensions. However, you are unlikely to need this. Take a look at
        /// <see cref="IqdbAllowedFormatNames"/> and <see cref="IqdbAllowedFormatExtensions"/> first.
        /// </summary>
        public static readonly IReadOnlyDictionary<string, string[]> IqdbAllowedFormats = new Dictionary<string, string[]>
        {
            { "JPEG", new[] { "jpg", "jpeg" } },
            { "PNG", new[] { "png" } },
            { "GIF", new[] { "gif" } }
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

        /// <summary>
        /// Reverse search an image using an image located at the given URL. Note that e621 will not
        /// download files domains that aren't whitelisted. If you provided a URL that e621 does not
        /// trust, a <see cref="E621ClientForbiddenException"/> will be thrown. If you provided a
        /// URL that is whitelisted, but e621 is unable to retrieve the image for whatever reason, a
        /// null value will be returned.
        /// </summary>
        /// <param name="url">The url of the image.</param>
        /// <param name="activeOnly">Whether or not to only return posts which are not deleted.</param>
        public Task<ICollection<IqdbPost>?> QueryIqdbByUrlAsync(string url, bool activeOnly = true)
        {
            return QueryIqdbAsync(content =>
            {
                content.AddString(MultipartUrlName, url);
            }, activeOnly, HttpStatusCode.InternalServerError);
        }

        /// <summary>
        /// Reverse search an image using a stream that contains image data. If the stream doesn't
        /// contain valid data, an empty list will be returned.
        /// </summary>
        /// <param name="stream">The stream containing the image data.</param>
        /// <param name="activeOnly">Whether or not to only return posts which are not deleted.</param>
        public Task<ICollection<IqdbPost>> QueryIqdbByStreamAsync(Stream stream, bool activeOnly = true)
        {
#pragma warning disable 8619 // Nullability of reference types in value doesn't match target type. Null values will only be returned in case defaultStatusCodes are provided.
            return QueryIqdbAsync(content =>
            {
                content.AddFile(MultipartFileName, stream, "image");
            }, activeOnly);
#pragma warning restore 8619 // Nullability of reference types in value doesn't match target type.
        }

        /// <summary>
        /// Reverse search an image using an image stored on a filesystem. A <see
        /// cref="ArgumentException"/> will be thrown in case there doesn't exist an image at the
        /// given path. You SHOULD make sure the file extension of the file your submitting matches
        /// any of the file extensions defined in <see cref="IqdbAllowedFormatExtensions"/>.
        /// However, e621 will simply return no matches in case an invalid file is uploaded.
        /// </summary>
        /// <param name="path">The path where the image is located.</param>
        /// <param name="activeOnly">Whether or not to only return posts which are not deleted.</param>
        public Task<ICollection<IqdbPost>> QueryIqdbByFileAsync(string path, bool activeOnly = true)
        {
            if (!File.Exists(path))
                throw new ArgumentException($"There doesn't exist a file at `{path}`.");

#pragma warning disable 8619 // Nullability of reference types in value doesn't match target type. Null values will only be returned in case defaultStatusCodes are provided.
            return QueryIqdbAsync(content =>
            {
                content.AddFile(MultipartFileName, path);
            }, activeOnly);
#pragma warning restore 8619 // Nullability of reference types in value doesn't match target type.
        }

        private async Task<ICollection<IqdbPost>?> QueryIqdbAsync(Action<CapturedMultipartContent> buildContent, bool activeOnly, params HttpStatusCode[] defaultStatusCodes)
        {
            return await RequestAsync("/iqdb_queries.json", async request =>
            {
                return await request
                    .Authenticated(this)
                    .PostMultipartAsync<ICollection<IqdbPost>>(buildContent, token =>
                    {
                        if (token.Type == JTokenType.Null)
                            return Array.Empty<IqdbPost>();

                        // e621 returns `{ "posts": [] }` when an image doesn't have any matches
                        // instead of an empty array like you'd expect
                        if (token.Type != JTokenType.Array)
                            return Array.Empty<IqdbPost>();

                        var result = token
                            .Select(t => t.ToObject<IqdbPost>());

                        if (activeOnly)
                            result = result.Where(p => !p.Flags.IsDeleted);

                        return result
                            .OrderByDescending(p => p.IqdbScore)
                            .ToList();
                    }, false, defaultStatusCodes);
            }, IqdbRequestInterval, IqdbRequestDelay);
        }
    }
}
