﻿using Shouldly;

namespace Noppes.E621.Tests.Area;

[TestFixture]
public class FavoriteTests
{
    [Test, Order(1)]
    public async Task AddFavoriteAsync_PostWithId546281_Returns()
    {
        await TestsHelper.E621Client.AddFavoriteAsync(546281);
    }

    [Test, Order(2)]
    public async Task AddFavoriteAsync_PostWithId546281AlreadyFavorited_Returns()
    {
        await TestsHelper.E621Client.AddFavoriteAsync(546281);
    }

    [Test, Order(3)]
    public async Task GetOwnFavoritesAsync_PostWithId546281Favorited_ReturnsExpected()
    {
        var favorites = await TestsHelper.E621Client.GetOwnFavoritesAsync();

        favorites.Select(x => x.Id).ShouldContain(546281);
    }

    [Test, Order(4)]
    public async Task RemoveFavoriteAsync_PostWithId546281_Returns()
    {
        await TestsHelper.E621Client.RemoveFavoriteAsync(546281);
    }

    [Test, Order(5)]
    public async Task RemoveFavoriteAsync_PostWithId546281AlreadyUnfavorited_Returns()
    {
        await TestsHelper.E621Client.RemoveFavoriteAsync(546281);
    }

    [Test, Order(6)]
    public async Task GetOwnFavoritesAsync_PostWithId546281NotFavorited_ReturnsExpected()
    {
        var favorites = await TestsHelper.E621Client.GetOwnFavoritesAsync();

        favorites.Select(x => x.Id).ShouldNotContain(546281);
    }

    [Test, Order(7)]
    public async Task GetOwnFavoritesAsync_Page2Limit1_ReturnsExpected()
    {
        var posts = await TestsHelper.E621Client.GetOwnFavoritesAsync(2, 1);

        posts.Count.ShouldBe(1);

        posts.Single().Id.ShouldBe(2290725);
    }

    [Test, Order(8)]
    public async Task GetFavoritesAsync_UserWithId1929205Page3Limit2_ReturnsExpected()
    {
        var posts = await TestsHelper.E621Client.GetFavoritesAsync(1929205, 3, 2);

        posts.ShouldNotBeNull();
        posts.Count.ShouldBe(2);
        posts.Skip(0).First().Id.ShouldBe(3744825);
        posts.Skip(1).First().Id.ShouldBe(4312488);
    }

    [Test, Order(9)]
    public async Task GetFavoritesAsync_UserWithNonExistentId_ReturnsExpected()
    {
        var posts = await TestsHelper.E621Client.GetFavoritesAsync(int.MaxValue);

        posts.ShouldBeNull();
    }

    [Test, Order(10)]
    public async Task GetFavoritesAsync_UserWithId563722HasPrivacyModeEnabled_ReturnsExpected()
    {
        var posts = await TestsHelper.E621Client.GetFavoritesAsync(563722);

        posts.ShouldBeEmpty();
    }
}
