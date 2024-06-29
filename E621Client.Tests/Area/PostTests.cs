using FluentAssertions;

namespace Noppes.E621.Tests.Area;

[TestFixture]
public class PostTests
{
    [Test]
    public async Task GetPostAsync_PostWithId4113987_ReturnsExpected()
    {
        var post = await TestsHelper.E621Client.GetPostAsync(4113987);

        AssertPost(post);
    }

    [Test]
    public async Task GetPostAsync_PostWithMd5d964e4f896f07ef694720902fcbb072b_ReturnsExpected()
    {
        var post = await TestsHelper.E621Client.GetPostAsync("d964e4f896f07ef694720902fcbb072b");

        AssertPost(post);
    }

    private static void AssertPost(Post? post)
    {
        post.Should().NotBeNull();
        post!.FavoriteCount.Should().BeGreaterThan(500);
        post.CreatedAt.ToUniversalTime().Should().Be(new DateTimeOffset(new DateOnly(2023, 6, 16), new TimeOnly(14, 32, 29, 860), TimeSpan.Zero));
        post.Flags.IsDeleted.Should().BeFalse();
        post.Rating.Should().Be(PostRating.Explicit);
        post.Description.Should().NotBeNullOrWhiteSpace();
        post.Tags.Character.Should().Contain("noppes");

        post.File.Should().NotBeNull();
        post.File!.Width.Should().Be(1754);
        post.File.Height.Should().Be(2048);
        post.File.FileExtension.Should().Be("jpg");
        post.File.Size.Should().Be(218804);
        post.File.Md5.Should().Be("d964e4f896f07ef694720902fcbb072b");
        post.File.Location.AbsoluteUri.Should().Be("https://static1.e621.net/data/d9/64/d964e4f896f07ef694720902fcbb072b.jpg");

        post.Preview.Should().NotBeNull();
        post.Preview!.Width.Should().Be(128);
        post.Preview.Height.Should().Be(150);
        post.Preview.Location.AbsoluteUri.Should().Be("https://static1.e621.net/data/preview/d9/64/d964e4f896f07ef694720902fcbb072b.jpg");

        post.Sample.Should().NotBeNull();
        post.Sample!.Has.Should().BeTrue();
        post.Sample.Width.Should().Be(850);
        post.Sample.Height.Should().Be(992);
        post.Sample.Location.AbsoluteUri.Should().Be("https://static1.e621.net/data/sample/d9/64/d964e4f896f07ef694720902fcbb072b.jpg");
    }

    [Test]
    public async Task GetPostAsync_PostWithNonExistentId_ReturnsNull()
    {
        var post = await TestsHelper.E621Client.GetPostAsync(int.MaxValue);

        post.Should().BeNull();
    }

    [Test]
    public async Task GetPostAsync_PostWithNonExistentMd5_ReturnsNull()
    {
        var post = await TestsHelper.E621Client.GetPostAsync("daaacec348b8edfea72dcdafc9f9a67d"); // "noppes" = daaacec348b8edfea72dcdafc9f9a67d

        post.Should().BeNull();
    }
}
