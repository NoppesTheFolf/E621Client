using Shouldly;

namespace Noppes.E621.Tests.Area;

[TestFixture]
public class DbExportTests
{
    private const int SampleSize = 1000;

    [Test, Order(1)]
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

    [Test, Order(2)]
    public async Task ReadStreamAsPostsDbExportAsync_DbExportPosts_ReturnsExpected()
    {
        await using var stream = await GetExportStreamAsync(DbExportType.Post);
        var posts = await TakeAsync(DbExportReader.ReadStreamAsPostsDbExportAsync(stream), SampleSize);

        posts.ShouldNotBeEmpty();

        foreach (var post in posts)
        {
            post.Id.ShouldBeGreaterThan(0);
            post.CreatedAt.ShouldNotBe(default);
            post.Md5.ShouldNotBeNullOrWhiteSpace();
            post.Md5.Length.ShouldBe(32);
            post.FileExtension.ShouldNotBeNullOrWhiteSpace();
            post.FileSize.ShouldBeGreaterThan(0);
            post.Sources.ShouldNotBeNull();
            post.Tags.ShouldNotBeNull();
            post.LockedTags.ShouldNotBeNull();
            post.FileLocation.ShouldNotBeNull();
            post.FileLocation.AbsoluteUri.ShouldEndWith($"{post.Md5}.{post.FileExtension}");
            Enum.IsDefined(post.Rating).ShouldBeTrue();
        }
    }

    [Test, Order(3)]
    public async Task ReadStreamAsPoolsDbExportAsync_DbExportPools_ReturnsExpected()
    {
        await using var stream = await GetExportStreamAsync(DbExportType.Pool);
        var pools = await TakeAsync(DbExportReader.ReadStreamAsPoolsDbExportAsync(stream), SampleSize);

        pools.ShouldNotBeEmpty();

        foreach (var pool in pools)
        {
            pool.Id.ShouldBeGreaterThan(0);
            pool.Name.ShouldNotBeNullOrWhiteSpace();
            pool.CreatedAt.ShouldNotBe(default);
            pool.UpdatedAt.ShouldNotBe(default);
            pool.PostIds.ShouldNotBeNull();
            Enum.IsDefined(pool.Category).ShouldBeTrue();
        }
    }

    [Test, Order(4)]
    public async Task ReadStreamAsTagsDbExportAsync_DbExportTags_ReturnsExpected()
    {
        await using var stream = await GetExportStreamAsync(DbExportType.Tag);
        var tags = await TakeAsync(DbExportReader.ReadStreamAsTagsDbExportAsync(stream), SampleSize);

        tags.ShouldNotBeEmpty();

        foreach (var tag in tags)
        {
            tag.Id.ShouldBeGreaterThan(0);
            tag.Name.ShouldNotBeNullOrWhiteSpace();
            tag.Count.ShouldBeGreaterThanOrEqualTo(0);
            Enum.IsDefined(tag.Category).ShouldBeTrue();
        }
    }

    [Test, Order(5)]
    public async Task ReadStreamAsTagImplicationsDbExportAsync_DbExportTagImplications_ReturnsExpected()
    {
        await using var stream = await GetExportStreamAsync(DbExportType.TagImplication);
        var implications = await TakeAsync(DbExportReader.ReadStreamAsTagImplicationsDbExportAsync(stream), SampleSize);

        implications.ShouldNotBeEmpty();

        foreach (var implication in implications)
        {
            implication.Id.ShouldBeGreaterThan(0);
            implication.AntecedentName.ShouldNotBeNullOrWhiteSpace();
            implication.ConsequentName.ShouldNotBeNullOrWhiteSpace();
            Enum.IsDefined(implication.Status).ShouldBeTrue();
        }
    }

    [Test, Order(6)]
    public async Task ReadStreamAsTagAliasesDbExportAsync_DbExportTagAliases_ReturnsExpected()
    {
        await using var stream = await GetExportStreamAsync(DbExportType.TagAlias);
        var aliases = await TakeAsync(DbExportReader.ReadStreamAsTagAliasesDbExportAsync(stream), SampleSize);

        aliases.ShouldNotBeEmpty();

        foreach (var alias in aliases)
        {
            alias.Id.ShouldBeGreaterThan(0);
            alias.AntecedentName.ShouldNotBeNullOrWhiteSpace();
            alias.ConsequentName.ShouldNotBeNullOrWhiteSpace();
            Enum.IsDefined(alias.Status).ShouldBeTrue();
        }
    }

    private static async Task<Stream> GetExportStreamAsync(string exportType)
    {
        var exports = await TestsHelper.E621Client.GetDbExportsAsync();
        var export = exports.Single(x => x.Name == exportType);

        return await TestsHelper.E621Client.GetDbExportStreamAsync(export);
    }

    private static async Task<IReadOnlyList<T>> TakeAsync<T>(IAsyncEnumerable<T> source, int count)
    {
        var items = new List<T>(count);

        await foreach (var item in source)
        {
            items.Add(item);

            if (items.Count == count)
                break;
        }

        return items;
    }
}
