using Shouldly;

namespace Noppes.E621.Tests.Area;

[TestFixture]
public class TagTests
{
    [Test]
    public async Task GetTagAsync_TagWithId813847_ReturnsExpected()
    {
        var tag = await TestsHelper.E621Client.GetTagAsync(813847);

        AssertTag(tag);
    }

    [Test]
    public async Task GetTagAsync_TagWithNameNoppes_ReturnsExpected()
    {
        var tag = await TestsHelper.E621Client.GetTagAsync("noppes");

        AssertTag(tag);
    }

    private static void AssertTag(Tag? tag)
    {
        tag.ShouldNotBeNull();
        tag.Id.ShouldBe(813847);
        tag.Name.ShouldBe("noppes");
        tag.Count.ShouldBeGreaterThan(0);
        tag.RelatedTags.ShouldBeEmpty();
        tag.RelatedTagsUpdatedAt.ToUniversalTime().ShouldBe(new DateTimeOffset(new DateOnly(2020, 5, 20), new TimeOnly(18, 24, 31, 74), TimeSpan.Zero));
        tag.Category.ShouldBe(TagCategory.Character);
        tag.IsCategoryLocked.ShouldBe(false);
        tag.CreatedAt.ToUniversalTime().ShouldBe(new DateTimeOffset(new DateOnly(2020, 5, 20), new TimeOnly(18, 24, 31, 111), TimeSpan.Zero));
        tag.UpdatedAt.ToUniversalTime().ShouldBe(new DateTimeOffset(new DateOnly(2020, 5, 20), new TimeOnly(18, 24, 46, 561), TimeSpan.Zero));
    }

    [Test]
    public async Task GetTagAsync_TagWithNonExistentId_ReturnsNull()
    {
        var tag = await TestsHelper.E621Client.GetTagAsync(int.MaxValue);

        tag.ShouldBeNull();
    }

    [Test]
    public async Task GetTagAsync_TagWithNonExistentName_ReturnsNull()
    {
        var tag = await TestsHelper.E621Client.GetTagAsync("abcxyz");

        tag.ShouldBeNull();
    }
}
