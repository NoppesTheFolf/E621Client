using Shouldly;

namespace Noppes.E621.Tests.Area;

[TestFixture]
public class PostFlagTests
{
    [Test]
    public async Task GetFlagsAsync_PostWithId2776401_ReturnsExpected()
    {
        var postFlags = await TestsHelper.E621Client.GetPostFlagsAsync([2776401], 1);

        postFlags.ShouldNotBeNull();
        postFlags.Count.ShouldBe(1);

        var postFlag = postFlags.Single();
        postFlag.Id.ShouldBe(525148);
        postFlag.CreatedAt.ToUniversalTime().ShouldBe(new DateTimeOffset(new DateOnly(2022, 4, 21), new TimeOnly(16, 35, 33, 916), TimeSpan.Zero));
        postFlag.PostId.ShouldBe(2776401);
        postFlag.Reason.ShouldBe("takedown #15214: Artist requested removal");
        postFlag.CreatorId.ShouldBe(360277);
        postFlag.IsResolved.ShouldBe(false);
        postFlag.UpdatedAt!.Value.ToUniversalTime().ShouldBe(new DateTimeOffset(new DateOnly(2022, 4, 21), new TimeOnly(16, 35, 33, 916), TimeSpan.Zero));
        postFlag.IsDeletion.ShouldBe(true);
        postFlag.Type.ShouldBe("deletion");
    }

    [Test]
    public async Task GetFlagsAsync_PostWithNonExistentId_ReturnsEmpty()
    {
        var postFlags = await TestsHelper.E621Client.GetPostFlagsAsync([int.MaxValue]);

        postFlags.ShouldBeEmpty();
    }
}
