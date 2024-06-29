using FluentAssertions;

namespace Noppes.E621.Tests.Area;

[TestFixture]
public class PoolTests
{
    [Test]
    public async Task GetPoolAsync_PoolWithId4458_ReturnsExpected()
    {
        var pool = await TestsHelper.E621Client.GetPoolAsync(4458);

        pool.Should().NotBeNull();
        pool!.Id.Should().Be(4458);
        pool.Name.Should().Be("Weekend – by Zeta-Haru");
        pool.CreatedAt.ToUniversalTime().Should().Be(new DateTimeOffset(new DateOnly(2014, 7, 30), new TimeOnly(9, 25, 2, 262), TimeSpan.Zero));
        pool.PostIds.Should().Contain(514723);
    }

    [Test]
    public async Task GetPoolAsync_PoolWithNonExistentId_ReturnsNull()
    {
        var pool = await TestsHelper.E621Client.GetPoolAsync(int.MaxValue);

        pool.Should().BeNull();
    }
}
