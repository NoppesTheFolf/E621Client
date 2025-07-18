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
        public Task<IList<Flag>?> GetFlagsAsync(int postId);

        /// <summary>
        /// Retrieves flags for one or more posts, with their descriptions
        /// </summary>
        /// <param name="postId">The IDs of one or more posts to check.</param>
        public Task<IList<Flag>?> GetFlagsAsync(IEnumerable<int> postIds, int limit = 320);
    }
}
