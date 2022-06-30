using CsvHelper.Configuration.Attributes;

namespace Noppes.E621.DbExport
{
    internal class DbExportTagRaw
    {
        [Name("id")]
        public int Id { get; set; }

        [Name("name")]
        public string Name { get; set; } = null!;

        [Name("category")]
        public int Category { get; set; }

        [Name("post_count")]
        public int PostCount { get; set; }
    }
}
