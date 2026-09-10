using CsvHelper;
using Humanizer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Noppes.E621
{
    /// <summary>
    /// Provides methods to read the <see cref="Stream"/> of a database export in the format of the
    /// different exports which are available.
    /// </summary>
    public static class DbExportReader
    {
        private const string DateFormat = "yyyy-MM-dd";
        private const string TimeFormat = "HH:mm:ss.FFFFFF";
        private const string DateTimeFormat = DateFormat + " " + TimeFormat;

        /// <summary>
        /// Read the provided <see cref="Stream"/> in the format in which posts are exported.
        /// </summary>
        public static IAsyncEnumerable<DbExportPost> ReadStreamAsPostsDbExportAsync(Stream stream)
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

        /// <summary>
        /// Read the provided <see cref="Stream"/> in the format in which pools are exported.
        /// </summary>
        public static IAsyncEnumerable<DbExportPool> ReadStreamAsPoolsDbExportAsync(Stream stream)
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

        /// <summary>
        /// Read the provided <see cref="Stream"/> in the format in which tags are exported.
        /// </summary>
        public static IAsyncEnumerable<DbExportTag> ReadStreamAsTagsDbExportAsync(Stream stream)
        {
            return ReadStreamAsDbExportAsync<DbExportTagRaw, DbExportTag>(stream, record => new DbExportTag
            {
                Id = record.Id,
                Name = record.Name,
                Category = (TagCategory)record.Category,
                Count = record.PostCount
            });
        }

        /// <summary>
        /// Read the provided <see cref="Stream"/> in the format in which tag implications are exported.
        /// </summary>
        public static IAsyncEnumerable<DbExportTagImplication> ReadStreamAsTagImplicationsDbExportAsync(Stream stream)
        {
            return ReadStreamAsDbExportAsync<DbExportTagImplicationRaw, DbExportTagImplication>(stream, record =>
            {
                if (!Enum.TryParse<TagStatus>(record.Status.Pascalize(), out var status))
                    status = TagStatus.Other;

                return new DbExportTagImplication
                {
                    Id = record.Id,
                    AntecedentName = record.AntecedentName,
                    ConsequentName = record.ConsequentName,
                    CreatedAt = string.IsNullOrWhiteSpace(record.CreatedAt) ? (DateTimeOffset?)null : DateTimeOffset.ParseExact(record.CreatedAt, DateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal),
                    Status = status
                };
            });
        }

        /// <summary>
        /// Read the provided <see cref="Stream"/> in the format in which tag aliases are exported.
        /// </summary>
        public static IAsyncEnumerable<DbExportTagAlias> ReadStreamAsTagAliasesDbExportAsync(Stream stream)
        {
            return ReadStreamAsDbExportAsync<DbExportTagAliasRaw, DbExportTagAlias>(stream, record =>
            {
                if (!Enum.TryParse<TagStatus>(record.Status.Pascalize(), out var status))
                    status = TagStatus.Other;

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

        private static async IAsyncEnumerable<TProcessed> ReadStreamAsDbExportAsync<TRaw, TProcessed>(Stream stream, Func<TRaw, TProcessed> map)
        {
            using var streamReader = new StreamReader(stream);
            using var csv = new CsvReader(streamReader, CultureInfo.InvariantCulture);

            await foreach (var record in csv.GetRecordsAsync<TRaw>().ConfigureAwait(false))
                yield return map(record);
        }
    }
}
