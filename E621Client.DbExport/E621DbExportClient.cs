﻿using CsvHelper;
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
        /// Read the provided <see cref="Stream"/> in the format in which tags are exported.
        /// </summary>
        IAsyncEnumerable<DbExportTag> ReadStreamAsTagsDbExportAsync(Stream stream);
    }

    /// <summary>
    /// This client is responsible for interacting with database exports of e621. You can create
    /// this class by calling <see cref="E621ClientExtensions.GetDbExportClient"/> on your <see cref="IE621Client"/>.
    /// </summary>
    public class E621DbExportClient : IE621DbExportClient
    {
        private const string Route = "/db_export/";

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
            var stream = await _e621Client.GetStreamAsync(url, HttpCompletionOption.ResponseHeadersRead);

            return new GZipStream(stream, CompressionMode.Decompress);
        }

        /// <inheritdoc/>
        public IAsyncEnumerable<DbExportPost> ReadStreamAsPostsDbExportAsync(Stream stream)
        {
            static bool? StringToBool(string? value)
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

            return ReadStreamAsDbExportAsync<DbExportPostRaw, DbExportPost>(stream, record =>
            {
                var post = new DbExportPost
                {
                    Id = record.Id,
                    UploaderId = record.UploaderId,
                    CreatedAt = record.CreatedAt,
                    Md5 = record.Md5,
                    Sources = record.Source == null ? Array.Empty<string>() : record.Source.Split('\n'),
                    Rating = PostRatingHelper.FromAbbreviation(record.Rating),
                    FileWidth = record.ImageWidth,
                    FileHeight = record.ImageHeight,
                    Tags = record.Tags == null ? Array.Empty<string>() : record.Tags.Split(' '),
                    LockedTags = record.LockedTags == null ? Array.Empty<string>() : record.LockedTags.Split(' '),
                    FavoriteCount = record.FavoriteCount,
                    FileExtension = record.FileExtension,
                    ParentId = record.ParentId,
                    ChangeSeq = record.ChangeSeq,
                    ApproverId = record.ApproverId,
                    FileSize = record.FileSize,
                    CommentCount = record.CommentCount,
                    Description = record.Description,
                    UpdatedAt = record.UpdatedAt,
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

        private static async IAsyncEnumerable<TProcessed> ReadStreamAsDbExportAsync<TRaw, TProcessed>(Stream stream, Func<TRaw, TProcessed> map)
        {
            using var streamReader = new StreamReader(stream);
            using var csv = new CsvReader(streamReader, CultureInfo.InvariantCulture);

            await foreach (var record in csv.GetRecordsAsync<TRaw>())
                yield return map(record);
        }

        /// <inheritdoc/>
        public async Task<ICollection<DbExport>> GetDbExportsAsync()
        {
            // Get the index page of https://e621.net/db_export/
            await using var stream = await _e621Client.GetStreamAsync(Route);
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
                var date = DateTime.ParseExact(parts[0], "yyyy-MM-dd", CultureInfo.InvariantCulture);

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
