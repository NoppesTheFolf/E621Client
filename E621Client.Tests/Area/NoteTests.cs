using Shouldly;

namespace Noppes.E621.Tests.Area;

[TestFixture]
public class NoteTests
{
    [Test]
    public async Task GetNotesAsync_NoParameters_ReturnsExpected()
    {
        var notes = await TestsHelper.E621Client.GetNotesAsync(null);

        notes.Count.ShouldBe(E621Constants.NotesMaximumLimit);
    }

    [Test]
    public async Task GetNotesAsync_PostWithId4113987_ReturnsExpected()
    {
        var notes = await TestsHelper.E621Client.GetNotesAsync([5746029]);

        notes.Count.ShouldBe(3);

        var note = notes.First();
        note.Id.ShouldBe(456764);
        note.CreatedAt.ToUniversalTime().ShouldBe(new DateTimeOffset(new DateOnly(2025, 7, 31), new TimeOnly(4, 17, 29, 927), TimeSpan.Zero));
        note.UpdatedAt.ShouldBe(note.CreatedAt);
        note.CreatorId.ShouldBe(285207);
        note.X.ShouldBe(3461);
        note.Y.ShouldBe(106);
        note.Width.ShouldBe(111);
        note.Height.ShouldBe(81);
        note.Version.ShouldBe(2);
        note.IsActive.ShouldBe(true);
        note.PostId.ShouldBe(5746029);
        note.Body.ShouldBe("Wulf?");
        note.CreatorName.ShouldBe("kokhee118");
    }

    [Test]
    public async Task GetNotesAsync_PostWithNonExistentId_ReturnsEmpty()
    {
        var notes = await TestsHelper.E621Client.GetNotesAsync([int.MaxValue]);

        notes.ShouldBeEmpty();
    }
}
