using System.Collections.Generic;
using System.Threading.Tasks;

namespace Noppes.E621
{
    // Interface for the Flag part of the E621Client (added by Jdbye)
    public partial interface IE621Client
    {
        /// <summary>
        /// Retrieves notes using the given parameters.
        /// </summary>
        /// <param name="postIds">The IDs of the posts for which to retrieve notes.</param>
        /// <param name="isActive">Whether to fetch only active notes.</param>
        /// <param name="limit">
        /// The maximum number of notes to retrieve in a single call. Maximum defined at <see cref="E621Constants.NotesMaximumLimit"/>.
        /// </param>
        public Task<ICollection<Note>> GetNotesAsync(IEnumerable<int>? postIds, bool? isActive = null, int limit = E621Constants.NotesMaximumLimit);
    }
}
