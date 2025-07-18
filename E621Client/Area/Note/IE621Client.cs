using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Noppes.E621
{
    // Interface for the Flag part of the E621Client (added by Jdbye)
    public partial interface IE621Client
    {
        /// <summary>
        /// Retrieves flags for one or more posts, with their descriptions
        /// </summary>
        /// <param name="postId">The ID of a post to check.</param>
        public Task<IList<Note>?> GetNotesAsync(int postId, bool activeOnly = true);

        /// <summary>
        /// Retrieves flags for one or more posts, with their descriptions
        /// </summary>
        /// <param name="postIds">The IDs of one or more posts to check.</param>
        /// <param name=limit">Optional note limit, default 300 (max 320). This is not the post limit!</param>
        public Task<IList<Note>?> GetNotesAsync(IEnumerable<int> postIds, bool activeOnly = true, int limit = 320);
    }
}
