using Shouldly;

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
        post.ShouldNotBeNull();
        post!.FavoriteCount.ShouldBeGreaterThan(500);
        post.CreatedAt.ToUniversalTime().ShouldBe(new DateTimeOffset(new DateOnly(2023, 6, 16), new TimeOnly(14, 32, 29, 860), TimeSpan.Zero));
        post.Flags.IsDeleted.ShouldBeFalse();
        post.Rating.ShouldBe(PostRating.Explicit);
        post.Description.ShouldNotBeNullOrWhiteSpace();
        post.Tags.Character.ShouldContain("noppes");

        post.File.ShouldNotBeNull();
        post.File!.Width.ShouldBe(1754);
        post.File.Height.ShouldBe(2048);
        post.File.FileExtension.ShouldBe("jpg");
        post.File.Size.ShouldBe(218804);
        post.File.Md5.ShouldBe("d964e4f896f07ef694720902fcbb072b");
        post.File.Location!.AbsoluteUri.ShouldBe("https://static1.e621.net/data/d9/64/d964e4f896f07ef694720902fcbb072b.jpg");

        post.Preview.ShouldNotBeNull();
        post.Preview!.Width.ShouldBe(256);
        post.Preview.Height.ShouldBe(299);
        post.Preview.Location!.AbsoluteUri.ShouldBe("https://static1.e621.net/data/preview/d9/64/d964e4f896f07ef694720902fcbb072b.jpg");

        post.Sample.ShouldNotBeNull();
        post.Sample!.Has.ShouldBeTrue();
        post.Sample.Width.ShouldBe(850);
        post.Sample.Height.ShouldBe(992);
        post.Sample.Location!.AbsoluteUri.ShouldBe("https://static1.e621.net/data/sample/d9/64/d964e4f896f07ef694720902fcbb072b.jpg");
    }

    [Test]
    public async Task GetPostAsync_PostWithNonExistentId_ReturnsNull()
    {
        var post = await TestsHelper.E621Client.GetPostAsync(int.MaxValue);

        post.ShouldBeNull();
    }

    [Test]
    public async Task GetPostAsync_PostWithNonExistentMd5_ReturnsNull()
    {
        var post = await TestsHelper.E621Client.GetPostAsync("daaacec348b8edfea72dcdafc9f9a67d"); // "noppes" = daaacec348b8edfea72dcdafc9f9a67d

        post.ShouldBeNull();
    }
}
