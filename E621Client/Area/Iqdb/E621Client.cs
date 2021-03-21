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

        /// <inheritdoc/>
        public Task<ICollection<IqdbPost>?> QueryIqdbByUrlAsync(string url, bool activeOnly = true)
        {
            return QueryIqdbAsync(content =>
            {
                content.AddString(MultipartUrlName, url);
            }, activeOnly, HttpStatusCode.InternalServerError);
        }

        /// <inheritdoc/>
        public Task<ICollection<IqdbPost>> QueryIqdbByStreamAsync(Stream stream, bool activeOnly = true)
        {
#pragma warning disable 8619 // Nullability of reference types in value doesn't match target type. Null values will only be returned in case defaultStatusCodes are provided.
            return QueryIqdbAsync(content =>
            {
                content.AddFile(MultipartFileName, stream, "image");
            }, activeOnly);
#pragma warning restore 8619 // Nullability of reference types in value doesn't match target type.
        }

        /// <inheritdoc/>
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