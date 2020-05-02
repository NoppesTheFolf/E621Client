using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Noppes.E621.Converters;
using System;
using System.Collections.Generic;

namespace Noppes.E621
{
    /// <summary>
    /// Represents a post. Contains the values each post, independent of its status, can have.
    /// </summary>
    public class Post
    {
        /// <summary>
        /// The ID of the post.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// When the post was created.
        /// </summary>
        [JsonProperty("created_at"), JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// The last time the post was updated. Null in case the post has never been updated before.
        /// </summary>
        [JsonProperty("updated_at"), JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTimeOffset? UpdatedAt { get; set; }

        /// <summary>
        /// The post's source image file. This is the highest quality image that is available for a
        /// post. This property might be null in case authorization is required to be allowed to
        /// view the post or in case the post has been deleted.
        /// </summary>
        [JsonProperty("file"), JsonConverter(typeof(PostImageConverter<PostFileImage>))]
        public PostFileImage? File { get; set; }

        /// <summary>
        /// A preview image that can be displayed in galleries and alike. The image is tiny and
        /// compressed. This property might be null in case authorization is required to be allowed
        /// to view the post or in case the post has been deleted.
        /// </summary>
        [JsonProperty("preview"), JsonConverter(typeof(PostImageConverter<PostImage>))]
        public PostImage? Preview { get; set; }

        /// <summary>
        /// A sample image that can be displayed to speed up loading times. This image is compressed
        /// and generally smaller than the source file. This property might be null in case
        /// authorization is required to be allowed to view the post or in case the post has been deleted.
        /// </summary>
        [JsonProperty("sample"), JsonConverter(typeof(PostImageConverter<PostSampleImage>))]
        public PostSampleImage? Sample { get; set; }

        /// <summary>
        /// The post's score.
        /// </summary>
        [JsonProperty("score")]
        public Score Score { get; set; } = null!;

        /// <summary>
        /// A categorized collection of tags.
        /// </summary>
        [JsonProperty("tags")]
        public TagCollection Tags { get; set; } = null!;

        /// <summary>
        /// The names of the tags that are locked on the post.
        /// </summary>
        [JsonProperty("locked_tags")]
        public ICollection<string> LockedTags { get; set; } = new List<string>();

        /// <summary>
        /// An ID that increases when the post is altered on e621. This happens when any of the
        /// following properties change: tags, sources, description, rating, image, parent, approver
        /// and flags.
        /// </summary>
        [JsonProperty("change_seq")]
        public int ChangeSeq { get; set; }

        /// <summary>
        /// The post's description. Null if the post has an empty description.
        /// </summary>
        [JsonProperty("description"), JsonConverter(typeof(EmptyStringConverter))]
        public string? Description { get; set; }

        /// <summary>
        /// The ID of the user who uploaded the post. Null in case the account has been deleted.
        /// </summary>
        [JsonProperty("uploader_id")]
        public int? UploaderId { get; set; }

        /// <summary>
        /// The ID of the user who approved the post. Null in case the post hasn't been approved yet
        /// or approver's account has been deleted.
        /// </summary>
        [JsonProperty("approver_id")]
        public int? ApproverId { get; set; }

        /// <summary>
        /// The number of users who favorited the post.
        /// </summary>
        [JsonProperty("fav_count")]
        public int FavoriteCount { get; set; }

        /// <summary>
        /// A collection of pool IDs the post is part of.
        /// </summary>
        [JsonProperty("pools")]
        public ICollection<int> Pools { get; set; } = new List<int>();

        /// <summary>
        /// The relationships the post has with other posts.
        /// </summary>
        [JsonProperty("relationships")]
        public Relationships Relationships { get; set; } = null!;

        /// <summary>
        /// The post's sources.
        /// </summary>
        [JsonProperty("sources")]
        public ICollection<string> Sources { get; set; } = new List<string>();

        /// <summary>
        /// Rating of the post.
        /// </summary>
        [JsonProperty("rating"), JsonConverter(typeof(PostRatingConverter))]
        public PostRating Rating { get; set; }

        /// <summary>
        /// The post's flags.
        /// </summary>
        [JsonProperty("flags")]
        public PostFlags Flags { get; set; } = null!;

        /// <summary>
        /// The number of comments the post has.
        /// </summary>
        [JsonProperty("comment_count")]
        public int CommentCount { get; set; }

        /// <summary>
        /// Whether or not the post has been favorited by the currently logged-in user.
        /// </summary>
        [JsonProperty("is_favorited")]
        public bool IsFavorite { get; set; }
    }
}
