using System;
using System.Collections.Generic;

namespace Noppes.E621.DbExport
{
    /// <summary>
    /// Represents a post sourced from a database export.
    /// </summary>
    public class DbExportPost
    {
        /// <summary>
        /// The ID of the post.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The ID of the user who uploaded the post. Null in case the account has been deleted.
        /// </summary>
        public int UploaderId { get; set; }

        /// <summary>
        /// When the post was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// The MD5 hash of the contents of the post's file.
        /// </summary>
        public string Md5 { get; set; } = null!;

        /// <summary>
        /// The post's sources.
        /// </summary>
        public ICollection<string> Sources { get; set; } = null!;

        /// <summary>
        /// Rating of the post.
        /// </summary>
        public PostRating Rating { get; set; }

        /// <summary>
        /// The width in pixels of the post's file.
        /// </summary>
        public int FileWidth { get; set; }

        /// <summary>
        /// The height in pixels of the post's file.
        /// </summary>
        public int FileHeight { get; set; }

        /// <summary>
        /// The extension of the post's file.
        /// </summary>
        public string FileExtension { get; set; } = null!;

        /// <summary>
        /// The size of the post's file in bytes.
        /// </summary>
        public int FileSize { get; set; }

        /// <summary>
        /// The URL where to post's file can be found.
        /// </summary>
        public Uri FileLocation { get; set; } = null!;

        /// <summary>
        /// The names of the tags for the post.
        /// </summary>
        public ICollection<string> Tags { get; set; } = null!;

        /// <summary>
        /// The names of the tags which are locked for the post.
        /// </summary>
        public ICollection<string> LockedTags { get; set; } = null!;

        /// <summary>
        /// The number of users who favorited the post.
        /// </summary>
        public int FavoriteCount { get; set; }

        /// <summary>
        /// The post's parent. Null if the post doesn't have a parent post.
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// An ID that increases when the post is altered on e621. This happens when any of the
        /// following properties change: tags, sources, description, rating, image, parent, approver
        /// and flags.
        /// </summary>
        public int ChangeSeq { get; set; }

        /// <summary>
        /// The ID of the user which approved the post.
        /// </summary>
        public int? ApproverId { get; set; }

        /// <summary>
        /// The number of comments left on the post.
        /// </summary>
        public int CommentCount { get; set; }

        /// <summary>
        /// The post's description. Null if the post has an empty description.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// The duration of the post's video.
        /// </summary>
        public TimeSpan? Duration { get; set; }

        /// <summary>
        /// The last time the post was updated. Null in case the post has never been updated before.
        /// </summary>
        public DateTimeOffset? UpdatedAt { get; set; }

        /// <summary>
        /// If the post has been deleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// If the post is pending approval.
        /// </summary>
        public bool IsPending { get; set; }

        /// <summary>
        /// If the post is flagged for deletion.
        /// </summary>
        public bool IsFlagged { get; set; }

        /// <summary>
        /// The number of upvotes the post has.
        /// </summary>
        public int ScoreUp { get; set; }

        /// <summary>
        /// The number of downvotes the post has.
        /// </summary>
        public int ScoreDown { get; set; }

        /// <summary>
        /// The total score the post has. Theoretically, this is the number of upvotes subtracted by
        /// the number of downvotes. However, what the API returns differs from this.
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// If the post’s rating has been locked.
        /// </summary>
        public bool IsRatingLocked { get; set; }

        /// <summary>
        /// If the post’s status has been locked. Null if a post has been deleted.
        /// </summary>
        public bool? IsStatusLocked { get; set; }

        /// <summary>
        /// If the post has its notes locked.
        /// </summary>
        public bool IsNoteLocked { get; set; }
    }
}
