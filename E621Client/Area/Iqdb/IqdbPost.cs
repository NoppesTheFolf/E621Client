using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Noppes.E621.Converters;
using System;
using System.Collections.Generic;

namespace Noppes.E621
{
    internal class IqdbApiPost
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("created_at"), JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("updated_at"), JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset? UpdatedAt { get; set; }

        [JsonProperty("image_width")]
        public int FileWidth { get; set; }

        [JsonProperty("image_height")]
        public int FileHeight { get; set; }

        [JsonProperty("file_size")]
        public int FileSize { get; set; }

        [JsonProperty("md5")]
        public string? FileMd5 { get; set; }

        [JsonProperty("file_url"), JsonConverter(typeof(UriConverter))]
        public Uri? FileLocation { get; set; }

        [JsonProperty("up_score")]
        public int ScoreUp { get; set; }

        [JsonProperty("down_score")]
        public int ScoreDown { get; set; }

        [JsonProperty("score")]
        public int Score { get; set; }

        [JsonProperty("uploader_id")]
        public int? UploaderId { get; set; }

        [JsonProperty("source"), JsonConverter(typeof(NewlineSplitConverter<string>))]
        public ICollection<string> Sources { get; set; } = null!;

        [JsonProperty("rating"), JsonConverter(typeof(PostRatingConverter))]
        public PostRating Rating { get; set; }

        [JsonProperty("fav_count")]
        public int FavoriteCount { get; set; }

        [JsonProperty("preview_file_url"), JsonConverter(typeof(UriConverter))]
        public Uri PreviewLocation { get; set; } = null!;

        [JsonProperty("large_file_url"), JsonConverter(typeof(UriConverter))]
        public Uri SampleLocation { get; set; } = null!;

        [JsonProperty("tag_string_general"), JsonConverter(typeof(WhitespaceSplitConverter<string>))]
        public ICollection<string> GeneralTags { get; set; } = null!;

        [JsonProperty("tag_string_species"), JsonConverter(typeof(WhitespaceSplitConverter<string>))]
        public ICollection<string> SpeciesTags { get; set; } = null!;

        [JsonProperty("tag_string_character"), JsonConverter(typeof(WhitespaceSplitConverter<string>))]
        public ICollection<string> CharacterTags { get; set; } = null!;

        [JsonProperty("tag_string_copyright"), JsonConverter(typeof(WhitespaceSplitConverter<string>))]
        public ICollection<string> CopyrightTags { get; set; } = null!;

        [JsonProperty("tag_string_artist"), JsonConverter(typeof(WhitespaceSplitConverter<string>))]
        public ICollection<string> ArtistTags { get; set; } = null!;

        [JsonProperty("tag_string_invalid"), JsonConverter(typeof(WhitespaceSplitConverter<string>))]
        public ICollection<string> InvalidTags { get; set; } = null!;

        [JsonProperty("tag_string_lore"), JsonConverter(typeof(WhitespaceSplitConverter<string>))]
        public ICollection<string> LoreTags { get; set; } = null!;

        [JsonProperty("tag_string_meta"), JsonConverter(typeof(WhitespaceSplitConverter<string>))]
        public ICollection<string> MetaTags { get; set; } = null!;

        [JsonProperty("locked_tags", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(WhitespaceSplitConverter<string>))]
        public ICollection<string>? LockedTags { get; set; }

        [JsonProperty("change_seq")]
        public int ChangeSeq { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; } = null!;

        [JsonProperty("approver_id")]
        public int? ApproverId { get; set; }

        [JsonProperty("pool_ids")]
        public ICollection<int> Pools { get; set; } = null!;

        [JsonProperty("parent_id")]
        public int? ParentId { get; set; }

        [JsonProperty("has_children")]
        public bool HasChildren { get; set; }

        [JsonProperty("has_active_children")]
        public bool HasActiveChildren { get; set; }

        [JsonProperty("children_ids", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(WhitespaceSplitConverter<int>))]
        public ICollection<int>? Children { get; set; }

        [JsonProperty("is_pending")]
        public bool IsPending { get; set; }

        [JsonProperty("is_flagged")]
        public bool IsFlagged { get; set; }

        [JsonProperty("is_note_locked")]
        public bool IsNoteLocked { get; set; }

        [JsonProperty("is_status_locked")]
        public bool? IsStatusLocked { get; set; }

        [JsonProperty("is_rating_locked")]
        public bool IsRatingLocked { get; set; }

        [JsonProperty("is_deleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("comment_count")]
        public int CommentCount { get; set; }

        [JsonProperty("is_favorited")]
        public bool IsFavorite { get; set; }

        [JsonProperty("last_noted_at"), JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset? LastNotedAt { get; set; }

        [JsonProperty("duration")]
        public float? Duration { get; set; }

        public IqdbPost AsPost()
        {
            var post = new IqdbPost
            {
                Id = Id,
                CreatedAt = CreatedAt,
                UpdatedAt = UpdatedAt,
                Score = new Score
                {
                    Up = ScoreUp,
                    Down = ScoreDown
                },
                Tags = new TagCollection
                {
                    General = GeneralTags,
                    Species = SpeciesTags,
                    Character = CharacterTags,
                    Copyright = CopyrightTags,
                    Artist = ArtistTags,
                    Invalid = InvalidTags,
                    Lore = LoreTags,
                    Meta = MetaTags
                },
                LockedTags = LockedTags ?? Array.Empty<string>(),
                ChangeSeq = ChangeSeq,
                Description = Description,
                UploaderId = UploaderId,
                ApproverId = ApproverId,
                FavoriteCount = FavoriteCount,
                Pools = Pools,
                Relationships = new Relationships
                {
                    ParentId = ParentId,
                    HasChildren = HasChildren,
                    HasActiveChildren = HasActiveChildren,
                    Children = Children ?? Array.Empty<int>()
                },
                Sources = Sources,
                Rating = Rating,
                Flags = new PostFlags
                {
                    IsPending = IsPending,
                    IsFlagged = IsFlagged,
                    IsNoteLocked = IsNoteLocked,
                    IsStatusLocked = IsStatusLocked,
                    IsRatingLocked = IsRatingLocked,
                    IsDeleted = IsDeleted
                },
                CommentCount = CommentCount,
                IsFavorite = IsFavorite,
                HasNotes = LastNotedAt != null,
                Duration = Duration != null ? TimeSpan.FromSeconds((float)Duration) : (TimeSpan?)null
        };

            if (IsDeleted)
                return post;

#pragma warning disable CS8601 // Possible null reference assignment. The values will only be null if the post has been deleted.
            post.File = new PostFileImage
            {
                Height = FileHeight,
                Location = FileLocation,
                Md5 = FileMd5,
                Width = FileWidth,
                Size = FileSize
            };
#pragma warning restore CS8601 // Possible null reference assignment.
            post.Preview = new PostImage
            {
                Location = PreviewLocation,
                Width = -1,
                Height = -1
            };
            post.Sample = new PostSampleImage
            {
                Location = SampleLocation,
                Has = SampleLocation != FileLocation,
                Width = -1,
                Height = -1
            };

            return post;
        }
    }
}
