using CsvHelper.Configuration.Attributes;

namespace Noppes.E621
{
    internal class DbExportPoolRaw
    {
        [Name("id")]
        public int Id { get; set; }

        [Name("name")]
        public string Name { get; set; } = null!;

        [Name("created_at")]
        public string CreatedAt { get; set; } = null!;

        [Name("updated_at")]
        public string UpdatedAt { get; set; } = null!;

        [Name("creator_id")]
        public int CreatorId { get; set; }

        [Name("description")]
        public string? Description { get; set; }

        [Name("is_active")]
        public string IsActive { get; set; } = null!;

        [Name("category")]
        public string Category { get; set; } = null!;

        [Name("post_ids")]
        public string PostIds { get; set; } = null!;
    }
}
