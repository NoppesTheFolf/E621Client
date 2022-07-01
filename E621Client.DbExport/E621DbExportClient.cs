using CsvHelper;
using Flurl;
using HtmlAgilityPack;
using Humanizer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Noppes.E621.DbExport
{
    /// <summary>
    /// This client is responsible for interacting with database exports of e621.
    /// </summary>
    public interface IE621DbExportClient
    {
        /// <summary>
        /// Get all the database exports which can be downloaded.
        /// </summary>
        Task<ICollection<DbExport>> GetDbExportsAsync();

        /// <summary>
        /// Get a <see cref="Stream"/> based on the given database export. This stream can be
        /// directly read by any of the methods prefixed "ReadStreamAs". for example <see
        /// cref="ReadStreamAsPostsDbExportAsync"/>. The returned stream can also be saved to a file
        /// and you can then feed that file into any of the methods. Use the stream in whatever way
        /// makes sense for your usage scenario.
        /// </summary>
        Task<Stream> GetDbExportStreamAsync(DbExport export);

        /// <summary>
        /// Read the provided <see cref="Stream"/> in the format in which posts are exported.
        /// </summary>
        IAsyncEnumerable<DbExportPost> ReadStreamAsPostsDbExportAsync(Stream stream);

        /// <summary>
        /// Read the provided <see cref="Stream"/> in the format in which pools are exported.
        /// </summary>
        IAsyncEnumerable<DbExportPool> ReadStreamAsPoolsDbExportAsync(Stream stream);

        /// <summary>
        /// Read the provided <see cref="Stream"/> in the format in which tags are exported.
        /// </summary>
        IAsyncEnumerable<DbExportTag> ReadStreamAsTagsDbExportAsync(Stream stream);

        /// <summary>
        /// Read the provided <see cref="Stream"/> in the format in which tag implications are exported.
        /// </summary>
        IAsyncEnumerable<DbExportTagImplication> ReadStreamAsTagImplicationsDbExportAsync(Stream stream);

        /// <summary>
        /// Read the provided <see cref="Stream"/> in the format in which tag aliases are exported.
        /// </summary>
        IAsyncEnumerable<DbExportTagAlias> ReadStreamAsTagAliasesDbExportAsync(Stream stream);
    }

    /// <summary>
    /// This client is responsible for interacting with database exports of e621. You can create
    /// this class by calling <see cref="E621ClientExtensions.GetDbExportClient"/> on your <see cref="IE621Client"/>.
    /// </summary>
    public class E621DbExportClient : IE621DbExportClient
    {
        private const string Route = "/db_export/";
        private const string DateFormat = "yyyy-MM-dd";
        private const string TimeFormat = "HH:mm:ss.FFFFFF";
        private const string DateTimeFormat = DateFormat + " " + TimeFormat;

        private readonly IE621Client _e621Client;

        internal E621DbExportClient(IE621Client e621Client)
        {
            if (e621Client.Imageboard == Imageboard.E926)
                throw new ArgumentException("e926 does not support database exports. You need to use a client configured to use e621.", nameof(e621Client));

            _e621Client = e621Client;
        }

        /// <inheritdoc/>
        public async Task<Stream> GetDbExportStreamAsync(DbExport export)
        {
            var url = Url.Combine(Route, export.FileName);
            var stream = await _e621Client.GetStreamAsync(url, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

            return new GZipStream(stream, CompressionMode.Decompress);
        }

        /// <inheritdoc/>
        public IAsyncEnumerable<DbExportPost> ReadStreamAsPostsDbExportAsync(Stream stream)
        {
            return ReadStreamAsDbExportAsync<DbExportPostRaw, DbExportPost>(stream, record =>
            {
                var post = new DbExportPost
                {
                    Id = record.Id,
                    UploaderId = record.UploaderId,
                    CreatedAt = DateTimeOffset.ParseExact(record.CreatedAt, DateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal),
                    Md5 = record.Md5,
                    Sources = string.IsNullOrWhiteSpace(record.Source) ? Array.Empty<string>() : record.Source.Split('\n', StringSplitOptions.RemoveEmptyEntries),
                    Rating = PostRatingHelper.FromAbbreviation(record.Rating),
                    FileWidth = record.ImageWidth,
                    FileHeight = record.ImageHeight,
                    Tags = record.Tags == null ? Array.Empty<string>() : record.Tags.Split(' ', StringSplitOptions.RemoveEmptyEntries),
                    LockedTags = record.LockedTags == null ? Array.Empty<string>() : record.LockedTags.Split(' ', StringSplitOptions.RemoveEmptyEntries),
                    FavoriteCount = record.FavoriteCount,
                    FileExtension = record.FileExtension,
                    ParentId = record.ParentId,
                    ChangeSeq = record.ChangeSeq,
                    ApproverId = record.ApproverId,
                    FileSize = record.FileSize,
                    CommentCount = record.CommentCount,
                    Description = string.IsNullOrWhiteSpace(record.Description) ? null : record.Description,
                    UpdatedAt = string.IsNullOrWhiteSpace(record.UpdatedAt) ? (DateTimeOffset?)null : DateTimeOffset.ParseExact(record.UpdatedAt, DateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal),
                    ScoreUp = record.ScoreUp,
                    ScoreDown = record.ScoreDown,
                    Score = record.Score,
                    IsDeleted = (bool)StringToBool(record.IsDeleted)!,
                    IsPending = (bool)StringToBool(record.IsPending)!,
                    IsFlagged = (bool)StringToBool(record.IsFlagged)!,
                    IsRatingLocked = (bool)StringToBool(record.IsRatingLocked)!,
                    IsStatusLocked = StringToBool(record.IsStatusLocked),
                    IsNoteLocked = (bool)StringToBool(record.IsNoteLocked)!,
                    FileLocation = new Uri($"https://static1.e621.net/data/{record.Md5[..2]}/{record.Md5[2..4]}/{record.Md5}.{record.FileExtension}")
                };

                if (record.Duration != null)
                    post.Duration = TimeSpan.FromSeconds((float)record.Duration);

                return post;
            });
        }

        /// <inheritdoc/>
        public IAsyncEnumerable<DbExportPool> ReadStreamAsPoolsDbExportAsync(Stream stream)
        {
            return ReadStreamAsDbExportAsync<DbExportPoolRaw, DbExportPool>(stream, record => new DbExportPool
            {
                Id = record.Id,
                Name = record.Name,
                CreatedAt = DateTimeOffset.ParseExact(record.CreatedAt, DateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal),
                UpdatedAt = DateTimeOffset.ParseExact(record.UpdatedAt, DateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal),
                CreatorId = record.CreatorId,
                Description = string.IsNullOrWhiteSpace(record.Description) ? null : record.Description,
                IsActive = (bool)StringToBool(record.IsActive)!,
                Category = Enum.Parse<PoolCategory>(record.Category.Pascalize()),
                PostIds = record.PostIds[1..^1].Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()
            });
        }

        private static bool? StringToBool(string? value)
        {
            return value switch
            {
                "t" => true,
                "f" => false,
                null => null,
                "" => null,
                _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
            };
        }

        /// <inheritdoc/>
        public IAsyncEnumerable<DbExportTag> ReadStreamAsTagsDbExportAsync(Stream stream)
        {
            return ReadStreamAsDbExportAsync<DbExportTagRaw, DbExportTag>(stream, record => new DbExportTag
            {
                Id = record.Id,
                Name = record.Name,
                Category = (TagCategory)record.Category,
                Count = record.PostCount
            });
        }

        /// <inheritdoc/>
        public IAsyncEnumerable<DbExportTagImplication> ReadStreamAsTagImplicationsDbExportAsync(Stream stream)
        {
            return ReadStreamAsDbExportAsync<DbExportTagImplicationRaw, DbExportTagImplication>(stream, record => new DbExportTagImplication
            {
                Id = record.Id,
                AntecedentName = record.AntecedentName,
                ConsequentName = record.ConsequentName,
                CreatedAt = string.IsNullOrWhiteSpace(record.CreatedAt) ? (DateTimeOffset?)null : DateTimeOffset.ParseExact(record.CreatedAt, DateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal),
                Status = Enum.Parse<TagImplicationStatus>(record.Status.Pascalize())
            });
        }

        /// <inheritdoc/>
        public IAsyncEnumerable<DbExportTagAlias> ReadStreamAsTagAliasesDbExportAsync(Stream stream)
        {
            return ReadStreamAsDbExportAsync<DbExportTagAliasRaw, DbExportTagAlias>(stream, record =>
            {
                if (!Enum.TryParse<TagAliasStatus>(record.Status.Pascalize(), out var status))
                    status = TagAliasStatus.Other;

                return new DbExportTagAlias
                {
                    Id = record.Id,
                    AntecedentName = record.AntecedentName,
                    ConsequentName = record.ConsequentName,
                    CreatedAt = string.IsNullOrWhiteSpace(record.CreatedAt) ? (DateTimeOffset?)null : DateTimeOffset.ParseExact(record.CreatedAt, DateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal),
                    Status = status
                };
            });
        }

        private static async IAsyncEnumerable<TProcessed> ReadStreamAsDbExportAsync<TRaw, TProcessed>(Stream stream, Func<TRaw, TProcessed> map)
        {
            using var streamReader = new StreamReader(stream);
            using var csv = new CsvReader(streamReader, CultureInfo.InvariantCulture);

            await foreach (var record in csv.GetRecordsAsync<TRaw>().ConfigureAwait(false))
                yield return map(record);
        }

        /// <inheritdoc/>
        public async Task<ICollection<DbExport>> GetDbExportsAsync()
        {
            // Get the index page of https://e621.net/db_export/
            await using var stream = await _e621Client.GetStreamAsync(Route).ConfigureAwait(false);
            var htmlDocument = new HtmlDocument();
            htmlDocument.Load(stream);

            // Get all of the HTML nodes that contain information about a database export
            var nodes = htmlDocument.DocumentNode.SelectNodes("/html/body/pre/a")
                .Select(x => new
                {
                    Text = x.InnerText,
                    Href = x.Attributes["href"].Value
                })
                .Where(x => x.Href != "../");

            // Extract information from the HTML nodes
            var exports = nodes.Select(x =>
            {
                // The names of exports come in the following format: tag_implications-2022-06-27.csv.gz
                // We split at the first dash one: ["tag_implications", "2022-06-27.csv.gz"]
                var parts = x.Text.Split('-', 2);
                var (name, leftover) = (parts[0], parts[1]);

                // Then we split the leftover part at the first dot: ["2022-06-27", "csv.gz"]
                parts = leftover.Split('.', 2);
                var date = DateTimeOffset.ParseExact(parts[0], DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);

                // Turn tag_implications --> TagImplication and parse that as an enum
                var typeName = name.Singularize().Pascalize();
                var type = Enum.Parse<DbExportType>(typeName);

                return new DbExport(type, date, x.Href);
            }).ToList();

            return exports;
        }
    }

    /// <summary>
    /// Extensions methods for <see cref="IE621Client"/>.
    /// </summary>
    public static class E621ClientExtensions
    {
        /// <summary>
        /// Get a client which can interact with the database exports of e621. The used <see
        /// cref="IE621Client"/> needs to be configured to use e621, not e926. e926 does not support
        /// database exports.
        /// </summary>
        public static IE621DbExportClient GetDbExportClient(this IE621Client e621Client)
        {
            return new E621DbExportClient(e621Client);
        }
    }
}
