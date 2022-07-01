using CsvHelper.Configuration.Attributes;

namespace Noppes.E621.DbExport
{
    internal class DbExportPostRaw
    {
        [Name("id")]
        public int Id { get; set; }

        [Name("uploader_id")]
        public int UploaderId { get; set; }

        [Name("created_at")]
        public string CreatedAt { get; set; } = null!;

        [Name("md5")]
        public string Md5 { get; set; } = null!;

        [Name("source")]
        public string? Source { get; set; }

        [Name("rating")]
        public string Rating { get; set; } = null!;

        [Name("image_width")]
        public int ImageWidth { get; set; }

        [Name("image_height")]
        public int ImageHeight { get; set; }

        [Name("tag_string")]
        public string? Tags { get; set; }

        [Name("locked_tags")]
        public string? LockedTags { get; set; }

        [Name("fav_count")]
        public int FavoriteCount { get; set; }

        [Name("file_ext")]
        public string FileExtension { get; set; } = null!;

        [Name("parent_id")]
        public int? ParentId { get; set; }

        [Name("change_seq")]
        public int ChangeSeq { get; set; }

        [Name("approver_id")]
        public int? ApproverId { get; set; }

        [Name("file_size")]
        public int FileSize { get; set; }

        [Name("comment_count")]
        public int CommentCount { get; set; }

        [Name("description")]
        public string? Description { get; set; }

        [Name("duration")]
        public float? Duration { get; set; }

        [Name("updated_at")]
        public string? UpdatedAt { get; set; }

        [Name("is_deleted")]
        public string IsDeleted { get; set; } = null!;

        [Name("is_pending")]
        public string IsPending { get; set; } = null!;

        [Name("is_flagged")]
        public string IsFlagged { get; set; } = null!;

        [Name("up_score")]
        public int ScoreUp { get; set; }

        [Name("down_score")]
        public int ScoreDown { get; set; }

        [Name("score")]
        public int Score { get; set; }

        [Name("is_rating_locked")]
        public string IsRatingLocked { get; set; } = null!;

        [Name("is_status_locked")]
        public string? IsStatusLocked { get; set; }

        [Name("is_note_locked")]
        public string IsNoteLocked { get; set; } = null!;
    }
}
