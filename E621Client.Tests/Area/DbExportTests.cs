using Shouldly;

namespace Noppes.E621.Tests.Area;

[TestFixture]
public class DbExportTests
{
    [Test]
    public async Task GetDbExportsAsync_NoParameters_ReturnsAllExports()
    {
        var exports = await TestsHelper.E621Client.GetDbExportsAsync();

        exports.Select(x => x.Name).ShouldBe(
        [
            DbExportType.Artist,
            DbExportType.BulkUpdateRequest,
            DbExportType.Pool,
            DbExportType.Post,
            DbExportType.PostReplacement,
            DbExportType.PostVersion,
            DbExportType.Tag,
            DbExportType.TagAlias,
            DbExportType.TagImplication,
            DbExportType.WikiPage,
        ], ignoreOrder: true);

        foreach (var export in exports)
        {
            export.FileName.ShouldNotBeNullOrWhiteSpace();
            export.FileSize.ShouldBeGreaterThan(0);
            export.Checksum.ShouldNotBeNullOrWhiteSpace();
            export.UpdatedAt.ShouldNotBe(default);
            export.Url.IsAbsoluteUri.ShouldBeTrue();
            export.Url.AbsoluteUri.ShouldEndWith(export.FileName);
        }
    }
}
