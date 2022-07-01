using CsvHelper.Configuration.Attributes;

namespace Noppes.E621.DbExport
{
    internal class DbExportTagImplicationRaw
    {
        [Name("id")]
        public int Id { get; set; }

        [Name("antecedent_name")]
        public string AntecedentName { get; set; } = null!;

        [Name("consequent_name")]
        public string ConsequentName { get; set; } = null!;

        [Name("created_at")]
        public string? CreatedAt { get; set; }

        [Name("status")]
        public string Status { get; set; } = null!;
    }
}
